using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Data
{
    public class RepositoryContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Blogging;Integrated Security=True";

            options.UseSqlServer(connection);
        }
    }
}
