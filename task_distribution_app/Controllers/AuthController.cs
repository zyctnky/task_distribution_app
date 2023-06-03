using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using task_distribution_app.DataAccess.User;
using task_distribution_app.Models.ViewModels;

namespace task_distribution_app.Controllers
{
    public class AuthController : Controller
    {
        IUserDataAccess _userDA;

        public AuthController(IUserDataAccess userDA)
        {
            _userDA = userDA;
        }

        [Route("login")]
        public ActionResult Login()
        {
            return View(new UserVM());
        }

        [HttpPost]
        public ActionResult LoginForm(UserVM user)
        {
            UserVM checkUser = _userDA.Login(user.username, user.password);
            if (checkUser != null)
            {
                Session.Add("USER_ID", checkUser.id);
                Session.Add("USER_FULLNAME", checkUser.fullname);
                Session.Add("USER_ROL_ID", checkUser.roleId);
                Session.Add("USER_ROL_NAME", checkUser.roleName);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Kullanıcı adı/şifre hatalı.";
            return View("Login");
        }

        [Route("logout")]
        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Login");
        }
    }
}