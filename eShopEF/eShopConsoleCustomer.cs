using Entities.Models;
using Repository;
using Repository.Implementations;
using Shared;
using Shared.DataTransferObjects;
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
        public static void CustomerPurcharse()
        {
            var cart = new Cart();
            var productDto = new ProductDto();

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

                Console.WriteLine($"Add {dbProduct.Name} to cart? write 'yes':");
                if (Console.ReadLine() == "yes")
                {

                    productDto = new ProductDto
                    {
                        ID = dbProduct.ID,
                        Name = dbProduct.Name,
                        Brand = dbProduct.Brand,
                        Description = dbProduct.Description,
                        Price = dbProduct.Price,
                        Stock = dbProduct.Stock,
                        SKU = dbProduct.SKU,
                    };

                    if (cart.Products.Contains(cart.GetProductByID(productDto.ID)))
                    {
                        productDto = cart.GetProductByID(productDto.ID);

                        Console.WriteLine();
                        Console.WriteLine("Product is already in cart");
                        Console.WriteLine("New quantity: ");
                        if (!int.TryParse(Console.ReadLine(), out int newQuantity))
                            throw new InvalidCastException("Invalid ID format");

                        if (dbProduct.Stock - newQuantity >= 1)
                        {
                            dbProduct.AddStock(productDto.Stock);
                            productDto.Stock = newQuantity;
                            dbProduct.RemoveStock(productDto.Stock);
                        }
                    }

                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Quantity: ");
                        if (!int.TryParse(Console.ReadLine(), out int Quantity))
                            throw new InvalidCastException("Invalid ID format");

                        if (dbProduct.Stock - Quantity >= 1)
                        {
                            productDto.Stock = Quantity;
                            cart.AddToCart(productDto);
                            dbProduct.RemoveStock(productDto.Stock);
                        }

                        else
                            throw new ApplicationException("Insufficient stock available");

                        Console.Clear();
                        Console.WriteLine("Product(s) added to cart... \n");
                    }
                }

                Console.WriteLine("See or modify cart? write 'yes':");
                if (Console.ReadLine() == "yes")
                {
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine("\t\tCart details\n\n");
                        if (!cart.Products.Any())
                            Console.WriteLine("Cart is empty");

                        else
                        {
                            foreach (var product in cart.Products)
                            {
                                Console.WriteLine(product.ToString());
                            }

                            Console.WriteLine("Delete product from cart? write 'yes':");
                            if (Console.ReadLine() == "yes")
                            {
                                Console.WriteLine();
                                Console.WriteLine("Product ID:");
                                if (!int.TryParse(Console.ReadLine(), out int deleteProductID))
                                    throw new InvalidCastException("Invalid ID format");

                                productDto = cart.GetProductByID(deleteProductID);
                                cart.DeleteFromCart(productDto);
                                dbProduct.AddStock(productDto.Stock);
                            }
                        }

                        Console.WriteLine("Still modifying cart? write 'yes':");
                        if (Console.ReadLine() != "yes")
                            break;
                    }
                }


                Console.WriteLine("Still buying? write 'yes':");
                if (Console.ReadLine() != "yes")
                    break;
            }

            try
            {
                int nextID
                    = _customerOrderRepository.GetCustomerOrders().Any() == true
                    ? (_customerOrderRepository.GetCustomerOrders().Last().ID + 1) : 1;

                var purcharseOrder = new CustomerOrder(cart.Products);

                _customerOrderRepository.CreateCustomerOrder(purcharseOrder, cart);

                Console.Clear();
                Console.WriteLine("\t\tOrder summary \n\n");
                Console.WriteLine(purcharseOrder.ToString());
                foreach (var product in _customerOrderRepository.GetOrderByID(purcharseOrder.ID).PurchasedProducts.ToList())
                {
                    Console.WriteLine(product.product.ToString());
                    Console.WriteLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void ShowCustomerOrder()
        {
            Console.WriteLine("Purcharse order ID:");
            if (!int.TryParse(Console.ReadLine(), out int OrderID))
                throw new ApplicationException("Invalid ID format");

            var order = _customerOrderRepository.GetOrderByID(OrderID);

            if (order == null)
                throw new ApplicationException("Purcharse order with ID not found");

            Console.WriteLine(order.ToString());
            foreach (var product in order.PurchasedProducts.ToList())
                Console.WriteLine(product.product.ToString());
        }

        public void ShowCustomerOrders()
        {
            decimal total = 0;

            foreach (var order in _customerOrderRepository.GetCustomerOrders())
            {
                total += order.Total;

                Console.WriteLine(order.ToString());
                foreach (var product in order.PurchasedProducts.ToList())
                    Console.WriteLine(product.product.ToString());
                Console.WriteLine("**********************************");
            }

            Console.WriteLine($"\n " +
                $"Total spended: {total} \n");
        }

        public void CancelCustomerOrder()
        {
            Console.WriteLine("Purcharse order ID:");
            if (!int.TryParse(Console.ReadLine(), out int OrderID))
                throw new ApplicationException("Invalid ID format");

            var order = _customerOrderRepository.GetOrderByID(OrderID);

            if (order == null)
                throw new ApplicationException("Purcharse order with ID not found");

            _customerOrderRepository.CancelCustomerOrder(order);
        }
    }
}
