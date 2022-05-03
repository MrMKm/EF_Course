using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace Entities
{
    public class RepositoryContext : IdentityDbContext<ApplicationUser>
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options) { }

        public DbSet<AdminOrderProducts> AdminOrderProduct { get; set; }
        public DbSet<CustomerOrder> CustomerOrder { get; set; }
        public DbSet<CustomerOrderProduct> CustomerOrderProduct { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Provider> Provider { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrder { get; set; }
        public DbSet<SubDepartment> SubDepartment { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    var connection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=eShopV2;Integrated Security=True";

        //    options.UseSqlServer(connection);
        //}
    }
}
