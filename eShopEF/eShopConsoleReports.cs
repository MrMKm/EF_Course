using Entities.Models;
using Repository;
using Repository.Implementations;
using Shared;
using Shared.DataTransferObjects;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopEF
{
    public partial class eShopConsole
    {
        public static void Top5MoreExpensive()
        {
            var products = _productRepository.GetProducts()
                .OrderByDescending(p => p.Price)
                .Take(5)
                .ToList();

            foreach (var product in products)
                Console.WriteLine(product.ToString());
        }

        public static void FiveOrLessStock()
        {
            var products = _productRepository.GetProducts()
                .Where(p => p.Stock <= 5)
                .OrderBy(p => p.Stock)
                .ToList();

            foreach (var product in products)
                Console.WriteLine(product.ToString());
        }

        public static void ProductsByBrand()
        {
            var group = _productRepository.GetProducts()
                .OrderBy(p => p.Name[0])
                .GroupBy(p => p.Brand)
                .ToList();

            var dtoList = new[] 
            { new
                {
                    Brand = "",
                    Product = ""
                }
            }.ToList();
            dtoList.Clear();

            foreach (var brand in group)
            {
                var products = brand.ToList();

                foreach (var product in products)
                {
                    var dto = new
                    {
                        Brand = brand.Key,
                        Product = product.Name
                    };

                    dtoList.Add(dto);
                }
            }

            foreach (var dto in dtoList)
                Console.WriteLine($"Brand: {dto.Brand} \n" +
                    $"Product: {dto.Product} \n\n");
        }

        public static void DeparmentsAndSubDepartments()
        {
            var groups = _productRepository.GetProducts()
                .GroupBy(p => p.subDepartmentID)
                .ToList();


            var dtoList = new[] 
            { new
                {
                    ProductName = "",
                    SubDepartmentName = "",
                    DepartmentName = ""
                }
            }.ToList();
            dtoList.Clear();

            foreach (var group in groups)
            {
                var sub = _subDepartmentRepository.GetSubDepartmentByID(group.Key);
                var dep = _departmentRepository.GetDepartmentByID(sub.DepartmentID);
                var products = group.ToList();

                foreach (var product in products)
                {
                    var dto = new
                    {
                        ProductName = product.Name,
                        SubDepartmentName = sub.Name,
                        DepartmentName = dep.Name
                    };

                    dtoList.Add(dto);
                }
            }

            foreach (var dto in dtoList)
                Console.WriteLine($"Department: {dto.DepartmentName} \n" +
                    $"Sub Department: {dto.SubDepartmentName} \n" +
                    $"Product: {dto.ProductName} \n\n");
        }

        public static void LastPaidPurchases()
        {
            var orders = _productOrderRepository.GetPurchaseOrders()
                .Where(o => o.Date.Date >= DateTime.Now.Date.AddDays(-7) && o.Status.Equals(OrderStatus.Paid))
                .ToList();

            foreach (var order in orders)
            {
                Console.WriteLine(order.ToString());
                foreach (var product in order.PurchasedProducts)
                    Console.WriteLine(product.ToString());
                Console.WriteLine("**********************************");
            }
        }

        public static void XboxPurchases()
        {
            var groups = _productOrderRepository.GetPurchaseOrders()
                .GroupBy(o => o.PurchasedProducts.FindAll(p => p.Name.Contains("Xbox")))
                .ToList();

            foreach (var group in groups)
            {
                foreach (var product in group.Key)
                    Console.WriteLine(product.ToString());
            }
        }

        public static void PendingPurcharses()
        {
            var orders = _productOrderRepository.GetPurchaseOrders()
                .Where(o => o.Status.Equals(OrderStatus.Pending) && o.ProviderID.Equals(1))
                .ToList();

            foreach (var order in orders)
            {
                Console.WriteLine(order.ToString());
                foreach (var product in order.PurchasedProducts)
                    Console.WriteLine(product.ToString());
                Console.WriteLine("**********************************");
            }
        }

        public static void MostPurchasedProduct()
        {
            Nullable<int> productID = _productOrderRepository.GetPurchaseOrders()
                .SelectMany(o => o.PurchasedProducts)
                .GroupBy(p => p.ID)
                .Select(g => new
                {
                    ProductID = g.Key,
                    Sum = g.Sum(p => p.Stock)
                })
                .OrderByDescending(g => g.Sum)
                .FirstOrDefault().ProductID;

            var product = _productOrderRepository.GetPurchaseOrders()
                .GroupBy(o => o.PurchasedProducts.FirstOrDefault(p => p.ID.Equals((int)productID)))
                .FirstOrDefault();

            if (product.Key == null)
                throw new ApplicationException("Product with ID not found");

            Console.WriteLine(product.Key.ToString());
        }

        public static void ReportsMenu()
        {
            bool exit = false;

            do
            {
                Console.Clear();
                Console.WriteLine("\t\t Reports \n\n" +
                    "1. Top 5 expensive products order by higher price \n" +
                    "2. Products with 5 or less stock and ordered by stock \n" +
                    "3. Products names by brand ordered by name \n" +
                    "4. Purcharse orders with status 'Paid' in last 7 days \n" +
                    "5. Purcharse orders where bought Xbox \n" +
                    "6. Purcharse orders with status 'Pending' to Company 1 provider \n" +
                    "7. Product with more purcharse orders \n" +
                    "8. Exit \n\n");
                Console.WriteLine("Choose an option");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        Top5MoreExpensive();
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadLine();
                        break;

                    case "2":
                        Console.Clear();
                        FiveOrLessStock();
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadLine();
                        break;

                    case "3":
                        Console.Clear();
                        ProductsByBrand();
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadLine();
                        break;

                    case "4":
                        Console.Clear();
                        LastPaidPurchases();
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadLine();
                        break;

                    case "5":
                        Console.Clear();
                        XboxPurchases();
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadLine();
                        break;

                    case "6":
                        Console.Clear();
                        PendingPurcharses();
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadLine();
                        break;

                    case "7":
                        Console.Clear();
                        MostPurchasedProduct();
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadLine();
                        break;

                    case "8":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid option, try again...");
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadLine();
                        break;
                }

            } while (!exit);
        }
    }
}
