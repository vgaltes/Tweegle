using Tweegle.Infrastructure.Configuration;

namespace Tweegle.Infrastructure.Twitter
{
    public class Client
    {
        private readonly IConfigurationReader configuration;

        public void ImportAllFavorites()
        {
            string screenName = configuration.GetScreenName();


        }
    }
}