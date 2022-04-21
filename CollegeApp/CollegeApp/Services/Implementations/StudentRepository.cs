using Entities;
using Entities.Models;
using Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeApp.Services.Implementations
{
    public class StudentRepository : IStudentRepository
    {
        private readonly RepositoryContext repositoryContext = new RepositoryContext();

        public void RegisterStudent(StudentRegisterDto studentDto)
        {
            var student = new Student(studentDto.Code, studentDto.FirstName, studentDto.LastName);

            try
            {
                repositoryContext.Add(student);
                repositoryContext.SaveChanges();
            }
            catch(Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }
    }
}
