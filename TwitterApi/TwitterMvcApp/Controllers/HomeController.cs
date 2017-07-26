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

        public ActionResult Index()
        {
            var t = new TwitterApi.Twitter();
           var url = t.GetRequestToken(Key,Secret, "http://735cec48.ngrok.io/Twitter/Auth");

            return Redirect(url);
            
        }
        public ActionResult Welcome(string screenName)
        {
            ViewBag.ScreenName = screenName;
            return View();
        }
        
    }
}