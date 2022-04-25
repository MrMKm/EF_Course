using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class CustomerOrderProduct
    {
        public int ID { get; set; }

        public int CustomerOrderID { get; set; }

        public int ProductID { get; set; }

        public int Quantity { get; set; }

        public virtual CustomerOrder customerOrder { get; set; }

        public virtual Product product { get; set; }

        public CustomerOrderProduct() { }

        public CustomerOrderProduct(int CustomerOrderID, int ProductID, int Quantity) 
        {
            this.CustomerOrderID = CustomerOrderID;
            this.ProductID = ProductID;
            this.Quantity = Quantity;
        }
    }
}
