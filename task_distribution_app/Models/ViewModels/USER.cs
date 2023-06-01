using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace task_distribution_app.Models.ViewModels
{
    public class USER
    {
        public int id { get; set; }
        public string username { get; set; }
        public string fullname { get; set; }
        public string password { get; set; }
        public int? rol_id { get; set; }
        public string rol_name { get; set; }
    }
}