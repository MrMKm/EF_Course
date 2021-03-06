using Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Enrollment
    {
        public int ID { get; set; }

        public int CourseID { get; set; }

        public int StudentID { get; set; }

        public Grade Grade { get; set; }

        public bool Active { get; set; }

        public virtual Course course { get; set; }

        public virtual Student student { get; set; }


        public Enrollment() { }

        public Enrollment(int StudentID, int CourseID)
        {
            this.StudentID = StudentID;
            this.CourseID = CourseID;
            this.Grade = Grade.NA;
            this.Active = true;
        }
    }
}
