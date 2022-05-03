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
    [Route("api/deparment")]
    [ApiController]
    [Authorize(Roles = "Staff")]
    public class DepartmentController : ControllerBase
    {
        private readonly IProductService _productRepository;
        private readonly ISubDepartmentService _subDepartmentRepository;
        private readonly IProductOrderService _productOrderRepository;
        private readonly IProviderService _providerRepository;
        private readonly IDepartmentService _departmentRepository;

        public DepartmentController(
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
        [Route("{DepartmentID}")]
        public IActionResult GetDepartmentByID(int DepartmentID)
        {
            var department = _departmentRepository.GetDepartmentByID(DepartmentID);

            if (department == null)
                return NotFound("Department with ID not found");

            return Ok(department);
        }

        [HttpGet]
        public IActionResult GetAllDepartments()
        {
            var departments = _departmentRepository.GetDepartments();

            if (!departments.Any())
                return NotFound("Departments not found");

            return Ok(departments);
        }

        [HttpPost]
        public IActionResult CreateDepartment(Department deparment)
        {
            _departmentRepository.CreateDepartment(deparment);

            return Ok(deparment);
        }

        [HttpPut]
        [Route("{DepartmentID}")]
        public IActionResult UpdateDepartment(int DepartmentID, Department department)
        {
            var DBdepartment = _departmentRepository.GetDepartmentByID(DepartmentID);

            if (DBdepartment == null)
                return NotFound("Department with ID not found");

            _departmentRepository.UpdateDepartment(department);

            return Ok(department);
        }

        [HttpDelete]
        [Route("{DepartmentID}")]
        public IActionResult DeleteDepartment(int DepartmentID)
        {
            var DBdepartment = _departmentRepository.GetDepartmentByID(DepartmentID);

            if (DBdepartment == null)
                return NotFound("Department with ID not found");

            _departmentRepository.DeleteDepartment(DBdepartment);

            return Ok("Department deleted successfully");
        }
    }
}
