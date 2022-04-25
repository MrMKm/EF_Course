using Entities.Models;
using Repository;
using Repository.Interfaces;
using Shared;
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
        private List<PurchaseOrder> purchaseOrdersList = new List<PurchaseOrder>();

        public void ChangeStatus(int OrderID, OrderStatus status)
        {
            var order = GetOrderByID(OrderID);
            order.Status = status;
        }

        public void CreatePurchaseOrder(PurchaseOrder purchaseOrder)
        {
            purchaseOrdersList.Add(purchaseOrder);
        }

        public PurchaseOrder GetOrderByID(int OrderID)
        {
            return purchaseOrdersList
                .FirstOrDefault(o => o.ID.Equals(OrderID));
        }

        public List<PurchaseOrder> GetPurchaseOrders()
        {
            return purchaseOrdersList;
        }
    }
}
