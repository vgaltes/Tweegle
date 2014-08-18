using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tweegle.Presentation.WebClient.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ImportAll()
        {

            return View();
        }

        public ActionResult ImportRecent()
        {
            return View();
        }
    }
}