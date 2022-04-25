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
        private List<SubDepartment> subDepartmentsList = TestData.subDepartmentsList;

        public SubDepartmentService()
        {
        }

        public void CreateSubDepartment(SubDepartment subDepartment)
        {
            subDepartmentsList.Add(subDepartment);
        }

        public bool DeleteSubDepartment(SubDepartment subDepartment)
        {
            return subDepartmentsList.Remove(subDepartment);
        }

        public SubDepartment GetSubDepartmentByID(int subID)
        {
            return subDepartmentsList
                .FirstOrDefault(s => s.ID.Equals(subID));
        }

        public List<SubDepartment> GetSubDepartments()
        {
            return subDepartmentsList;
        }

        public void UpdateSubDepartment(SubDepartment subDepartment)
        {
            if (subDepartmentsList.Remove(subDepartment))
                CreateSubDepartment(subDepartment);

            else
                throw new ApplicationException("Sub department not found");
        }
    }
}
