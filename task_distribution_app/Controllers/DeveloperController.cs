using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace task_distribution_app.Controllers
{
    [RoutePrefix("developer")]
    public class DeveloperController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }
    }
}