using Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeApp.Services.Interfaces
{
    public interface ICourseRepository
    {
        public void RegisterCourse(CourseRegisterDto courseDto);
        public List<CourseDto> GetAllCourses();
        public List<CourseDto> GetAvailableCourses(int StudentID);
        public List<CourseDto> GetCoursesByStudentID(int StudentID);
        public void UpdateCourse(CourseDto courseDto);
        public CourseDto GetCourseByID(int CourseID);
        public void DeleteCourse(int CourseID);
    }
}
