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
    [RoutePrefix("analyst")]
    public class AnalystController : Controller
    {
        ITaskDataAccess _taskDA;
        public AnalystController(ITaskDataAccess taskDA)
        {
            _taskDA = taskDA;
        }

        [Route("")]
        [SessionCheck(role = ROLES.ANALIST)]
        public ActionResult Index()
        {
            List<TaskVM> taskList = _taskDA.GetList(0).OrderByDescending(t => t.createdAt).ToList();
            return View(taskList);
        }

        [Route("task/create")]
        [SessionCheck(role = ROLES.ANALIST)]
        public ActionResult TaskCreate()
        {
            PopulateDifficultyLevelDropdown();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TaskCreateForm(TaskVM task)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.error = "Tüm alanları doldurunuz.";
                    return View("Index");
                }
                else
                {
                    task.createdBy = (int)Session["USER_ID"];
                    _taskDA.Insert(task);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [Route("task/{id}/details")]
        [SessionCheck(role = ROLES.ANALIST)]
        public ActionResult TaskDetails(int id)
        {
            TaskVM task = _taskDA.GetById(id);
            return View(task);
        }

        [Route("task/{id}/edit")]
        [SessionCheck(role = ROLES.ANALIST)]
        public ActionResult TaskEdit(int id)
        {
            TaskVM task = _taskDA.GetById(id);
            PopulateDifficultyLevelDropdown(task.difficultylevelId);
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TaskEditForm(TaskVM task)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.error = "Tüm alanları doldurunuz.";
                    return View("Index");
                }
                else
                {
                    TaskVM checkTask = _taskDA.GetById(task.id);
                    checkTask.title = task.title;
                    checkTask.description = task.description;
                    checkTask.difficultylevelId = task.difficultylevelId;
                    _taskDA.Update(checkTask);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [Route("task/{id}/delete")]
        [SessionCheck(role = ROLES.ANALIST)]
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

        private void PopulateDifficultyLevelDropdown(object selected = null)
        {
            List<DifficultyLevelVM> difficultyLevels = _taskDA.GetDifficultyLevelList();
            difficultyLevels.Insert(0, new DifficultyLevelVM() { id = 0, name = "Zorluk Seviyesi Seçiniz", score = 0 });
            ViewBag.difficultylevelId = new SelectList(difficultyLevels, "id", "name", selected);
        }
    }
}