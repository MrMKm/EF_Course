using Entities.Models;
using Repository;
using Repository.Interfaces;
using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementations
{
    public class ProductService : IProductService
    {
        private List<Product> ProductsList = TestData.ProductsList;

        public void CreateProduct(Product product)
        {
            ProductsList.Add(product);
        }

        public bool DeleteProduct(Product product)
        {
            return ProductsList.Remove(product);
        }

        public Product GetProductByID(int ProductID)
        {
            return ProductsList
                .FirstOrDefault(p => p.ID.Equals(ProductID));
        }

        public List<Product> GetProducts()
        {
            return ProductsList;
        }

        public void UpdateProduct(Product product)
        {
            Validation.ObjectValidator(product);

            if (ProductsList.Remove(product))
                CreateProduct(product);

            else
                throw new ApplicationException("Product not found");

            ProductsList = ProductsList.OrderBy(p => p.ID).ToList();
        }
    }
}
