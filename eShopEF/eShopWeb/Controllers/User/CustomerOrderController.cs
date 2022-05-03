using Entities.Models;
using Entities.Models.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopWeb.Controllers.User
{
    [Route("api/customer-order")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class CustomerOrderController : ControllerBase
    {
        private readonly IProductService _productRepository;
        private readonly ISubDepartmentService _subDepartmentRepository;
        private readonly ICustomerOrderService _customerOrderRepository;
        private readonly IProviderService _providerRepository;

        public CustomerOrderController(
            IProductService productRepository,
            ISubDepartmentService subDepartmentRepository,
            ICustomerOrderService customerOrderRepository,
            IProviderService providerRepository
            )
        {
            _productRepository = productRepository;
            _subDepartmentRepository = subDepartmentRepository;
            _customerOrderRepository = customerOrderRepository;
            _providerRepository = providerRepository;
        }

        [HttpGet]
        [Route("{OrderID}")]
        public IActionResult GetPurcharseOrderByID(int OrderID)
        {
            var order = _customerOrderRepository.GetOrderByID(OrderID);

            if (order == null)
                return NotFound("Order with ID not found");

            return Ok(order);
        }

        [HttpGet]
        public IActionResult GetAllPurcharseOrders()
        {
            var orders = _customerOrderRepository.GetCustomerOrders();

            if (!orders.Any())
                return NotFound("Orders not found");

            return Ok(orders);
        }

        [HttpPost]
        public IActionResult CreateCustomerOrder(CustomerOrderCreateDto orderCreateDto)
        {
            foreach (var product in orderCreateDto.Cart.Products)
            {
                if (_productRepository.GetProductByID(product.ID) == null)
                    return NotFound($"Product with ID: {product.ID} not found");
            }

            _customerOrderRepository.CreateCustomerOrder(orderCreateDto.CustomerOrder, orderCreateDto.Cart);

            return Ok(orderCreateDto);
        }

        [HttpDelete]
        [Route("{OrderID}")]
        public IActionResult CancelOrder(int OrderID)
        {
            var order = _customerOrderRepository.GetOrderByID(OrderID);

            if (order == null)
                return NotFound("Order with ID not found");

            _customerOrderRepository.CancelCustomerOrder(order);

            return Ok("Order canceled successfully");
        }
    }
}
