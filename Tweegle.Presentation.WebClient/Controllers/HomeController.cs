using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tweegle.Infrastructure.Twitter;

namespace Tweegle.Presentation.WebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITwitterClient client;

        public HomeController(ITwitterClient client)
        {
            this.client = client;
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
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