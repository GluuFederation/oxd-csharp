using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http;
using UMATestApi.Models;
using System.Web.Http.Filters;

namespace UMATestApi.Auth
{

    public class SecureApiAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            var check = new RSCheckAccess();

            string rpt = "";
            if (filterContext.Request.Headers.Contains("RPT"))
                rpt = filterContext.Request.Headers.GetValues("RPT").First();

            var response = check.CheckAccess(rpt);

            if (response != null)
            {
                if (response.Status.ToLower() == "ok" && response.Data.Access.ToLower() == "granted")
                {
                    return;
                }
                else if (response.Status.ToLower() == "ok" && response.Data.Access.ToLower() == "denied")
                {
                    var responseMessage = new HttpResponseMessage();
                    filterContext.Response = responseMessage;

                    filterContext.Response.Headers.Add("ticket", response.Data.Ticket);
                }
            }
            

            base.OnActionExecuting(filterContext);

        }
    }
}