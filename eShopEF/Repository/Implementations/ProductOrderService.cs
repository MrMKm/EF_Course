using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Interfaces;
using Shared;
using Shared.DataTransferObjects;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementations
{
    public class ProductOrderService : IProductOrderService
    {
        private readonly RepositoryContext repositoryContext;

        public ProductOrderService(RepositoryContext _repositoryContext)
        {
            repositoryContext = _repositoryContext;
        }

        public void ChangeStatus(int OrderID, OrderStatus status)
        {
            var order = GetOrderByID(OrderID);
            order.Status = status;

            repositoryContext.SaveChanges();
        }

        public void CreatePurchaseOrder(PurchaseOrder purchaseOrder, List<ProductDto> products)
        {
            repositoryContext.PurchaseOrder.Add(purchaseOrder);
            repositoryContext.SaveChanges();

            foreach (var product in products)
            {
                repositoryContext.AdminOrderProduct.Add(new AdminOrderProducts(purchaseOrder.ID, product.ID, product.Stock));
            }
            repositoryContext.SaveChanges();
        }

        public PurchaseOrder GetOrderByID(int OrderID)
        {
            return repositoryContext.PurchaseOrder
                .Include(o => o.AdminOrderProducts).ThenInclude(a => a.product)
                .FirstOrDefault(o => o.ID.Equals(OrderID));
        }

        public List<PurchaseOrder> GetPurchaseOrders()
        {
            return repositoryContext.PurchaseOrder
                .Include(o => o.AdminOrderProducts).ThenInclude(a => a.product)
                .ToList();
        }
    }
}
