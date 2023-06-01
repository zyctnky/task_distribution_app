using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using task_distribution_app.Helpers;
using static task_distribution_app.Models.Enums;

namespace task_distribution_app.Controllers
{
    [RoutePrefix("developer")]
    public class DeveloperController : Controller
    {
        [Route("")]
        [SessionCheck(role = ROLES.DEVELOPER)]
        public ActionResult Index()
        {
            return View();
        }
    }
}