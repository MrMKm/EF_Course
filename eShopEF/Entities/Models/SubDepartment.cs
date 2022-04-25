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
        [Required]
        public int ID { get; private set; }

        [Required(ErrorMessage = "Sub department name is required")]
        [MinLength(1, ErrorMessage = "Sub department name is required")]
        public string Name { get; private set; }

        [Required(ErrorMessage = "Department ID is required")]
        public int DepartmentID { get; private set; }

        public Department Department { get; private set; }


        public SubDepartment() { }

        public SubDepartment(int ID, string Name, int DepartmentID)
        {
            if (DepartmentID < 1)
                throw new RankException("Invalid Department ID");

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
            this.Department = department;
        }
    }
}
