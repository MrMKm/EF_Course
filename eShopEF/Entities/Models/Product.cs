using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Product
    {
        public int ID { get; private set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(1, ErrorMessage = "Name is required")]
        public string Name { get; private set; }

        [Required(ErrorMessage = "Stock is required")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; private set; }

        [Required(ErrorMessage = "SKU is required")]
        [MinLength(1, ErrorMessage = "SKU is required")]
        public string SKU { get; private set; }

        [Required(ErrorMessage = "Description is required")]
        [MinLength(1, ErrorMessage = "Description is required")]
        public string Description { get; private set; }

        [Required(ErrorMessage = "Brand is required")]
        [MinLength(1, ErrorMessage = "Brand is required")]
        public string Brand { get; private set; }

        public int subDepartmentID { get; private set; }

        public Department Department { get; private set; }
        public SubDepartment SubDepartment { get; private set; }


        public Product() { }

        public Product(int ID, string Name, decimal Price, string Description, 
            string Brand, string SKU, int Stock, int subDepartmentID) 
        {
            if (Price < 0)
                throw new InvalidOperationException("Price can't be lower than 0");

            if (Stock <= 0)
                throw new InvalidOperationException("Stock can't be lower than 1");

            this.ID = ID;
            this.Name = Name;
            this.Price = Price;
            this.Description = Description;
            this.Brand = Brand;
            this.SKU = SKU;
            this.Stock = Stock;
            this.subDepartmentID = subDepartmentID;
        }

        public void Update(string Name, decimal Price, string Description, string Brand, int Stock)
        {
            this.Name = Name;
            this.Price = Price;
            this.Description = Description;
            this.Brand = Brand;
            this.Stock = Stock;
        }

        public override string ToString()
        {
            return $"Product Information; \n\n" +
                $"ID: {this.ID} \n" +
                $"Name: {this.Name} \n" +
                $"Price: {this.Price} \n" +
                $"Description: {this.Description} \n" +
                $"Brand: {this.Brand} \n" +
                $"SKU: {this.SKU} \n" +
                $"Stock: {this.Stock} \n" +
                $"Sub department ID: {this.subDepartmentID} \n";
        }


        public void SetSubDepartment(SubDepartment subDepartment)
        {
            this.SubDepartment = subDepartment;
        }

        public void AddStock(int Stock)
        {
            this.Stock += Stock;
        }

        public void RemoveStock(int Stock)
        {
            this.Stock -= Stock;
        }

        public int GetAvailableStock()
        {
            return this.Stock;
        }
    }
}
