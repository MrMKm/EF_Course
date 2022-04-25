using Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto
{
    public class EnrollmentDto
    {

        public int ID { get; set; }

        public int CourseID { get; set; }

        public int StudentID { get; set; }

        public Grade Grade { get; set; }

        public bool Active { get; set; }

        public EnrollmentDto(int StudentID, int CourseID, Grade grade)
        {
            this.StudentID = StudentID;
            this.CourseID = CourseID;
            this.Grade = grade;
        }
    }
}
