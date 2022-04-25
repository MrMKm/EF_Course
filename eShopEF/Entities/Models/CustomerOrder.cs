using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class CustomerOrder
    {
        public int ID { get; private set; }

        public decimal Total { get; private set; }

        public DateTime Date { get; private set; } = DateTime.Now;

        public List<ProductDto> PurchasedProducts { get; private set; }

        public CustomerOrder() { }

        public CustomerOrder(int ID, List<ProductDto> PurchasedProducts)
        {
            this.ID = ID;
            this.Total = PurchasedProducts.Sum(p => p.Price * p.Stock);
            this.PurchasedProducts = PurchasedProducts;
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
