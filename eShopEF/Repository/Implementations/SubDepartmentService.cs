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
    public class SubDepartmentService : ISubDepartmentService
    {
        private readonly RepositoryContext repositoryContext = new RepositoryContext();

        public SubDepartmentService()
        {
        }

        public void CreateSubDepartment(SubDepartment subDepartment)
        {
            repositoryContext.SubDepartment.Add(subDepartment);
            repositoryContext.SaveChanges();
        }

        public bool DeleteSubDepartment(SubDepartment subDepartment)
        {
            if (!repositoryContext.SubDepartment.ToList().Remove(subDepartment))
                throw new ApplicationException("SubDepartment not found in database");

            repositoryContext.SaveChanges();

            return true;
        }

        public SubDepartment GetSubDepartmentByID(int subID)
        {
            return repositoryContext.SubDepartment
                .FirstOrDefault(s => s.ID.Equals(subID));
        }

        public List<SubDepartment> GetSubDepartments()
        {
            return repositoryContext.SubDepartment.ToList();
        }

        public void UpdateSubDepartment(SubDepartment subDepartment)
        {
            if (repositoryContext.SubDepartment.ToList().Remove(subDepartment))
                CreateSubDepartment(subDepartment);

            else
                throw new ApplicationException("Sub department not found");
        }
    }
}
