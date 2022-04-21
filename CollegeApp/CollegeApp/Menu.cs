using CollegeApp.Services.Implementations;
using Shared;
using Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeApp
{
    public class Menu
    {
        private readonly IStudentRepository _studentRepository;

        public Menu()
        {
            _studentRepository = new StudentRepository();
        }

        public bool Show()
        {
            Console.Clear();
            Console.WriteLine($"Choose an option \n" +
                $"1. Register student \n" +
                $"2. Register course \n" +
                $"3. Assign course \n" +
                $"4. Evaluate student performance \n" +
                $"5. Consult student performance \n" +
                $"X. Any other key for exit \n\n");

            switch(Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    RegisterStudent();
                    break;

                case "2":
                    Console.Clear();
                    RegisterCourse();
                    break;

                default:
                    return false;
            }

            return true;
        }

        private void RegisterStudent()
        {
            var studentRegister = new StudentRegisterDto();

            Console.WriteLine("Code: ");
            studentRegister.Code = Console.ReadLine();
            Console.WriteLine("First name: ");
            studentRegister.FirstName = Console.ReadLine(); 
            Console.WriteLine("Last name: ");
            studentRegister.LastName = Console.ReadLine();

            try
            {
                Validation.ObjectValidator(studentRegister);

                _studentRepository.RegisterStudent(studentRegister);

                Console.WriteLine();
                Console.WriteLine("Student registered successfully");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();
            }

        }

        private void RegisterCourse()
        {

        }
    }
}
