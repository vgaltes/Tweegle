using System;
using System.Collections.Generic;
using System.Linq;
using LinqToTwitter;
using Tweegle.Infrastructure.Configuration;
using Tweegle.Infrastructure.DTOs;
using Tweegle.Infrastructure.Repositories;

namespace Tweegle.Infrastructure.Twitter
{
    public class TwitterClient : ITwitterClient
    {
        private readonly IConfigurationReader configuration;

        private readonly IFavoritesRepository favoritesRepository;

        public TwitterClient(IConfigurationReader configuration, IFavoritesRepository favoritesRepository)
        {
            this.configuration = configuration;
            this.favoritesRepository = favoritesRepository;
        }

        public void ImportAllFavorites()
        {
            favoritesRepository.Empty();

            string screenName = configuration.GetScreenName();

            var auth = new SingleUserAuthorizer
            {
                CredentialStore = new SingleUserInMemoryCredentialStore
                {
                    ConsumerKey = configuration.GetOAuthConsumerKey(),
                    ConsumerSecret = configuration.GetOAuthConsumerSecret(),
                    AccessToken = configuration.GetOAuthToken(),
                    AccessTokenSecret = configuration.GetOAuthTokenSecret()
                }
            };

            var twitterCtx = new TwitterContext(auth);

            List<TwitterFavorite> favorites = new List<TwitterFavorite>();

            var favsResponse = twitterCtx.Favorites
                .Where(fav => fav.Type == FavoritesType.Favorites && fav.ScreenName == screenName)
                .Select(f => new TwitterFavorite(f.StatusID, f.Text, f.User.Name, f.User.ScreenNameResponse, f.CreatedAt,
                        f.Entities.HashTagEntities.Select(h => h.Tag).ToList()))
                .ToList();

            favorites.AddRange(favsResponse);

            while (favsResponse.Count >= 19)
            {
                System.Threading.Thread.Sleep(TimeSpan.FromMinutes(1));
                favsResponse = twitterCtx.Favorites
                    .Where(fav => fav.Type == FavoritesType.Favorites && fav.ScreenName == screenName && fav.MaxID == favsResponse.Last().Id)
                    .Select(f => new TwitterFavorite(f.StatusID, f.Text, f.User.Name, f.User.ScreenNameResponse, f.CreatedAt,
                            f.Entities.HashTagEntities.Select(h => h.Tag).ToList()))
                    .ToList();

                if (favsResponse.Any())
                {
                    favsResponse.RemoveAt(0);

                    favorites.AddRange(favsResponse);
                }
            }

            favoritesRepository.Insert(favorites);
        }
    }
}