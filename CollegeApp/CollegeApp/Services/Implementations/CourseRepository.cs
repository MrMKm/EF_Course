using CollegeApp.Services.Interfaces;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeApp.Services.Implementations
{
    public class CourseRepository : ICourseRepository
    {
        private readonly RepositoryContext repositoryContext = new RepositoryContext();

        public void DeleteCourse(int CourseID)
        {
            var course = repositoryContext.Course
                .FirstOrDefault(c => c.ID.Equals(CourseID));

            if(course == null)
                throw new NullReferenceException("Course with ID not found");

            var enrollments = repositoryContext.Enrollment
                .Where(e => e.CourseID.Equals(CourseID))
                .ToList();

            try
            {
                repositoryContext.Enrollment.RemoveRange(enrollments);
                repositoryContext.Course.Remove(course);

                repositoryContext.SaveChanges();
            }
            catch(Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public List<CourseDto> GetAllCourses()
        {
            var courses = repositoryContext.Course
                .Select(c => new CourseDto
                {
                    ID = c.ID,
                    Title = c.Title,
                    Credits = c.Credits,
                    Capacity = c.Capacity
                }).ToList();

            if (!courses.Any())
                throw new ApplicationException("Courses not found");

            return courses;
        }

        public List<CourseDto> GetAvailableCourses(int StudentID)
        {
            var courses = repositoryContext.Course
                .Include(c => c.Enrollments)
                .Where(c => c.Enrollments.Select(e => e.StudentID).Contains(StudentID).Equals(false))
                .Select(c => new CourseDto
                {
                    ID = c.ID,
                    Title = c.Title,
                    Credits = c.Credits,
                    Capacity = c.Capacity
                }).ToList();

            if (!courses.Any())
                throw new ApplicationException("Not available courses, try checking inscription status...");

            return courses;
        }

        public CourseDto GetCourseByID(int CourseID)
        {
            var course = repositoryContext.Course
                .Where(c => c.ID.Equals(CourseID))
                .Select(c => new CourseDto
                {
                    ID = c.ID,
                    Title = c.Title,
                    Credits = c.Credits,
                    Capacity = c.Capacity
                }).FirstOrDefault();

            if (course == null)
                throw new NullReferenceException("Course with ID not found");

            return course;
        }

        public List<CourseDto> GetCoursesByStudentID(int StudentID)
        {
            var courses = repositoryContext.Course
                .Include(c => c.Enrollments)
                .Where(c => c.Enrollments.Select(e => e.StudentID).Contains(StudentID))
                .Select(c => new CourseDto
                {
                    ID = c.ID,
                    Title = c.Title,
                    Credits = c.Credits
                }).ToList();

            if (courses == null)
                throw new ArgumentNullException("Courses with Student ID not found");

            return courses;
        }

        public void RegisterCourse(CourseRegisterDto courseDto)
        {
            var course = new Course(courseDto.Title, courseDto.Credits, courseDto.Capacity);

            try
            {
                repositoryContext.Add(course);
                repositoryContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public void UpdateCourse(CourseDto courseDto)
        {
            var course = repositoryContext.Course
                .FirstOrDefault(c => c.ID.Equals(courseDto.ID));

            try
            {
                if (course == null)
                    throw new ArgumentNullException("Course not found");

                course.Title = courseDto.Title;
                course.Credits = courseDto.Credits;
                course.Capacity = courseDto.Capacity;

                repositoryContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }
    }
}
