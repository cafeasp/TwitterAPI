using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace TwitterMvcApp.Controllers
{
    public class HomeController : Controller
    {
        private string Key = System.Configuration.ConfigurationManager.AppSettings["Key"];
        private string Secret = System.Configuration.ConfigurationManager.AppSettings["Secret"];
        private string userToken = System.Configuration.ConfigurationManager.AppSettings["userToken"];
        private string userSecret = System.Configuration.ConfigurationManager.AppSettings["userSecret"];
        public ActionResult Index()
        {
            var t = new TwitterApi.Twitter();
            //var url = t.GetRequestToken(Key,Secret, "http://8b2ac84b.ngrok.io/Twitter/Auth");

            //return Redirect(url);
            t.UpdateStatus("Happy customers, Happy day", Key, Secret, userToken, userSecret);
            return View();
        }
        public ActionResult Welcome(string screenName)
        {
            ViewBag.ScreenName = screenName;
            return View();
        }
        
    }
}