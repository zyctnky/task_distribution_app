using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace task_distribution_app.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _Navbar()
        {
            ViewBag.fullname = Session["USER_FULLNAME"];
            ViewBag.rol_name = Session["USER_ROL_NAME"] == null ? "" : Session["USER_ROL_NAME"];
            return View();
        }

        public ActionResult _PageHeader()
        {
            return View();
        }
    }
}