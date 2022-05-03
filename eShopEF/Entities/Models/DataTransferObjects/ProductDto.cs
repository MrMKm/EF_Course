using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.DataTransferObjects
{
    public class ProductDto
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(1, ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Stock is required")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "SKU is required")]
        [MinLength(1, ErrorMessage = "SKU is required")]
        public string SKU { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MinLength(1, ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Brand is required")]
        [MinLength(1, ErrorMessage = "Brand is required")]
        public string Brand { get; set; }

        public override string ToString()
        {
            return $"Product Information; \n\n" +
                $"ID: {this.ID} \n" +
                $"Name: {this.Name} \n" +
                $"Price: {this.Price} \n" +
                $"Description: {this.Description} \n" +
                $"Brand: {this.Brand} \n" +
                $"SKU: {this.SKU} \n" +
                $"Stock: {this.Stock} \n";
        }
    }
}
