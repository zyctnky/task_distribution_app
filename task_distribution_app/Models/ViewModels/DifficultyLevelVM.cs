using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace task_distribution_app.Models.ViewModels
{
    public class DifficultyLevelVM
    {
        public int id { get; set; }
        public int? score { get; set; }
        public string name { get; set; }
    }
}