using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto
{
    public class CourseRegisterDto
    {
        [Required(ErrorMessage = "Title is required")]
        [MinLength(1)]
        public string Title { get; set; }

        [Range(1, 20, ErrorMessage = "Out of credits range")]
        public int Credits { get; set; }
    }
}
