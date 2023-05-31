using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace task_distribution_app.Controllers
{
    public class AuthController : Controller
    {
        [Route("login")]
        public ActionResult Login()
        {
            return View();
        }
    }
}