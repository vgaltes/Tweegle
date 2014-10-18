using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tweegle.Infrastructure.Repositories;
using Tweegle.Infrastructure.Twitter;
using Tweegle.Presentation.WebClient.Models;

namespace Tweegle.Presentation.WebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITwitterClient client;
        private readonly IFavoritesRepository favoritesRepository;

        public HomeController(ITwitterClient client, IFavoritesRepository favoritesRepository)
        {
            this.client = client;
            this.favoritesRepository = favoritesRepository;
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(string searchTerm)
        {
            var favorites = favoritesRepository.FindByText(searchTerm);

            return PartialView(new SearchResultViewModel(favorites));
        }

        public ActionResult ImportAll()
        {
            client.ImportAllFavorites();
            return View();
        }

        public ActionResult ImportRecent()
        {
            return View();
        }
    }
}