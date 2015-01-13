using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using TechRecycleManager.Models;

namespace TechRecycleManager.DAL
{
    public class TicketsContext : DbContext
    {
        public TicketsContext() : base("TicketsContext") { }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<BulkTicketDetails> BulkTicketDetails { get; set; }
        public DbSet<HBITicketDetails> HBITicketDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}