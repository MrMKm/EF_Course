using CollegeApp.Services.Interfaces;
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
    public class CourseRepository : ICourseRepository
    {
        private readonly RepositoryContext repositoryContext = new RepositoryContext();

        public void RegisterCourse(CourseRegisterDto courseDto)
        {
            var course = new Course(courseDto.Title, courseDto.Credits);

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
    }
}
