using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TokenManager.Filters;

namespace TokenManager.Controllers
{
    public class HomeController : Controller
    {
        [TokenValidate]
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
