using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using static task_distribution_app.Models.Enums;

namespace task_distribution_app.Helpers
{
    public class SessionCheck : ActionFilterAttribute
    {
        public ROLES role { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            int user_id = session["USER_ID"] == null ? 0 : (int)session["USER_ID"];
            int role_id = session["USER_ROL_ID"] == null ? 0 : (int)session["USER_ROL_ID"];
            if (user_id <= 0)
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Auth" }, { "Action", "Login" } });

            if (role_id <= 0 || role_id != (int)role)
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Auth" }, { "Action", "Login" } });
        }
    }
}