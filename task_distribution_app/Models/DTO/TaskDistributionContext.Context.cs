﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TaskDistributionEntities : DbContext
    {
        public TaskDistributionEntities()
            : base("name=TaskDistributionEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TDIFFICULTYLEVEL> TDIFFICULTYLEVEL { get; set; }
        public virtual DbSet<TROLE> TROLE { get; set; }
        public virtual DbSet<TTASK> TTASK { get; set; }
        public virtual DbSet<TUSER> TUSER { get; set; }
    }
}
