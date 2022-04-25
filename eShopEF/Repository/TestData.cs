﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public static class TestData
    {
        public static List<SubDepartment> subDepartmentsList = new List<SubDepartment>() 
        {
            new SubDepartment("Juices", 1),
            new SubDepartment("Frutas", 1),
            new SubDepartment("Verduras", 1),
            new SubDepartment("Salas", 2),
            new SubDepartment("TVs", 3),
            new SubDepartment("Audio", 3),
            new SubDepartment("Videojuegos", 3)

        };

        public static List<Product> ProductsList = new List<Product>()
        {
            new Product("Orange juice", 10.99m, "1L", "LALA", "0000001", 100, 1),
            new Product("Apple juice", 15.99m, "1L", "DelValle", "0000002", 100, 1),
            new Product("Xbox Series X", 1000.99m, "Console", "Microsoft", "0000003", 100, 7),
            new Product("Xbox Series S", 600.99m, "Console", "Microsoft", "0000004", 100, 7),
            new Product("Samsung 70'' Crystal UHD", 1500.99m, "4K TV", "Samsung", "0000005", 100, 5)
        };

        public static List<Department> DepartmentsList = new List<Department>()
        {
            new Department("Alimentos",
                subDepartmentsList.Where(p => p.DepartmentID.Equals(1)).ToList()),
            new Department("Muebles",
                subDepartmentsList.Where(p => p.DepartmentID.Equals(2)).ToList()),
            new Department("Electrónica", 
                subDepartmentsList.Where(p => p.DepartmentID.Equals(3)).ToList())
        };

        public static List<Provider> ProvidersList = new List<Provider>()
        {
            new Provider("Company 1", "Fake Street #935", "3314879491", "fake@fake.com", "Jalisco"),
            new Provider("Company 2", "Fake Street #936", "6641234567", "fake@fake.com", "Tijuana"),
            new Provider("Company 3", "Fake Street #937", "5511234567", "fake@fake.com", "CDMX"),
            new Provider("Company 4", "Fake Street #938", "3561234567", "fake@fake.com", "Michoacan")
        };

        public static string AdminPassword = "password";
    }
}
