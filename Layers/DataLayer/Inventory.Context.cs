﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class InventoryEntities : DbContext
    {
        public InventoryEntities()
            : base("name=InventoryEntities")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public DbSet<category> categories { get; set; }
        public DbSet<dealer> dealers { get; set; }
        public DbSet<product> products { get; set; }
        public DbSet<selling_history> selling_history { get; set; }
        public DbSet<sub_category> sub_category { get; set; }
        public DbSet<transaction_type> transaction_type { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
