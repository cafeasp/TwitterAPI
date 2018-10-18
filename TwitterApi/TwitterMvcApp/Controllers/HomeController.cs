using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TwitterApi;
using TwitterMvcApp.Models;
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
            var t = new Twitter();
            //var url = t.GetRequestToken(Key,Secret, "http://8b2ac84b.ngrok.io/Twitter/Auth");

            var timeLineAsString = t.GetTimeLine(Key,Secret,userToken,userSecret);

            if (!string.IsNullOrEmpty(timeLineAsString))
            {
                TwitterTimeLineModel[] twitterTimeLineModels = JsonConvert.DeserializeObject<TwitterTimeLineModel[]>(timeLineAsString);
                
            }



            //return Redirect(url);
            //t.UpdateStatus("Happy customers, Happy day", Key, Secret, userToken, userSecret);
            return View();
        }
        public ActionResult Welcome(string screenName)
        {
            ViewBag.ScreenName = screenName;
            return View();
        }
        
    }
}