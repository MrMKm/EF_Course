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
    [Route("api/provider")]
    [ApiController]
    [Authorize(Roles = "Staff")]
    public class ProviderController : ControllerBase
    {
        private readonly IProductService _productRepository;
        private readonly ISubDepartmentService _subDepartmentRepository;
        private readonly IProductOrderService _productOrderRepository;
        private readonly IProviderService _providerRepository;

        public ProviderController(
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
        [Route("{ProviderID}")]
        public IActionResult GetProviderByID(int ProviderID)
        {
            var provider = _providerRepository.GetProviderByID(ProviderID);

            if (provider == null)
                return NotFound("Provider with ID not found");

            return Ok(provider);
        }

        [HttpGet]
        public IActionResult GetAllProviders()
        {
            var providers = _providerRepository.GetProviders();

            if (!providers.Any())
                return NotFound("Providers not found");

            return Ok(providers);
        }

        [HttpPost]
        public IActionResult CreateProvider(Provider provider)
        {
            _providerRepository.CreateProvider(provider);

            return Ok(provider);
        }
    }
}
