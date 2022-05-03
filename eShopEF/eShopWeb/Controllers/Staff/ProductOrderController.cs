using Entities.Models;
using Entities.Models.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopWeb.Controllers.Staff
{
    [Route("api/product-order")]
    [ApiController]
    [Authorize(Roles = "Staff")]
    public class ProductOrderController : ControllerBase
    {
        private readonly IProductService _productRepository;
        private readonly ISubDepartmentService _subDepartmentRepository;
        private readonly IProductOrderService _productOrderRepository;
        private readonly IProviderService _providerRepository;

        public ProductOrderController(
            IProductService productRepository, 
            ISubDepartmentService subDepartmentRepository,
            IProductOrderService productOrderRepository,
            IProviderService providerRepository
            )
        {
            _productRepository = productRepository;
            _subDepartmentRepository = subDepartmentRepository;
            _productOrderRepository = productOrderRepository;
            _providerRepository = providerRepository;
        }

        [HttpGet]
        [Route("{OrderID}")]
        public IActionResult GetPurcharseOrderByID(int OrderID)
        {
            var order = _productOrderRepository.GetOrderByID(OrderID);

            if (order == null)
                return NotFound("Order with ID not found");

            return Ok(order);
        }

        [HttpGet]
        public IActionResult GetAllPurcharseOrders()
        {
            var orders = _productOrderRepository.GetPurchaseOrders();

            if (!orders.Any())
                return NotFound("Orders not found");

            return Ok(orders);
        }

        [HttpPost]
        public IActionResult CreatePurcharseOrder(ProductOrderCreateDto orderCreateDto)
        {
            var provider = _providerRepository.GetProviderByID(orderCreateDto.PurchaseOrder.ProviderID);

            if (provider == null)
                return NotFound("Provider with ID not found");

            foreach(var product in orderCreateDto.Cart.Products)
            {
                if (_productRepository.GetProductByID(product.ID) == null)
                    return NotFound($"Product with ID: {product.ID} not found");
            }

            _productOrderRepository.CreatePurchaseOrder(orderCreateDto.PurchaseOrder, orderCreateDto.Cart.Products);

            return Ok(orderCreateDto);
        }

        [HttpPut]
        [Route("change-status/{OrderID}")]
        public IActionResult ChangeOrderStatus(int OrderID, OrderStatus NewStatus)
        {
            var order = _productOrderRepository.GetOrderByID(OrderID);

            if (order == null)
                return NotFound("Order with ID not found");

            _productOrderRepository.ChangeStatus(OrderID, NewStatus);

            return Ok(order);
        }


    }
}
