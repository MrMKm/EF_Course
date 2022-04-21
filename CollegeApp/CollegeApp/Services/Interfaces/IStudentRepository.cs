using Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeApp.Services.Implementations
{
    public interface IStudentRepository
    {
        public void RegisterStudent(StudentRegisterDto studentDto);
    }
}
