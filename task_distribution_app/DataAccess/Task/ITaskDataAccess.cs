using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using task_distribution_app.Models.ViewModels;

namespace task_distribution_app.DataAccess.Task
{
    public interface ITaskDataAccess
    {
        List<TaskVM> GetList();
        TaskVM GetById(int task_id);
        bool Insert(TaskVM task);
        bool Update(TaskVM task);
        bool Delete(TaskVM task);
        List<DifficultyLevelVM> GetDifficultyLevelList();
    }
}
