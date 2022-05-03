using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.DataTransferObjects
{
    public class ProductOrderCreateDto
    {
        [Required]
        public PurchaseOrder PurchaseOrder { get; set; }

        [Required]
        public Cart Cart { get; set; }
    }
}
