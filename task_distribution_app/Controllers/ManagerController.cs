using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using task_distribution_app.Helpers;
using static task_distribution_app.Models.Enums;

namespace task_distribution_app.Controllers
{
    [RoutePrefix("manager")]
    public class ManagerController : Controller
    {
        [Route("")]
        [SessionCheck(role = ROLES.YONETICI)]
        public ActionResult Index()
        {
            return View();
        }

        [Route("task/{id}/details")]
        [SessionCheck(role = ROLES.YONETICI)]
        public ActionResult TaskDetails(int id)
        {
            return View();
        }

        [Route("task/{id}/edit")]
        [SessionCheck(role = ROLES.YONETICI)]
        public ActionResult TaskEdit(int id)
        {
            return View();
        }

        [Route("task/{id}/delete")]
        [SessionCheck(role = ROLES.YONETICI)]
        public ActionResult TaskDelete(int id)
        {
            return View(); 
        }

        [Route("developer")]
        [SessionCheck(role = ROLES.YONETICI)]
        public ActionResult DeveloperList()
        {
            return View();
        }

        [Route("developer/{id}/details")]
        [SessionCheck(role = ROLES.YONETICI)]
        public ActionResult DeveloperDetails(int id)
        {
            return View();
        }
    }
}