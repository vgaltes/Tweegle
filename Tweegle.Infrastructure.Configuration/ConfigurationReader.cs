using System;
using System.Configuration;

namespace Tweegle.Infrastructure.Configuration
{
    public class ConfigurationReader : IConfigurationReader
    {
        public string GetScreenName()
        {
            return ConfigurationManager.AppSettings["ScreenName"];
        }


        public string GetOAuthTokenSecret()
        {
            return ConfigurationManager.AppSettings["OAuthTokenSecret"];
        }

        public string GetOAuthToken()
        {
            return ConfigurationManager.AppSettings["OAuthToken"];
        }

        public string GetOAuthConsumerSecret()
        {
            return ConfigurationManager.AppSettings["OAuthConsumerSecret"];
        }

        public string GetOAuthConsumerKey()
        {
            return ConfigurationManager.AppSettings["OAuthConsumerKey"];
        }


        public string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["TweegleMongoDB"].ConnectionString;
        }
    }
}