using Entities;
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
    public class DepartmentService : IDepartmentService
    {
        private readonly RepositoryContext repositoryContext;

        public DepartmentService(RepositoryContext _repositoryContext)
        {
            repositoryContext = _repositoryContext;
        }

        public void CreateDepartment(Department department)
        {
            repositoryContext.Department.Add(department);
            repositoryContext.SaveChanges();
        }

        public void DeleteDepartment(Department department)
        {
            repositoryContext.Department.Remove(department);
            repositoryContext.SaveChanges();
        }

        public Department GetDepartmentByID(int DepartmentID)
        {
            return repositoryContext.Department
                .FirstOrDefault(d => d.ID.Equals(DepartmentID));
        }

        public List<Department> GetDepartments()
        {
            return repositoryContext.Department.ToList();
        }

        public void UpdateDepartment(Department department)
        {
            if (repositoryContext.Department.ToList().Remove(department))
                CreateDepartment(department);

            else
                throw new ApplicationException("Department not found");
        }
    }
}
