using Entities.Configurations;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Entities
{
    public class RepositoryContext : DbContext
    {
        DbSet<Course> Course { get; set; }
        DbSet<Student> Student { get; set; }
        DbSet<Enrollment> Enrollment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentConfig());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CollegeApp;Integrated Security=True";

            options.UseSqlServer(connection);
        }
    }
}
