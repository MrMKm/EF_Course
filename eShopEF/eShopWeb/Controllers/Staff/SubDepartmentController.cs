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
    [Route("api/sub-department")]
    [ApiController]
    [Authorize(Roles = "Staff")]
    public class SubDepartmentController : ControllerBase
    {
        private readonly IProductService _productRepository;
        private readonly ISubDepartmentService _subDepartmentRepository;
        private readonly IProductOrderService _productOrderRepository;
        private readonly IProviderService _providerRepository;
        private readonly IDepartmentService _departmentRepository;

        public SubDepartmentController(
            IProductService productRepository,
            ISubDepartmentService subDepartmentRepository,
            IProductOrderService productOrderRepository,
            IProviderService providerRepository,
            IDepartmentService departmentRepository
            )
        {
            _productRepository = productRepository;
            _subDepartmentRepository = subDepartmentRepository;
            _productOrderRepository = productOrderRepository;
            _providerRepository = providerRepository;
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        [Route("{SubDepartmentID}")]
        public IActionResult GetSubDepartmentByID(int SubDepartmentID)
        {
            var subDepartment = _subDepartmentRepository.GetSubDepartmentByID(SubDepartmentID);

            if (subDepartment == null)
                return NotFound("Sub department with ID not found");

            return Ok(subDepartment);
        }

        [HttpGet]
        public IActionResult GetAllSubDepartments()
        {
            var subDepartments = _subDepartmentRepository.GetSubDepartments();

            if (!subDepartments.Any())
                return NotFound("Sub departments not found");

            return Ok(subDepartments);
        }

        [HttpPost]
        public IActionResult CreateSubDepartment(SubDepartment subDepartment)
        {
            var department = _departmentRepository.GetDepartmentByID(subDepartment.DepartmentID);

            if (department == null)
                return NotFound("Department with ID not found");

            _subDepartmentRepository.CreateSubDepartment(subDepartment);

            return Ok(subDepartment);
        }

        [HttpPut]
        [Route("{SubDepartmentID}")]
        public IActionResult UpdateSubDepartment(int SubDepartmentID, SubDepartment subDepartment)
        {
            var DBsubDepartment = _subDepartmentRepository.GetSubDepartmentByID(SubDepartmentID);

            if (DBsubDepartment == null)
                return NotFound("Sub department with ID not found");

            var department = _departmentRepository.GetDepartmentByID(subDepartment.DepartmentID);

            if (department == null)
                return NotFound("Department with ID not found");

            _subDepartmentRepository.UpdateSubDepartment(subDepartment);

            return Ok(subDepartment);
        }

        [HttpDelete]
        [Route("{SubDepartmentID}")]
        public IActionResult DeleteSubDepartment(int SubDepartmentID)
        {
            var DBsubDepartment = _subDepartmentRepository.GetSubDepartmentByID(SubDepartmentID);

            if (DBsubDepartment == null)
                return NotFound("Sub department with ID not found");

            _subDepartmentRepository.DeleteSubDepartment(DBsubDepartment);

            return Ok("Sub department deleted successfully");
        }
    }
}
