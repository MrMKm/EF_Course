using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopWeb.Controllers.Staff
{
    [Route("api/product")]
    [ApiController]
    [Authorize(Roles = "Staff")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productRepository;
        private readonly ISubDepartmentService _subDepartmentRepository;

        public ProductController(IProductService productRepository, ISubDepartmentService subDepartmentRepository)
        {
            _productRepository = productRepository;
            _subDepartmentRepository = subDepartmentRepository;
        }

        [HttpGet]
        [Route("{ProductID}")]
        public IActionResult GetProductByID(int ProductID)
        {
            var product = _productRepository.GetProductByID(ProductID);

            if (product == null)
                return NotFound("Product with ID not found");

            return Ok(product);
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _productRepository.GetProducts();

            if (!products.Any())
                return NotFound("Products not found");

            return Ok(products);
        }

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            var subDepartment = _subDepartmentRepository.GetSubDepartmentByID(product.SubDepartmentID);

            if (subDepartment == null)
                return NotFound("Sub department with ID not found");

            return Ok(_productRepository.CreateProduct(product));
        }

        [HttpPut]
        [Route("{ProductID}")]
        public IActionResult UpdateProduct(int ProductID, Product product)
        {
            var DBproduct = _productRepository.GetProductByID(ProductID);

            if (DBproduct == null)
                return NotFound("Product with ID not found");

            var subDepartment = _subDepartmentRepository.GetSubDepartmentByID(product.SubDepartmentID);

            if (subDepartment == null)
                return NotFound("Sub department with ID not found");

            return Ok(_productRepository.UpdateProduct(product));
        }

        [HttpDelete]
        [Route("{ProductID}")]
        public IActionResult DeleteProduct(int ProductID)
        {
            var DBproduct = _productRepository.GetProductByID(ProductID);

            if (DBproduct == null)
                return NotFound("Product with ID not found");

            if (_productRepository.DeleteProduct(DBproduct))
                return Ok("Product deleted successfully");

            return BadRequest("Something went wrong...");
        }
    }
}
