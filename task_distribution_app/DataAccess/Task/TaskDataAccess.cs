﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using task_distribution_app.Models.DTO;
using task_distribution_app.Models.ViewModels;
using task_distribution_app.Repository;

namespace task_distribution_app.DataAccess.Task
{
    public class TaskDataAccess : ITaskDataAccess
    {
        IGenericRepository<TTASK> _taskRepo;
        IGenericRepository<TDIFFICULTYLEVEL> _difficultyLevelRepo;
        TaskDistributionEntities _context;

        public List<TaskVM> GetList(int assigned_user_id = 0)
        {
            using (_context = new TaskDistributionEntities())
            {
                _taskRepo = new GenericRepository<TTASK>(_context);

                List<TTASK> taskList = assigned_user_id <= 0
                    ? _taskRepo.Select().ToList()
                    : _taskRepo.Select(t => t.TASK_ASSIGNED_USER_ID == assigned_user_id).ToList();

                List<TaskVM> taskVmList = new List<TaskVM>();
                foreach (TTASK task in taskList)
                {
                    taskVmList.Add(Map_TaskToTaskVm(task));
                }

                return taskVmList;
            }
        }

        public TaskVM GetById(int task_id)
        {
            using (_context = new TaskDistributionEntities())
            {
                _taskRepo = new GenericRepository<TTASK>(_context);

                TTASK task = _taskRepo.FindById(task_id);
                TaskVM taskVm = new TaskVM();
                taskVm = Map_TaskToTaskVm(task);

                return taskVm;
            }
        }

        public bool Insert(TaskVM taskVM)
        {
            using (_context = new TaskDistributionEntities())
            {
                _taskRepo = new GenericRepository<TTASK>(_context);

                object[] parameters = { taskVM.difficultylevelId };
                int availableDeveloperId = _context.Database.SqlQuery<int>("SELECT DBO.F_FIND_AVAILABLE_DEVELOPER({0}) AS USER_ID", parameters).FirstOrDefault();
                taskVM.assignedUserId = availableDeveloperId;
                taskVM.assignedDate = new DateTime();
                _taskRepo.Insert(Map_TaskVMToTask(taskVM));
                _taskRepo.Save();

                return true;
            }
        }

        public bool Update(TaskVM taskVM)
        {
            using (_context = new TaskDistributionEntities())
            {
                _taskRepo = new GenericRepository<TTASK>(_context);
                _taskRepo.Update(Map_TaskVMToTask(taskVM));
                _taskRepo.Save();

                return true;
            }
        }

        public bool Delete(TaskVM taskVM)
        {
            using (_context = new TaskDistributionEntities())
            {
                _taskRepo = new GenericRepository<TTASK>(_context);
                _taskRepo.Delete(taskVM.id);
                _taskRepo.Save();
                return true;
            }
        }

        public List<DifficultyLevelVM> GetDifficultyLevelList()
        {
            using (_context = new TaskDistributionEntities())
            {
                _difficultyLevelRepo = new GenericRepository<TDIFFICULTYLEVEL>(_context);

                List<TDIFFICULTYLEVEL> difficultyLevelList = _difficultyLevelRepo.Select().ToList();
                List<DifficultyLevelVM> difficultyLevelVmList = new List<DifficultyLevelVM>();
                foreach (TDIFFICULTYLEVEL difficultyLevel in difficultyLevelList)
                {
                    difficultyLevelVmList.Add(new DifficultyLevelVM()
                    {
                        id = difficultyLevel.DIFFICULTYLEVEL_ID,
                        name = difficultyLevel.DIFFICULTYLEVEL_NAME,
                        score = difficultyLevel.DIFFICULTYLEVEL_SCORE
                    });
                }

                return difficultyLevelVmList;
            }
        }

        private TTASK Map_TaskVMToTask(TaskVM taskVM)
        {
            return new TTASK()
            {
                TASK_ID = taskVM.id,
                TASK_TITLE = taskVM.title,
                TASK_DESCRIPTION = taskVM.description,
                TASK_CREATED_AT = DateTime.Now,
                TASK_CREATED_BY = taskVM.createdBy,
                TASK_DIFFICULTYLEVEL_ID = taskVM.difficultylevelId,
                TASK_IS_COMPLETE = taskVM.isComplete,
                TASK_COMPLETE_DATE = taskVM.completeDate,
                TASK_ASSIGNED_DATE = DateTime.Now,
                TASK_ASSIGNED_USER_ID = taskVM.assignedUserId,
            };
        }

        private TaskVM Map_TaskToTaskVm(TTASK task)
        {
            return new TaskVM()
            {
                id = task.TASK_ID,
                assignedDate = task.TASK_ASSIGNED_DATE,
                assignedUserFullname = task.TUSER.USER_FULLNAME,
                assignedUserId = task.TASK_ASSIGNED_USER_ID,
                completeDate = task.TASK_COMPLETE_DATE,
                createdAt = task.TASK_CREATED_AT,
                createdBy = task.TASK_CREATED_BY,
                createdUserFullname = task.TUSER1.USER_FULLNAME,
                description = task.TASK_DESCRIPTION,
                difficultylevelId = task.TASK_DIFFICULTYLEVEL_ID,
                difficultylevelName = task.TDIFFICULTYLEVEL.DIFFICULTYLEVEL_NAME,
                isComplete = task.TASK_IS_COMPLETE,
                title = task.TASK_TITLE
            };
        }

    }
}