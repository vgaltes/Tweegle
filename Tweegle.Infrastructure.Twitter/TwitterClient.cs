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
        private readonly IConfigurationReader _configuration;

        private readonly IFavoritesRepository _favoritesRepository;

        public TwitterClient(IConfigurationReader configuration, IFavoritesRepository favoritesRepository)
        {
            _configuration = configuration;
            _favoritesRepository = favoritesRepository;
        }

        public void ImportAllFavorites()
        {
            _favoritesRepository.Empty();

            string screenName = _configuration.GetScreenName();

            var auth = new SingleUserAuthorizer
            {
                CredentialStore = new SingleUserInMemoryCredentialStore
                {
                    ConsumerKey = _configuration.GetOAuthConsumerKey(),
                    ConsumerSecret = _configuration.GetOAuthConsumerSecret(),
                    AccessToken = _configuration.GetOAuthToken(),
                    AccessTokenSecret = _configuration.GetOAuthTokenSecret()
                }
            };

            var twitterCtx = new TwitterContext(auth);

            var favorites = new List<TwitterFavorite>();

            var favsResponse = twitterCtx.Favorites
                .Where(fav => fav.Type == FavoritesType.Favorites && fav.ScreenName == screenName)
                .Select(f => new TwitterFavorite(f.StatusID, f.Text, f.User.Name, f.User.ScreenNameResponse, f.CreatedAt,
                        f.Entities.HashTagEntities.Select(h => h.Tag).ToList()))
                .ToList();

            favorites.AddRange(favsResponse);

            while (favsResponse.Count > 0)
            {
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(61));
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

            _favoritesRepository.Insert(favorites);
        }
    }
}