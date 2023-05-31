using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace task_distribution_app.Controllers
{
    [RoutePrefix("analyst")]
    public class AnalystController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("task/create")]
        public ActionResult TaskCreate()
        {
            return View();
        }

        [Route("task/{id}/details")]
        public ActionResult TaskDetails(int id)
        {
            return View();
        }

        [Route("task/{id}/edit")]
        public ActionResult TaskEdit(int id)
        {
            return View();
        }

        [Route("task/{id}/delete")]
        public ActionResult TaskDelete(int id)
        {
            return View();
        }
    }
}