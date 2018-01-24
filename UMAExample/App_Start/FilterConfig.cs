using System.Web;
using System.Web.Mvc;
using UMATestApi.Auth;

namespace UMATestApi
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new AuthorizationRequiredAttribute());
        }
    }
}
