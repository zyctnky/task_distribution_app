using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static task_distribution_app.Models.Enums;

namespace task_distribution_app.Controllers
{
    public class HomeController : Controller
    {
        [Route("~/")]
        public ActionResult Index()
        {
            int role_id = Session["USER_ROL_ID"] == null ? 0 : (int)Session["USER_ROL_ID"];

            if (role_id == (int)ROLES.YONETICI)
                return RedirectToAction("Index", "Manager");

            if (role_id == (int)ROLES.ANALIST)
                return RedirectToAction("Index", "Analyst");

            if (role_id == (int)ROLES.DEVELOPER)
                return RedirectToAction("Index", "Developer");

            return RedirectToAction("Login", "Auth");
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