using Entities.Models;
using Shared.Dto;
using Shared.Enum;
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
        public StudentDto GetStudentByCode(string Code);
        public List<Enrollment> GetEnrollmentsByStudent(int StudentID);
        public void AssignCourse(CourseAssignDto courseAssign);
        public void EvaluateStudent(EnrollmentDto enrollmentDto);
        StudentEvaluationDto GetEvaluationByCode(string Code);
        public void ChangeStudentStatusFromCourse(int StudentID, int CourseID);
    }
}
