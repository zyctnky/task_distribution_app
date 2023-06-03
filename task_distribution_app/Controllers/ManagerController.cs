using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using task_distribution_app.DataAccess.Task;
using task_distribution_app.DataAccess.User;
using task_distribution_app.Helpers;
using task_distribution_app.Models.ViewModels;
using static task_distribution_app.Models.Enums;

namespace task_distribution_app.Controllers
{
    [RoutePrefix("manager")]
    public class ManagerController : Controller
    {
        ITaskDataAccess _taskDA;
        IUserDataAccess _userDA;
        public ManagerController(ITaskDataAccess taskDA, IUserDataAccess userDA)
        {
            _taskDA = taskDA;
            _userDA = userDA;
        }

        [Route("")]
        [SessionCheck(role = ROLES.YONETICI)]
        public ActionResult Index()
        {
            List<TaskVM> taskList = _taskDA.GetList(0).OrderByDescending(t => t.createdAt).ToList();
            return View(taskList);
        }

        [Route("task/{id}/details")]
        [SessionCheck(role = ROLES.YONETICI)]
        public ActionResult TaskDetails(int id)
        {
            TaskVM task = _taskDA.GetById(id);
            return View(task);
        }

        [Route("task/{id}/edit")]
        [SessionCheck(role = ROLES.YONETICI)]
        public ActionResult TaskEdit(int id)
        {
            TaskVM task = _taskDA.GetById(id);
            PopulateDeveloperDropdown(task.assignedUserId);
            return View(task);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TaskEditForm(TaskVM task)
        {
            try
            {
                TaskVM checkTask = _taskDA.GetById(task.id);
                checkTask.assignedUserId = task.assignedUserId;
                checkTask.assignedDate = new DateTime();
                _taskDA.Update(checkTask);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [Route("task/{id}/delete")]
        [SessionCheck(role = ROLES.YONETICI)]
        public ActionResult TaskDelete(int id)
        {
            TaskVM task = _taskDA.GetById(id);
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TaskDeleteForm(TaskVM task)
        {
            try
            {
                _taskDA.Delete(task);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [Route("developer")]
        [SessionCheck(role = ROLES.YONETICI)]
        public ActionResult DeveloperList()
        {
            List<DeveloperVM> developerList = _userDA.GetDeveloperList();
            return View(developerList);
        }

        [Route("developer/{id}/details")]
        [SessionCheck(role = ROLES.YONETICI)]
        public ActionResult DeveloperDetails(int id)
        {
            DeveloperVM developer = _userDA.GetDeveloperById(id);
            return View(developer);
        }

        private void PopulateDeveloperDropdown(object selected = null)
        {
            List<UserVM> userList = _userDA.GetList((int)ROLES.DEVELOPER);
            userList.Insert(0, new UserVM() { id = 0, fullname = "Developer Seçiniz" });
            ViewBag.assignedUserId = new SelectList(userList, "id", "fullname", selected);
        }
    }
}