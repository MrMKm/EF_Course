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
        public List<SubDepartment> subDepartments { get; set; }
        public List<Product> Products { get; set; }


        public Department() { }

        public Department(int ID, string Name, List<SubDepartment> subDepartments, List<Product> products)
        {
            this.ID = ID;
            this.Name = Name;
            this.subDepartments = subDepartments;
            this.Products = products;
        }
        public override string ToString()
        {
            return $"Department Information; \n\n" +
                $"ID: {this.ID} \n" +
                $"Name: {this.Name} \n";
        }
    }
}
