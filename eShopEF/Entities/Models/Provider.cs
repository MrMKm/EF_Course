using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Provider
    {
        public int ID { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Phone is required")]
        [Phone]
        public string Phone { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "State is required")]
        public string State { get; set; }



        public Provider() { }

        public Provider(string Name, string Address, string Phone, string Email, string State)
        { 
            this.Name = Name;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.State = State;
        }

        public override string ToString()
        {
            return $"Provider Information; \n\n" +
                $"ID: {this.ID} \n" +
                $"Name: {this.Name} \n" +
                $"Address: {this.Address} \n" +
                $"Phone: {this.Phone} \n" +
                $"Email: {this.Email} \n" +
                $"State: {this.State} \n";
        }
    }
}
