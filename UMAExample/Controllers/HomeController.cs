using Newtonsoft.Json;
using oxdCSharp.UMA.CommandParameters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using UMATestApi.Auth;
using UMATestApi.Models;

namespace UMATestApi.Controllers
{
    
    public class HomeController : Controller
    {
        

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public string Add()
        {
            return "Add method called";
        }

        //[AuthorizationRequired]
        public string Update()
        {
            return "Update method called";
        }

        [AuthorizationRequired]
        public string GetAll()
        {
            return "Getting all objects";
        }

        [AuthorizationRequired]
        public string Get(int id)
        {
            return "Fetched Id: " + id;
        }
        
    }
}
