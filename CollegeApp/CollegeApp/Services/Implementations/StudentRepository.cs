using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Dto;
using Shared.Enum;
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

        public StudentDto GetStudentByCode(string Code)
        {
            var student = repositoryContext.Student
                .Select(s => new StudentDto
                {
                    ID = s.ID,
                    Code = s.Code,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                }).FirstOrDefault(s => s.Code.Equals(Code));

            if (student == null)
                throw new ArgumentNullException("Student with Code not found");

            return student;
        }

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

        public void AssignCourse(CourseAssignDto courseAssign)
        {
            var student = repositoryContext.Student
                .FirstOrDefault(s => s.Code.ToLower().Equals(courseAssign.StudentCode));

            var course = repositoryContext.Course
                .Include(c => c.Enrollments)
                .FirstOrDefault(c => c.ID.Equals(courseAssign.CourseID));

            if (student == null)
                throw new ArgumentNullException("Student with Code not found");

            if (course == null)
                throw new ArgumentNullException("Course with ID not found");

            if (course.Enrollments.Select(c => c.StudentID).Contains(student.ID))
                throw new ArgumentNullException("Student already assigned to this course, try to change inscription status");

            if(course.Capacity == 0)
                throw new ApplicationException("Course it's full");

            if(course.Capacity == 1)
                course.Capacity = 0;

            else
                course.Capacity -= 1;

            var newEnrollment = repositoryContext.Enrollment.Add(new Enrollment(student.ID, course.ID));

            student.Enrollments.Add(newEnrollment.Entity);
            course.Enrollments.Add(newEnrollment.Entity);

            repositoryContext.SaveChanges();
        }

        public List<Enrollment> GetEnrollmentsByStudent(int StudentID)
        {
            var enrollments = repositoryContext.Enrollment
                .Include(c => c.course)
                .Where(c => c.StudentID.Equals(StudentID))
                .ToList();

            if(!enrollments.Any())
                throw new ArgumentNullException("Enrollments with Student ID not found");

            return enrollments;
        }

        public void EvaluateStudent(EnrollmentDto enrollmentDto)
        {
            var enrollment = repositoryContext.Enrollment
                .FirstOrDefault(e => e.StudentID.Equals(enrollmentDto.StudentID) && 
                                e.CourseID.Equals(enrollmentDto.CourseID) && e.Active.Equals(true));

            if (enrollment == null)
                throw new ArgumentNullException("Enrollment with credentials not found or inactive inscription");

            enrollment.Grade = enrollmentDto.Grade;
            repositoryContext.SaveChanges();
        }

        public StudentEvaluationDto GetEvaluationByCode(string Code)
        {
            var enrollments = repositoryContext.Enrollment
                .Where(e => e.student.Code.ToLower().Equals(Code.ToLower()) && e.Active.Equals(true))
                .Select(e => new StudentEnrollmentDto
                {
                    Title = e.course.Title,
                    Grade = e.Grade
                }).ToList();

            var evaluation = repositoryContext.Student
                .Where(s => s.Code.ToLower().Equals(Code.ToLower()))
                .Select(s => new StudentEvaluationDto
                {
                    Student = new StudentDto
                    {
                        ID = s.ID,
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        Code = s.Code
                    },
                    Enrollments = enrollments
                }).FirstOrDefault();

            if (evaluation == null)
                throw new ArgumentNullException("Student evaluation not found");

            if(!enrollments.Any())
                throw new ArgumentNullException("Enrollments not assigned or all courses are already evaluated");

            return evaluation;
        }

        public void ChangeStudentStatusFromCourse(int StudentID, int CourseID)
        {
            var enrollment = repositoryContext.Enrollment
                .Where(c => c.StudentID.Equals(StudentID) && c.CourseID.Equals(CourseID))
                .FirstOrDefault();

            if (enrollment == null)
                throw new NullReferenceException("Enrollment with this credentials not found");

            enrollment.Active = !enrollment.Active;

            var course = repositoryContext.Course
                .FirstOrDefault(c => c.ID.Equals(CourseID));

            if (enrollment.Active)
                course.Capacity -= 1;

            else
                course.Capacity += 1;

            repositoryContext.SaveChanges();
        }
    }
}
