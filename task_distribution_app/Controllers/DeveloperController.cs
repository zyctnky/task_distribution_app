using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using task_distribution_app.DataAccess.Task;
using task_distribution_app.Helpers;
using task_distribution_app.Models.ViewModels;
using static task_distribution_app.Models.Enums;

namespace task_distribution_app.Controllers
{
    [RoutePrefix("developer")]
    public class DeveloperController : Controller
    {
        ITaskDataAccess _taskDA;
        public DeveloperController(ITaskDataAccess taskDA)
        {
            _taskDA = taskDA;
        }

        [Route("")]
        [SessionCheck(role = ROLES.DEVELOPER)]
        public ActionResult Index()
        {
            int user_id = (int)Session["USER_ID"];
            List<TaskVM> taskList = _taskDA.GetList(user_id).OrderByDescending(t => t.createdAt).ToList();
            return View(taskList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TaskCompletedForm(FormCollection form)
        {
            try
            {
                int taskId = int.Parse(form["task.id"]);
                TaskVM checkTask = _taskDA.GetById(taskId);
                checkTask.isComplete = true;
                checkTask.completeDate = DateTime.Now;
                _taskDA.Update(checkTask);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}