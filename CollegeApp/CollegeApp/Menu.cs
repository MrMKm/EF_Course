using CollegeApp.Services.Implementations;
using CollegeApp.Services.Interfaces;
using Shared;
using Shared.Dto;
using Shared.Enum;
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
        private readonly ICourseRepository _courseRepository;

        public Menu()
        {
            _studentRepository = new StudentRepository();
            _courseRepository = new CourseRepository();
        }

        public bool Show()
        {
            Console.Clear();
            Console.WriteLine($"College App \n\n" +
                $"1. Register student \n" +
                $"2. Register course \n" +
                $"3. Assign course \n" +
                $"4. Evaluate student performance \n" +
                $"5. Consult student performance \n" +
                $"6. Edit course \n" +
                $"7. Change student inscription status from course \n" +
                $"8. Delete Course \n\n" +
                $"X. Any other key for exit \n\n" +
                $"Choose an option: ");

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

                case "3":
                    Console.Clear();
                    AssignCourse();
                    break;

                case "4":
                    Console.Clear();
                    EvaluateStudentPerformance();
                    break;

                case "5":
                    Console.Clear();
                    ConsultStudentPerformance();
                    break;

                case "6":
                    Console.Clear();
                    UpdateCourse();
                    break;

                case "7":
                    Console.Clear();
                    ChangeStudentStatusFromCourse();
                    break;

                case "8":
                    Console.Clear();
                    DeleteCourse();
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
            var CourseRegister = new CourseRegisterDto();

            try
            {
                Console.WriteLine("Title: ");
                CourseRegister.Title = Console.ReadLine();

                Console.WriteLine("Credits: ");
                if (!Int32.TryParse(Console.ReadLine(), out int credits))
                    throw new FormatException("Invalid credits format");

                Console.WriteLine("Capacity: ");
                if (!Int32.TryParse(Console.ReadLine(), out int capacity))
                    throw new FormatException("Invalid capacity format");

                CourseRegister.Credits = credits;
                CourseRegister.Capacity = capacity;

                Validation.ObjectValidator(CourseRegister);

                _courseRepository.RegisterCourse(CourseRegister);

                Console.WriteLine();
                Console.WriteLine("Course registered successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();
            }
        }

        private void AssignCourse()
        {
            var courseAssignment = new CourseAssignDto();

            Console.WriteLine("Student Code: ");
            courseAssignment.StudentCode = Console.ReadLine();

            try
            {
                var student = _studentRepository.GetStudentByCode(courseAssignment.StudentCode);
                var courses = _courseRepository.GetAvailableCourses(student.ID);

                Console.WriteLine();
                Console.WriteLine($"\nStudent Information \n" +
                    $"Code: {student.Code} \t Name: {student.FirstName} {student.LastName} \n\n");

                Console.WriteLine("Available courses;");
                foreach(var course in courses)
                {
                    Console.WriteLine($"\nID: {course.ID} " +
                        $"\t Capacity: {course.Capacity}  " +
                        $"\t Credits: {course.Credits}  " +
                        $"\t Title: {course.Title}");
                }

                Console.WriteLine("\nCourse ID:");
                if (!Int32.TryParse(Console.ReadLine(), out int courseID))
                    throw new FormatException("Invalid ID");

                else
                    courseAssignment.CourseID = courseID;

                Validation.ObjectValidator(courseAssignment);

                _studentRepository.AssignCourse(courseAssignment);

                Console.WriteLine();
                Console.WriteLine("Course assigned successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();
            }
        }

        private void UpdateCourse()
        {
            try
            {
                var courses = _courseRepository.GetAllCourses();

                Console.WriteLine("Courses information");
                foreach (var course in courses)
                {
                    Console.WriteLine($"\nID: {course.ID} " +
                        $"\t Capacity: {course.Capacity}  " +
                        $"\t Credits: {course.Credits}  " +
                        $"\t Title: {course.Title}");
                }

                Console.WriteLine("\nCourse ID:");
                if (!Int32.TryParse(Console.ReadLine(), out int courseID))
                    throw new FormatException("Invalid ID");

                var courseDto = new CourseDto();
                courseDto.ID = courseID;

                Console.WriteLine("\nNew title: ");
                courseDto.Title = Console.ReadLine();

                Console.WriteLine("\nNew credits quantity: ");
                if (!Int32.TryParse(Console.ReadLine(), out int credits))
                    throw new FormatException("Invalid credits format");

                courseDto.Credits = credits;

                Console.WriteLine("\nNew capacity: ");
                if (!Int32.TryParse(Console.ReadLine(), out int capacity))
                    throw new FormatException("Invalid capacity format");

                courseDto.Capacity = capacity;

                Validation.ObjectValidator(courseDto);

                _courseRepository.UpdateCourse(courseDto);

                Console.WriteLine("\nCourse updated successfully\n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();
            }
        }

        private void EvaluateStudentPerformance()
        {
            try
            {
                do
                {
                    Console.Clear();
                    var student = ConsultStudentPerformance();

                    Console.WriteLine($"\n\nGrades: {Grade.A} - {Grade.B} - {Grade.C} - {Grade.D} - {Grade.F} \n\n");

                    Console.WriteLine($"Course ID: ");
                    if (!Int32.TryParse(Console.ReadLine(), out int courseID))
                        throw new FormatException("Invalid ID");

                    Console.WriteLine($"New grade: ");
                    if (!Enum.TryParse(Console.ReadLine().ToUpper(), out Grade newGrade))
                        throw new FormatException("Invalid grade");

                    var enrollmentDto = new EnrollmentDto(student.ID, courseID, newGrade);

                    _studentRepository.EvaluateStudent(enrollmentDto);

                    Console.WriteLine("\n\n");
                    Console.Clear();
                    Console.WriteLine("Student's grade updated... \n" +
                        "Still evaluating? write 'yes':");

                    if (Console.ReadLine() != "yes")
                        break;

                } while (true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();
            }
        }

        private StudentDto ConsultStudentPerformance()
        {
            try
            {
                Console.WriteLine("Student Code: ");
                var student = _studentRepository.GetStudentByCode(Console.ReadLine());

                var enrollments = _studentRepository.GetEnrollmentsByStudent(student.ID);

                Console.WriteLine();
                Console.WriteLine($"\nStudent Information \n" +
                    $"Code: {student.Code} \t Name: {student.FirstName} {student.LastName} \n\n");

                Console.WriteLine($"Courses Information");
                foreach (var enrollment in enrollments)
                {
                    Console.WriteLine($"ID: {enrollment.CourseID} " +
                        $"\t Grade: {enrollment.Grade} " +
                        $"\t Credits: {enrollment.course.Credits} " +
                        $"\t Title: {enrollment.course.Title} " +
                        $"\nInscription Status: {enrollment.Active} \n");
                }

                return student;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("\n\nPress any key to continue...");
                Console.ReadLine();
            }

            return null;
        }

        private void ChangeStudentStatusFromCourse()
        {
            try
            {
                Console.Clear();
                var student = ConsultStudentPerformance();

                Console.WriteLine("\nCourse ID:");
                if (!Int32.TryParse(Console.ReadLine(), out int courseID))
                    throw new FormatException("Invalid ID");

                _studentRepository.ChangeStudentStatusFromCourse(student.ID, courseID);

                Console.WriteLine("\nStudent status from course change successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("\n\nPress any key to continue...");
                Console.ReadLine();
            }
        }

        private void DeleteCourse()
        {
            try
            {
                var courses = _courseRepository.GetAllCourses();

                Console.WriteLine("Courses information");
                foreach (var course in courses)
                {
                    Console.WriteLine($"\nID: {course.ID} " +
                        $"\t Capacity: {course.Capacity}  " +
                        $"\t Credits: {course.Credits}  " +
                        $"\t Title: {course.Title}");
                }

                Console.WriteLine("\nCourse ID:");
                if (!Int32.TryParse(Console.ReadLine(), out int courseID))
                    throw new FormatException("Invalid ID");

                var dbCourse = _courseRepository.GetCourseByID(courseID);

                _courseRepository.DeleteCourse(dbCourse.ID);

                Console.WriteLine("\nCourse deleted succesfully");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();
            }
        }

        //private StudentEvaluationDto ConsultStudentPerformance2()
        //{
        //    try
        //    {
        //        Console.Clear();
        //        Console.WriteLine("Student Code: ");
        //        var evaluation = _studentRepository.GetEvaluationByCode(Console.ReadLine());

        //        Console.WriteLine();
        //        Console.WriteLine($"\nStudent Information \n" +
        //            $"Code: {evaluation.Student.Code} " +
        //            $"\t Name: {evaluation.Student.FirstName} {evaluation.Student.LastName} \n\n");

        //        var counter = 1;
        //        Console.WriteLine($"Courses Information");
        //        foreach (var enrollment in evaluation.Enrollments)
        //        {
        //            Console.WriteLine($"{counter}. " +
        //                $"\t Grade: {enrollment.Grade} " +
        //                $"\t Title: {enrollment.Title}");
        //            counter++;
        //        }

        //        return evaluation;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //    finally
        //    {
        //        Console.WriteLine("Press any key to continue...");
        //        Console.ReadLine();
        //    }

        //    return null;
        //}
    }
}
