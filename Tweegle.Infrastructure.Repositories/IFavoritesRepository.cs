using System.Collections.Generic;
using Tweegle.Infrastructure.DTOs;

namespace Tweegle.Infrastructure.Repositories
{
    public interface IFavoritesRepository
    {
        void Insert(List<TwitterFavorite> favorites);

        void Empty();
    }
}
