﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TechRecycleClient.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TechRecycleEntities : DbContext
    {
        public TechRecycleEntities()
            : base("name=TechRecycleEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BulkTicketDetail> BulkTicketDetails { get; set; }
        public virtual DbSet<HBITicketDetail> HBITicketDetails { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
    }
}
