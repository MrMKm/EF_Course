using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class SubDepartment
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Sub department name is required")]
        [MinLength(1, ErrorMessage = "Sub department name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Department ID is required")]
        public int DepartmentID { get; set; }

        public Department department { get; set; }


        public SubDepartment() { }

        public SubDepartment(string Name, int DepartmentID)
        {
            this.ID = ID;
            this.Name = Name;
            this.DepartmentID = DepartmentID;
        }

        public override string ToString()
        {
            return $"\tSub department Information; \n\n" +
                $"\tID: {this.ID} \n" +
                $"\tName: {this.Name} \n" +
                $"\tDepartment ID: {this.DepartmentID} \n";
        }

        public void SetDepartment(Department department)
        {
            this.department = department;
        }
    }
}
