using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Department
    {
        [Required]
        public int ID { get; private set; }

        [Required(ErrorMessage = "Department name is required")]
        [MinLength(1, ErrorMessage = "Name is required")]
        public string Name { get; private set; }
        public virtual ICollection<SubDepartment> subDepartments { get; set; } 

        public Department() { }

        public Department(string Name)
        {
            this.Name = Name;
        }
        public override string ToString()
        {
            return $"Department Information; \n\n" +
                $"ID: {this.ID} \n" +
                $"Name: {this.Name} \n";
        }
    }
}
