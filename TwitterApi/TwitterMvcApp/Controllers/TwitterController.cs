using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TwitterMvcApp.Controllers
{
    public class TwitterController : Controller
    {
        private string Key = System.Configuration.ConfigurationManager.AppSettings["Key"];
        private string Secret = System.Configuration.ConfigurationManager.AppSettings["Secret"];

        public ActionResult Auth(string oauth_token,string oauth_verifier)
        {
            var t = new TwitterApi.Twitter();
            //the 4th parameter is blank since we don't have it
            //and accoding to twitter docs it's not needed https://dev.twitter.com/oauth/reference/post/oauth/access_token
            string result = t.GetAccessToken(Key, Secret, oauth_token, "", oauth_verifier);

            return RedirectToAction("Welcome", "Home",new { screenName = result });
        }
    }
}