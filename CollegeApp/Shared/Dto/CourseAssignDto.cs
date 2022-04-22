using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto
{
    public class CourseAssignDto
    {
        [Required(ErrorMessage = "Student Code is required")]
        [MinLength(1, ErrorMessage = "Invalid Student Code")]
        public string StudentCode { get; set; }

        [Required(ErrorMessage = "Course ID is required")]
        public int CourseID { get; set; }
    }
}
