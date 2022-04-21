using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto
{
    public class StudentRegisterDto
    {
        [Required(ErrorMessage = "Code is required")]
        [MinLength(1)]
        public string Code { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [MinLength(1)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [MinLength(1)]
        public string LastName { get; set; }
    }
}
