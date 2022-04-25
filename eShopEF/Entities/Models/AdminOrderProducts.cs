using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class AdminOrderProducts
    {
        public int ID { get; set; }

        public int PurchaseOrderID { get; set; }

        public int ProductID { get; set; }

        public int Quantity { get; set; }

        public virtual PurchaseOrder purchaseOrder { get; set; }

        public virtual Product product { get; set; }

        
        public AdminOrderProducts() { }
    }
}
