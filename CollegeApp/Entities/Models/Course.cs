using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Course
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public int Credits { get; set; }

        public int Capacity { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }

        public Course() { }
        public Course(string Title, int Credits, int Capacity) 
        {
            this.Title = Title;
            this.Credits = Credits;
            this.Capacity = Capacity;
        }
    }
}
