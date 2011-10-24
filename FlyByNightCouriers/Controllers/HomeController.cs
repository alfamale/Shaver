using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;
using System.Web.UI;

namespace FlyByNight.Controllers
{
    public class HomeController : Controller
    {
        public const string IndexMessage = "Fly by Night Couriers";

        public ActionResult Index()
        {
            ViewBag.Message = IndexMessage;
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

    }
}
