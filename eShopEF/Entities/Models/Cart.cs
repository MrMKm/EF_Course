using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Cart
    {
        public List<ProductDto> Products { get; set; } = new List<ProductDto>();


        public Cart() { }

        public Cart(List<ProductDto> Products)
        {
            this.Products = Products;
        }

        public void AddToCart(ProductDto product)
        {
            this.Products.Add(product);
        }

        public void DeleteFromCart(ProductDto productDto)
        {
            this.Products.Remove(productDto);
        }

        public ProductDto GetProductByID(int ID)
        {
            return this.Products
                    .FirstOrDefault(p => p.ID.Equals(ID));
        }
    }
}
