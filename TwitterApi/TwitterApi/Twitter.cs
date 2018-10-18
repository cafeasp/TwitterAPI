using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Extensions.MonoHttp;


namespace TwitterApi
{
    public class Twitter
    {
        private string TwitterApiUrl { get; set; }
        private RestClient Client { get; set; }

        public Twitter()
        {
            TwitterApiUrl = "https://api.twitter.com";
            Client = new RestClient(TwitterApiUrl);
        }
        public void GetTimeLine(string key,string secret,string userToken,string userSecret)
        {
            var request = new RestRequest("/1.1/statuses/user_timeline.json", Method.GET);

            Client.Authenticator = OAuth1Authenticator.ForProtectedResource(key, secret, userToken, userSecret);

            var response = Client.Execute(request);


        }
        public void UpdateStatus(string status,string key,string secret,string token,string tokenSecret)
        {
            

            var request = new RestRequest("/1.1/statuses/update.json", Method.POST);

            request.AddQueryParameter("status", status);

            Client.Authenticator = OAuth1Authenticator.ForProtectedResource(key, secret, token, tokenSecret);

            var response = Client.Execute(request);


        }
        public string GetRequestToken(string key,string secret,string callBackUrl)
        {
            

            Client.Authenticator = OAuth1Authenticator.ForRequestToken(
                key,
                secret,
                callBackUrl 
            );

            var request = new RestRequest("/oauth/request_token", Method.POST);
            var response = Client.Execute(request);
           
            var qs = HttpUtility.ParseQueryString(response.Content);

            string oauthToken = qs["oauth_token"];
            string oauthTokenSecret = qs["oauth_token_secret"];

            request = new RestRequest("oauth/authorize?oauth_token=" + oauthToken);

            string url = Client.BuildUri(request).ToString();
            return url;

        }

        public string GetAccessToken(string key, string secret,string oauth_token,string oauth_token_secret,string oauth_verifier)
        {
            
            var request = new RestRequest("/oauth/access_token", Method.POST);

            //oauth_token_secret is unknown at this state of the application. Passing an empty string
            Client.Authenticator = OAuth1Authenticator.ForAccessToken(key, secret, oauth_token,oauth_token_secret, oauth_verifier);
            
            var response = Client.Execute(request);
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
