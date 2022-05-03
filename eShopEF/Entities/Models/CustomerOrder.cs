
using Entities.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class CustomerOrder
    {
        public int ID { get; set; }

        public decimal Total { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public virtual ICollection<CustomerOrderProduct> PurchasedProducts { get; set; }

        public CustomerOrder() { }

        public CustomerOrder(List<ProductDto> PurchasedProducts)
        {
            this.Total = PurchasedProducts.Sum(p => p.Price * p.Stock);
        }

        public override string ToString()
        {
            return $"Purchase Order Information; \n\n" +
                $"ID: {this.ID} \n" +
                $"Date: {this.Date.ToShortDateString()} \n" +
                $"Total: ${this.Total} \n" +
                $"Products: \n";
        }
    }
}
