using Entities;
using Entities.Models;
using Entities.Models.DataTransferObjects;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Interfaces;
using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementations
{
    public class CustomerOrderService : ICustomerOrderService
    {
        private readonly RepositoryContext repositoryContext;

        public CustomerOrderService(RepositoryContext _repositoryContext)
        {
            repositoryContext = _repositoryContext;
        }

        public void CancelCustomerOrder(CustomerOrder customerOrder)
        {
            var _productServiceRepository = new ProductService(repositoryContext);

            foreach (var product in customerOrder.PurchasedProducts)
            {
                var dbProduct = _productServiceRepository.GetProductByID(product.ID);
                dbProduct.AddStock(product.Quantity);
            }

            repositoryContext.CustomerOrder.Remove(customerOrder);
            repositoryContext.SaveChanges();
        }

        public void CreateCustomerOrder(CustomerOrder customerOrder, Cart cart)
        {
            repositoryContext.CustomerOrder.Add(customerOrder);
            repositoryContext.SaveChanges();

            foreach(var product in cart.Products)
            {
                repositoryContext.CustomerOrderProduct.Add(new CustomerOrderProduct(customerOrder.ID, product.ID, product.Stock));
            }

            repositoryContext.SaveChanges();
        }

        public List<CustomerOrder> GetCustomerOrders()
        {
            return repositoryContext.CustomerOrder
                .Include(c => c.PurchasedProducts).ThenInclude(p => p.product)
                .ToList();
        }

        public CustomerOrder GetOrderByID(int OrderID)
        {
            return repositoryContext.CustomerOrder
                .Include(c => c.PurchasedProducts).ThenInclude(p => p.product)
                .FirstOrDefault(o => o.ID.Equals(OrderID));
        }
    }
}
