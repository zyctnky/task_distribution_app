//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace task_distribution_app.Models.DTO
{
    using System;
    using System.Collections.Generic;
    
    public partial class TTASK
    {
        public int TASK_ID { get; set; }
        public string TASK_TITLE { get; set; }
        public string TASK_DESCRIPTION { get; set; }
        public Nullable<int> TASK_DIFFICULTYLEVEL_ID { get; set; }
        public Nullable<int> TASK_ASSIGNED_USER_ID { get; set; }
        public Nullable<System.DateTime> TASK_ASSIGNED_DATE { get; set; }
        public Nullable<bool> TASK_IS_COMPLETE { get; set; }
        public Nullable<System.DateTime> TASK_COMPLETE_DATE { get; set; }
        public Nullable<System.DateTime> TASK_CREATED_AT { get; set; }
        public Nullable<int> TASK_CREATED_BY { get; set; }
    
        public virtual TDIFFICULTYLEVEL TDIFFICULTYLEVEL { get; set; }
        public virtual TUSER TUSER { get; set; }
        public virtual TUSER TUSER1 { get; set; }
    }
}
