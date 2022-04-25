using Shared.Enums;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects;

namespace Repository.Interfaces
{
    public interface IProductOrderService
    {
        public void CreatePurchaseOrder(PurchaseOrder purchaseOrder, List<ProductDto> products);
        public List<PurchaseOrder> GetPurchaseOrders();
        public PurchaseOrder GetOrderByID(int OrderID);
        public void ChangeStatus(int OrderID, OrderStatus status);
    }
}
