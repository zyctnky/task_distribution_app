using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace task_distribution_app.Models.ViewModels
{
    public class DeveloperVM
    {
        public UserVM user { get; set; }
        public List<TaskVM> taskList { get; set; }
    }
}