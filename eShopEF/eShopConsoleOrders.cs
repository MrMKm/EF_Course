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
        public static void PurchaseOrder()
        {
            var purchasedProducts = new List<ProductDto>();

            Console.Clear();
            Console.WriteLine($"\t\t Provider List \n\n");
            foreach(var provider in _providerRepository.GetProviders())
            {
                Console.WriteLine(provider.ToString());
                Console.WriteLine();
            }
            Console.WriteLine("Provider ID:");
            if (!int.TryParse(Console.ReadLine(), out int ProviderID))
                throw new ApplicationException("Invalid ID format");

            var dbProvider = _providerRepository.GetProviderByID(ProviderID);

            if (dbProvider == null)
                throw new ApplicationException("Provider with ID not found");

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"\t\t Product List \n\n");
                foreach (var product in _productRepository.GetProducts())
                {
                    Console.WriteLine(product.ToString());
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine("Product ID:");
                if (!int.TryParse(Console.ReadLine(), out int ProductID))
                    throw new InvalidCastException("Invalid ID format");

                var dbProduct = _productRepository.GetProductByID(ProductID);

                if (dbProduct == null)
                    throw new ApplicationException("Product with ID not found");

                var productDto = new ProductDto 
                {
                    ID = dbProduct.ID,
                    Name = dbProduct.Name,
                    Brand = dbProduct.Brand,
                    Description = dbProduct.Description,
                    Price = dbProduct.Price,
                    Stock = dbProduct.Stock,
                    SKU = dbProduct.SKU,
                };

                Console.WriteLine();
                Console.WriteLine("Quantity: ");
                if (!int.TryParse(Console.ReadLine(), out int Quantity))
                    throw new InvalidCastException("Invalid ID format");

                productDto.Stock = Quantity;

                purchasedProducts.Add(productDto);

                Console.Clear();
                Console.WriteLine("Product(s) added to order... \n" +
                    "Still buying? write 'yes':");

                if (Console.ReadLine() != "yes")
                    break;
            }

            try
            {
                int nextID
                    = _productOrderRepository.GetPurchaseOrders().Any() == true
                    ? (_productOrderRepository.GetPurchaseOrders().Last().ID + 1) : 1;

                var purcharseOrder = new PurchaseOrder(dbProvider.ID, DateTime.Now, purchasedProducts);
                purcharseOrder.SetProvider(dbProvider);

                _productOrderRepository.CreatePurchaseOrder(purcharseOrder, purchasedProducts);

                Console.Clear();
                Console.WriteLine("\t\tOrder summary \n\n");
                Console.WriteLine(dbProvider.ToString());
                Console.WriteLine(purcharseOrder.ToString());
                foreach (var product in purcharseOrder.AdminOrderProducts.ToList())
                {
                    Console.WriteLine(product.ToString());
                    Console.WriteLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void ShowOrders()
        {
            foreach(var order in _productOrderRepository.GetPurchaseOrders())
            {
                Console.WriteLine(order.ToString());
                foreach (var product in order.AdminOrderProducts.ToList())
                    Console.WriteLine(product.ToString());
                Console.WriteLine("**********************************");
            }
        }

        public static void ShowOrder()
        {
            Console.WriteLine("Purcharse order ID:");
            if (!int.TryParse(Console.ReadLine(), out int OrderID))
                throw new ApplicationException("Invalid ID format");

            var order = _productOrderRepository.GetOrderByID(OrderID);

            if (order == null)
                throw new ApplicationException("Purcharse order with ID not found");

            Console.WriteLine(order.ToString());
            foreach (var product in order.AdminOrderProducts.ToList())
                Console.WriteLine(product.ToString());
        }

        public static void ChangeStatus()
        {
            Console.WriteLine("Purcharse order ID:");
            if (!int.TryParse(Console.ReadLine(), out int OrderID))
                throw new ApplicationException("Invalid ID format");

            var order = _productOrderRepository.GetOrderByID(OrderID);

            if (order == null)
                throw new ApplicationException("Purcharse order with ID not found");

            Console.WriteLine();
            Console.WriteLine($"Actual status order: {order.Status}");
            Console.WriteLine();

            foreach (var status in Enum.GetNames<OrderStatus>())
                Console.WriteLine(status);

            Console.WriteLine();
            Console.WriteLine("New Status:");
            if (!Enum.TryParse(Console.ReadLine(), out OrderStatus newStatus))
                throw new FormatException("Invalid status");

            _productOrderRepository.ChangeStatus(OrderID, newStatus);

            if(newStatus == OrderStatus.Paid)
            {
                foreach(var product in order.AdminOrderProducts)
                {
                    var dbProduct = _productRepository.GetProductByID(product.ID);

                    dbProduct.AddStock(product.Quantity);
                }
            }
        }

        public static void PurchaseOrderMenu()
        {
            bool exit = false;

            do
            {
                Console.Clear();
                Console.WriteLine("\t\t Purchase Menu \n\n" +
                    "1. Purcharse \n" +
                    "2. View all purcharse order \n" +
                    "3. View purcharse order \n" +
                    "4. Change order status \n" +
                    "5. Exit \n\n");
                Console.WriteLine("Choose an option");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        PurchaseOrder();
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadLine();
                        break;

                    case "2":
                        Console.Clear();
                        ShowOrders();
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadLine();
                        break;

                    case "3":
                        Console.Clear();
                        ShowOrder();
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadLine();
                        break;

                    case "4":
                        Console.Clear();
                        ChangeStatus();
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadLine();
                        break;

                    case "5":
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
