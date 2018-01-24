using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http;
namespace UMATestApi.Auth
{
    public class SecureCall_bkpAttribute : AuthorizeAttribute
    {
        private string _reason = "";
        public bool ByPassAuthorization { get; set; }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
            if (!string.IsNullOrEmpty(_reason))
                actionContext.Response.ReasonPhrase = _reason;

        }

        private IEnumerable<SecureApiAttribute> GetApiAuthorizeAttributes(HttpActionDescriptor descriptor)
        {
            return descriptor.GetCustomAttributes<SecureApiAttribute>(true)
                .Concat(descriptor.ControllerDescriptor.GetCustomAttributes<SecureApiAttribute>(true));
        }

        private bool IsSecuredApiCallRequested(HttpActionContext actionContext)
        {
            var apiAttributes = GetApiAuthorizeAttributes(actionContext.ActionDescriptor);
            if (apiAttributes != null && apiAttributes.Any())
                return true;
            return false;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (IsSecuredApiCallRequested(actionContext))
            {

                var queryParams = actionContext.Request.GetQueryNameValuePairs();
                if (queryParams.Any(x => x.Key.ToLower() == "requestToken") && queryParams.Any(x => x.Key.ToLower() == "epoch"))
                {
                    this.HandleUnauthorizedRequest(actionContext);
                    _reason = "Invalid Request , No RequestToken and Epoch";
                }
                else
                {
                    base.OnAuthorization(actionContext);
                }
            }
        }



        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if (ByPassAuthorization || GetApiAuthorizeAttributes(actionContext.ActionDescriptor).Any(x => x.ByPassAuthorization))
                return true;

            if (!this.IsValidRequestTokenWithEpoch(actionContext.Request.GetQueryNameValuePairs()))
            {
                this.HandleUnauthorizedRequest(actionContext);
                _reason = "Invalid Epoch or RequestToken, Access Denied";
                return false;
            }
            return base.IsAuthorized(actionContext);
        }

        private bool IsValidRequestTokenWithEpoch(IEnumerable<KeyValuePair<string, string>> QueryParams)
        {
            throw new NotImplementedException();
        }


    }
}