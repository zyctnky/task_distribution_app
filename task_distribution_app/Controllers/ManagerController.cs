using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace task_distribution_app.Controllers
{
    [RoutePrefix("manager")]
    public class ManagerController : Controller
    {
        [Route("")]
        public ActionResult Index()
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

        [Route("developer")]
        public ActionResult DeveloperList()
        {
            return View();
        }

        [Route("developer/{id}/details")]
        public ActionResult DeveloperDetails(int id)
        {
            return View();
        }
    }
}