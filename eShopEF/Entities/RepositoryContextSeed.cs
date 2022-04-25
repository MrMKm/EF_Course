using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class RepositoryContextSeed
    {
        public static async Task SeedAsync(RepositoryContext repositoryContext)
        {
            if(!repositoryContext.SubDepartment.Any())
            {
                var seedSubDepartments = new List<SubDepartment>
                {
                    new SubDepartment("Juices", 1),
                    new SubDepartment("Frutas", 1),
                    new SubDepartment("Verduras", 1),
                    new SubDepartment("Salas", 2),
                    new SubDepartment("TVs", 3),
                    new SubDepartment("Audio", 3),
                    new SubDepartment("Videojuegos", 3)
                };

                foreach(var subDepartment in seedSubDepartments)
                {
                    repositoryContext.Add(subDepartment);
                }

                repositoryContext.SaveChanges();
            }

            if (!repositoryContext.Product.Any())
            {
                var seedProducts = new List<Product>
                {
                    new Product("Orange juice", 10.99m, "1L", "LALA", "0000001", 100, 1),
                    new Product("Apple juice", 15.99m, "1L", "DelValle", "0000002", 100, 1),
                    new Product("Xbox Series X", 1000.99m, "Console", "Microsoft", "0000003", 100, 7),
                    new Product("Xbox Series S", 600.99m, "Console", "Microsoft", "0000004", 100, 7),
                    new Product("Samsung 70'' Crystal UHD", 1500.99m, "4K TV", "Samsung", "0000005", 100, 5)
                };

                foreach (var product in seedProducts)
                {
                    repositoryContext.Add(product);
                }

                repositoryContext.SaveChanges();
            }

            if (!repositoryContext.Department.Any())
            {
                var seedDepartments = new List<Department>
                {
                    new Department("Alimentos",
                        repositoryContext.SubDepartment.Where(p => p.DepartmentID.Equals(1)).ToList()),
                    new Department("Muebles",
                        repositoryContext.SubDepartment.Where(p => p.DepartmentID.Equals(2)).ToList()),
                    new Department("Electrónica",
                        repositoryContext.SubDepartment.Where(p => p.DepartmentID.Equals(3)).ToList())
                };

                foreach (var department in seedDepartments)
                {
                    repositoryContext.Add(department);
                }

                repositoryContext.SaveChanges();
            }

            if (!repositoryContext.Department.Any())
            {
                var seedDepartments = new List<Department>
                {
                    new Department("Alimentos",
                        repositoryContext.SubDepartment.Where(p => p.DepartmentID.Equals(1)).ToList()),
                    new Department("Muebles",
                        repositoryContext.SubDepartment.Where(p => p.DepartmentID.Equals(2)).ToList()),
                    new Department("Electrónica",
                        repositoryContext.SubDepartment.Where(p => p.DepartmentID.Equals(3)).ToList())
                };

                foreach (var department in seedDepartments)
                {
                    repositoryContext.Add(department);
                }

                repositoryContext.SaveChanges();
            }

            if (!repositoryContext.Provider.Any())
            {
                var seedProvider = new List<Provider>
                {
                    new Provider("Company 1", "Fake Street #935", "3314879491", "fake@fake.com", "Jalisco"),
                    new Provider("Company 2", "Fake Street #936", "6641234567", "fake@fake.com", "Tijuana"),
                    new Provider("Company 3", "Fake Street #937", "5511234567", "fake@fake.com", "CDMX"),
                    new Provider("Company 4", "Fake Street #938", "3561234567", "fake@fake.com", "Michoacan")
                };

                foreach (var provider in seedProvider)
                {
                    repositoryContext.Add(provider);
                }

                repositoryContext.SaveChanges();
            }
        }

        public static string AdminPassword = "password";
    }
}
