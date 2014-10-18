using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Tweegle.Infrastructure.Configuration;
using Tweegle.Infrastructure.DTOs;
using System.Linq;
using System.Text.RegularExpressions;

namespace Tweegle.Infrastructure.Repositories
{
    public class FavoritesRepository : IFavoritesRepository
    {
        MongoCollection collection;
        private readonly IConfigurationReader configuration;

        public FavoritesRepository(IConfigurationReader configuration)
        {
            this.configuration = configuration;
        }

        private void ConnectToDb()
        {
            var connectionString = configuration.GetConnectionString();
            collection = new MongoClient(connectionString)
                .GetServer()
                .GetDatabase("tweegle")
                .GetCollection("favorites");
        }

        public void Insert(List<TwitterFavorite> favorites)
        {
            ConnectToDb();
            var bulkOperation = collection.InitializeOrderedBulkOperation();
            foreach (var favorite in favorites)
                bulkOperation.Insert<TwitterFavorite>(favorite);
            bulkOperation.Execute();
        }

        public void Empty()
        {
            ConnectToDb();
            collection.RemoveAll();
        }


        public List<TwitterFavorite> FindByText(string searchTerm)
        {
            ConnectToDb();
            
            var query = Query<TwitterFavorite>.Matches( fav => fav.Text, 
                new BsonRegularExpression(string.Format("/{0}/i", searchTerm)));

            var results = collection.FindAs<TwitterFavorite>(query)
                .OrderByDescending(fav => fav.Id);

            return results.ToList();
        }
    }
}
