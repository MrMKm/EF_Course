using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IDepartmentService
    {
        public List<Department> GetDepartments();
        public Department GetDepartmentByID(int DepartmentID);
        public void CreateDepartment(Department department);
        public void UpdateDepartment(Department department);
        public void DeleteDepartment(Department department);

    }
}
