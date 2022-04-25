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
        [Required]
        public int ID { get; private set; }

        [Required]
        [MinLength(1, ErrorMessage = "Name is required")]
        public string Name { get; private set; }

        [Required]
        [MinLength(1, ErrorMessage = "Address is required")]
        public string Address { get; private set; }

        [Required]
        [MinLength(1, ErrorMessage = "Phone is required")]
        [Phone]
        public string Phone { get; private set; }

        [Required]
        [MinLength(1, ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; private set; }

        [Required]
        [MinLength(1, ErrorMessage = "State is required")]
        public string State { get; private set; }



        public Provider() { }

        public Provider(int ID, string Name, string Address, string Phone, string Email, string State)
        {
            if (ID < 1)
                throw new FormatException("Invalid ID");

            this.ID = ID;
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
