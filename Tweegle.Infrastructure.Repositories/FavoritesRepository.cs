using System.Collections.Generic;
using MongoDB.Driver;
using Tweegle.Infrastructure.Configuration;
using Tweegle.Infrastructure.DTOs;

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
    }
}
