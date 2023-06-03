using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace task_distribution_app.Models.ViewModels
{
    public class TaskVM
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int? difficultylevelId { get; set; }
        public string difficultylevelName { get; set; }
        public int? assignedUserId { get; set; }
        public string assignedUserFullname { get; set; }
        public DateTime? assignedDate { get; set; }
        public bool? isComplete { get; set; }
        public DateTime? completeDate { get; set; }
        public DateTime? createdAt { get; set; }
        public int? createdBy { get; set; }
        public string createdUserFullname { get; set; }
    }
}