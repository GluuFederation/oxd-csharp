using Newtonsoft.Json;
using oxdCSharp.UMA.CommandParameters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
//using System.Web.Http;
using System.Web.Mvc.Filters;
using System.Web.Routing;
using UMATestApi.Models;

namespace UMATestApi.Auth
{
    
    public class AuthorizationRequiredAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var check = new RSCheckAccess();

            string rpt = "";
            if (filterContext.RequestContext.HttpContext.Request.Headers.GetValues("RPT") != null)
                rpt = filterContext.RequestContext.HttpContext.Request.Headers.GetValues("RPT").First();

            var response = check.CheckAccess(rpt);

            if (response != null)
            {
                if (response.Status.ToLower() == "ok" && response.Data.Access.ToLower() == "granted")
                {
                    return;
                }
                else if (response.Status.ToLower() == "ok" && response.Data.Access.ToLower() == "denied")
                {
                    filterContext.HttpContext.Response.Headers.Add("ticket", response.Data.Ticket);
                }
            }

            base.OnAuthorization(filterContext);
        }
        
    }
    
}