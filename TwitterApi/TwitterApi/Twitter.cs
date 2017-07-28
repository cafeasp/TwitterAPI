using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Extensions.MonoHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterApi
{
    public class Twitter
    {
        public void UpdateStatus(string status,string key,string secret,string token,string tokenSecret)
        {
            var client = new RestClient("https://api.twitter.com");

            var request = new RestRequest("/1.1/statuses/update.json", Method.POST);

            request.AddQueryParameter("status", status);

            client.Authenticator = OAuth1Authenticator.ForProtectedResource(key, secret, token, tokenSecret);

            var response = client.Execute(request);


        }
        public string GetRequestToken(string key,string secret,string callBackUrl)
        {
            var client = new RestClient("https://api.twitter.com");

            client.Authenticator = OAuth1Authenticator.ForRequestToken(
                key,
                secret,
                callBackUrl 
            );

            var request = new RestRequest("/oauth/request_token", Method.POST);
            var response = client.Execute(request);
           
            var qs = HttpUtility.ParseQueryString(response.Content);

            string oauthToken = qs["oauth_token"];
            string oauthTokenSecret = qs["oauth_token_secret"];

            request = new RestRequest("oauth/authorize?oauth_token=" + oauthToken);

            string url = client.BuildUri(request).ToString();
            return url;

        }

        public string GetAccessToken(string key, string secret,string oauth_token,string oauth_token_secret,string oauth_verifier)
        {
            var client = new RestClient("https://api.twitter.com");

            var request = new RestRequest("/oauth/access_token", Method.POST);

            //oauth_token_secret is unknown at this state of the application. Passing an empty string
            client.Authenticator = OAuth1Authenticator.ForAccessToken(key, secret, oauth_token,oauth_token_secret, oauth_verifier);
            
            var response = client.Execute(request);
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var qs = HttpUtility.ParseQueryString(response.Content);
                //we have token

                //TODO Save User Details in database
                string oauthToken = qs["oauth_token"];
                string oauthTokenSecret = qs["oauth_token_secret"];
                string userId = qs["user_id"];
                string screenName = qs["screen_name"];
                string xAuthExpires = qs["x_auth_expires"];

                return screenName;
            }
            else
            {
                //fail to get token
                return "Error";
            }
        }
    }
}
