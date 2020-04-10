using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Acerca de Ottone Design.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contáctenos.";

            return View();
        }
    }
}