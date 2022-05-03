using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
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
        private readonly RepositoryContext repositoryContext;

        public ProductService(RepositoryContext _repositoryContext)
        {
            repositoryContext = _repositoryContext;
        }

        public Product CreateProduct(Product product)
        {
            repositoryContext.Product.Add(product);
            repositoryContext.SaveChanges();

            return product;
        }

        public bool DeleteProduct(Product product)
        {
            if(!repositoryContext.Product.ToList().Remove(product))
                throw new ApplicationException("Product not found in database");

            repositoryContext.SaveChanges();

            return true;
        }

        public Product GetProductByID(int ProductID)
        {
            return repositoryContext.Product
                .FirstOrDefault(p => p.ID.Equals(ProductID));
        }

        public List<Product> GetProducts()
        {
            return repositoryContext.Product.ToList();
        }

        public Product UpdateProduct(Product product)
        {
            Validation.ObjectValidator(product);

            if (repositoryContext.Product.ToList().Remove(product))
            {
                CreateProduct(product);
                return product;
            }

            else
                throw new ApplicationException("Product not found");
        }
    }
}
