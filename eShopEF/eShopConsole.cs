using Entities;
using Entities.Models;
using Repository.Implementations;
using Shared;
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
        private static ProductService _productRepository;
        private static DepartmentService _departmentRepository;
        private static SubDepartmentService _subDepartmentRepository;
        private static ProviderService _providerRepository;
        private static ProductOrderService _productOrderRepository;
        private static CustomerOrderService _customerOrderRepository;

        public void AddProduct()
        {
            Console.WriteLine("ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
                throw new ApplicationException("Invalid ID format");

            Console.WriteLine("Name: ");
            var name = Console.ReadLine();

            Console.WriteLine("Price: ");
            if (!Decimal.TryParse(Console.ReadLine(), out decimal price))
                throw new ApplicationException("Invalid Price format");

            Console.WriteLine("Description: ");
            var description = Console.ReadLine();

            Console.WriteLine("Brand: ");
            var brand = Console.ReadLine();

            Console.WriteLine("SKU: ");
            var sku = Console.ReadLine();

            Console.WriteLine("Stock: ");
            if (!int.TryParse(Console.ReadLine(), out int stock))
                throw new ApplicationException("Invalid Stock format");

            Console.Clear();

            foreach (var department in _departmentRepository.GetDepartments())
            {
                Console.WriteLine(department.ToString());
                foreach (var subDepartment in department.subDepartments)
                {
                    Console.WriteLine(subDepartment.ToString());
                }
            }

            Console.WriteLine();
            Console.WriteLine("Department ID: ");
            if (!int.TryParse(Console.ReadLine(), out int departmentID))
                throw new ApplicationException("Invalid Sub department ID format");

            Console.WriteLine();
            Console.WriteLine("Sub department ID: ");
            if (!int.TryParse(Console.ReadLine(), out int subDepartmentID))
                throw new ApplicationException("Invalid Sub department ID format");

            try
            {
                Product product = new 
                    Product(name, price, description, brand, sku, stock, subDepartmentID);

                Validation.ObjectValidator(product);

                _productRepository.CreateProduct(product);

                Console.WriteLine("\n\n Product added successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public void UpdateProduct()
        {
            Console.WriteLine("Product ID:");
            if (!int.TryParse(Console.ReadLine(), out int ProductID))
                throw new InvalidCastException("Invalid ID format");

            var dbProduct = _productRepository.GetProductByID(ProductID);

            if (dbProduct == null)
                throw new ApplicationException("Product with ID not found");

            Console.WriteLine("New name: ");
            var name = Console.ReadLine();

            Console.WriteLine("New price: ");
            if (!Decimal.TryParse(Console.ReadLine(), out decimal price))
                throw new ApplicationException("Invalid Price format");

            Console.WriteLine("New description: ");
            var description = Console.ReadLine();

            Console.WriteLine("New brand: ");
            var brand = Console.ReadLine();

            Console.WriteLine("New stock: ");
            if (!int.TryParse(Console.ReadLine(), out int stock))
                throw new ApplicationException("Invalid Stock format");

            try
            {
                dbProduct.Update(name, price, description, brand, stock);
                _productRepository.UpdateProduct(dbProduct);

                Console.WriteLine("\n\n Product updated successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void ViewProduct()
        {
            Console.WriteLine("Product ID:");
            if (!int.TryParse(Console.ReadLine(), out int ProductID))
                throw new InvalidCastException("Invalid ID format");

            var dbProduct = _productRepository.GetProductByID(ProductID);

            if (dbProduct == null)
                throw new ApplicationException("Product with ID not found");

            Console.WriteLine("\n\n");
            Console.WriteLine(dbProduct.ToString());
        }

        public void ViewAllProducts()
        {
            var products = _productRepository.GetProducts();

            if (!products.Any())
                throw new ApplicationException("Products not found");

            foreach (var product in products)
            {
                Console.WriteLine("\n");
                Console.WriteLine(product.ToString());
            }
        }

        public void DeleteProduct()
        {
            Console.WriteLine("Product ID:");
            if (!int.TryParse(Console.ReadLine(), out int ProductID))
                throw new InvalidCastException("Invalid ID format");

            var dbProduct = _productRepository.GetProductByID(ProductID);

            if (dbProduct == null)
                throw new ApplicationException("Product with ID not found");

            try
            {
                if (!_productRepository.DeleteProduct(dbProduct))
                    throw new ApplicationException("Product not found in database");

                Console.WriteLine("\n\n Product deleted successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public bool AdminMenu()
        {
            bool exit = false;

            do
            {
                Console.Clear();
                Console.WriteLine("\t\t eShop ***Admin*** \n\n" +
                    "1. Add product \n" +
                    "2. Update product \n" +
                    "3. View product \n" +
                    "4. View all products \n" +
                    "5. Delete product \n" +
                    "6. Reports \n" +
                    "7. Purchase Orders \n" +
                    "8. Sign up \n\n");
                Console.WriteLine("Choose an option");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        AddProduct();
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadLine();
                        break;

                    case "2":
                        Console.Clear();
                        UpdateProduct();
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadLine();
                        break;

                    case "3":
                        Console.Clear();
                        ViewProduct();
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadLine();
                        break;

                    case "4":
                        Console.Clear();
                        ViewAllProducts();
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadLine();
                        break;

                    case "5":
                        Console.Clear();
                        DeleteProduct();
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadLine();
                        break;

                    case "6":
                        Console.Clear();
                        ReportsMenu();
                        break;

                    case "7":
                        Console.Clear();
                        PurchaseOrderMenu();
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

            return exit;
        }

        public bool CustomerMenu()
        {
            bool exit = false;

            do
            {
                Console.Clear();
                Console.WriteLine("\t\t eShop ***Customer*** \n\n" +
                    "1. Purchase \n" +
                    "2. View an order \n" +
                    "3. View all orders \n" +
                    "4. Cancel order \n" +
                    "5. Sign up \n\n");
                Console.WriteLine("Choose an option");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        CustomerPurcharse();
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadLine();
                        break;

                    case "2":
                        Console.Clear();
                        ShowCustomerOrder();
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadLine();
                        break;

                    case "3":
                        Console.Clear();
                        ShowCustomerOrders();
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadLine();
                        break;

                    case "4":
                        Console.Clear();
                        CancelCustomerOrder();
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

            return exit;
        }

        public bool PrincipalMenu()
        {
            bool exit = false;

            do
            {
                Console.Clear();
                Console.WriteLine("\t\t eShop \n\n" +
                    "1. Customer \n" +
                    "2. Admin \n" +
                    "3. Exit \n\n");
                Console.WriteLine("Choose an option");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        CustomerMenu();
                        break;

                    case "2":
                        Console.Clear();
                        Console.WriteLine("Password: ");
                        if(Console.ReadLine() == RepositoryContextSeed.AdminPassword)
                            AdminMenu();

                        else
                        {
                            Console.WriteLine("Wrong password");
                            Console.WriteLine("\nPress any key to continue...");
                            Console.ReadLine();
                        }
                        break;

                    case "3":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid option, try again...");
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadLine();
                        break;
                }
            } while (!exit);

            return exit;
        }

        public eShopConsole()
        {
            _productRepository = new ProductService();
            _departmentRepository = new DepartmentService();
            _subDepartmentRepository = new SubDepartmentService();
            _providerRepository = new ProviderService();
            _productOrderRepository = new ProductOrderService();
            _customerOrderRepository = new CustomerOrderService();
        }
    }
}
