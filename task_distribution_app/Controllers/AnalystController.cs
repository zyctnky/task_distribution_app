using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using task_distribution_app.Helpers;
using static task_distribution_app.Models.Enums;

namespace task_distribution_app.Controllers
{
    [RoutePrefix("analyst")]
    public class AnalystController : Controller
    {
        [Route("")]
        [SessionCheck(role = ROLES.ANALIST)]
        public ActionResult Index()
        {
            return View();
        }

        [Route("task/create")]
        [SessionCheck(role = ROLES.ANALIST)]
        public ActionResult TaskCreate()
        {
            return View();
        }

        [Route("task/{id}/details")]
        [SessionCheck(role = ROLES.ANALIST)]
        public ActionResult TaskDetails(int id)
        {
            return View();
        }

        [Route("task/{id}/edit")]
        [SessionCheck(role = ROLES.ANALIST)]
        public ActionResult TaskEdit(int id)
        {
            return View();
        }

        [Route("task/{id}/delete")]
        [SessionCheck(role = ROLES.ANALIST)]
        public ActionResult TaskDelete(int id)
        {
            return View();
        }
    }
}