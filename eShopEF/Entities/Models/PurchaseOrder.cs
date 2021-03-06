using Shared.DataTransferObjects;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class PurchaseOrder
    {
        public int ID { get; private set; }

        public decimal Total { get; private set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        public int ProviderID { get; set; }

        public Provider Provider { get; set; }

        public virtual ICollection<AdminOrderProducts> AdminOrderProducts { get; set; }

        public OrderStatus Status { get; set; }




        public PurchaseOrder() { }

        public PurchaseOrder(int ProviderID, DateTime Date, List<ProductDto> PurchasedProducts)
        {
            this.Total = PurchasedProducts.Sum(p => p.Price * p.Stock);
            this.ProviderID = ProviderID;
            this.Date = Date;

            this.Status = OrderStatus.Pending;
        }

        public void SetProvider(Provider provider)
        {
            this.Provider = provider;
        }

        public override string ToString()
        {
            return $"Purchase Order Information; \n\n" +
                $"ID: {this.ID} \n" +
                $"Date: {this.Date.ToShortDateString()} \n" +
                $"Provider ID: {this.ProviderID} \n" +
                $"Status: {this.Status} \n" +
                $"Total: ${this.Total} \n" +
                $"Products: \n";
        }
    }
}
