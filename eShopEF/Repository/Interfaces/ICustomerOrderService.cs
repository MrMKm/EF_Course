using Entities.Models;
using Entities.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ICustomerOrderService
    {
        public List<CustomerOrder> GetCustomerOrders();
        public CustomerOrder GetOrderByID(int OrderID);
        public void CreateCustomerOrder(CustomerOrder customerOrder, Cart cart);
        public void CancelCustomerOrder(CustomerOrder customerOrder);
    }
}
