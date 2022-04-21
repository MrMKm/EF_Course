using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class RepositoryContextSeed
    {
        public static async Task SeedAsync(RepositoryContext repositoryContext)
        {
            if(!repositoryContext.Student.Any())
            {
                var seedStudents = new List<Student>
                {
                    new Student {Code = "0123456789", FirstName = "Ricardo", LastName = "Orozco"},
                    new Student {Code = "1234567890", FirstName = "Jair", LastName = "Alvarez"}
                };

                foreach(var student in seedStudents)
                {
                    repositoryContext.Add(student);
                }

                repositoryContext.SaveChanges();
            }

            if (!repositoryContext.Course.Any())
            {
                var seedCourses = new List<Course>
                {
                    new Course {Title = "Spanish", Credits = 4},
                    new Course {Title = "English", Credits = 5}
                };

                foreach (var course in seedCourses)
                {
                    repositoryContext.Add(course);
                }

                repositoryContext.SaveChanges();
            }
        }
    }
}
