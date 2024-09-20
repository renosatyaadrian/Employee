using Employees.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Employees
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("\n PROJECT - CRUD SQLSERVER\n");
                Console.WriteLine("\n (1) - Insert Employee");
                Console.WriteLine("\n (2) - Update Employee");
                Console.WriteLine("\n (3) - Delete Employee");
                Console.WriteLine("\n (4) - Find All Employee");
                Console.WriteLine("\n (5) - Find Employee By Id");
                Console.WriteLine("\n (0) - Exit");
                Console.WriteLine("Please, inform the desired option ");
                var input = Console.ReadLine();
                var opcao = 0;
                if (input == null || input == string.Empty)
                {

                    Console.WriteLine("\n Input null, please make");
                    Main(args);
                }
                else
                {
                    opcao = int.Parse(input);
                }

                switch (opcao)
                {
                    // Insert New Employee
                    case 1:
                        Console.Clear();
                        var employeeInsert = new Employee();
                        Console.WriteLine("\n Employee Id:");
                        var employeeIdInsert = CheckStringNullOrEmpty(Console.ReadLine());
                        employeeInsert.EmployeeId = employeeIdInsert;

                        var employeeRepositoryFindById = new EmployeeRepository();
                        var tempEmployeeExist = employeeRepositoryFindById.FindById(employeeIdInsert);
                        if (tempEmployeeExist != null) throw new Exception("Employee Exist");


                        Console.WriteLine("\n Employee Name: ");
                        employeeInsert.FullName = CheckStringNullOrEmpty(Console.ReadLine());
                        Console.WriteLine("\n Employee Birth Date: ");
                        employeeInsert.BirthDate = ParseDateTime(Console.ReadLine());

                        var EmployeeRepositoryInsert = new EmployeeRepository();
                        EmployeeRepositoryInsert.Insert(employeeInsert);
                        Console.WriteLine("\n Employee registred successfully ");
                        Console.ReadKey();
                        Console.Clear();

                        break;

                    // Update Existing Employee
                    case 2:
                        Console.Clear();
                        Console.WriteLine("\n Employee Update ");
                        Console.WriteLine("\n Employee Id:  ");
                        var EmployeeIdUpdate = CheckStringNullOrEmpty(Console.ReadLine());
                        var EmployeeRepositoryUpdate = new EmployeeRepository();
                        var EmployeeUpdate = EmployeeRepositoryUpdate.FindById(EmployeeIdUpdate);

                        if (EmployeeUpdate == null) throw new Exception("Employee Not Found");

                        Console.WriteLine("\n Employee Name: ");
                        EmployeeUpdate.FullName = CheckStringNullOrEmpty(Console.ReadLine());
                        Console.WriteLine("\n Employee Birth Date: ");
                        EmployeeUpdate.BirthDate = ParseDateTime(Console.ReadLine());

                        EmployeeRepositoryUpdate.Alter(EmployeeUpdate);
                        Console.WriteLine("\n Employee updated successfully ");
                        Console.ReadKey();
                        Console.Clear();
                        Main(args);

                        break;

                    // Delete Existing Employee
                    case 3:
                        Console.Clear();
                        Console.WriteLine("\n Employee Update ");
                        Console.WriteLine("\n Employee Id:  ");
                        var EmployeeIDDelete = Console.ReadLine();
                        var EmployeeRepositoryDelete = new EmployeeRepository();
                        var EmployeeDelete = EmployeeRepositoryDelete.FindById(CheckStringNullOrEmpty(EmployeeIDDelete));
                        if (EmployeeDelete == null) throw new Exception("Employee Not Found");

                        Console.WriteLine($"\n Do you really want to delete {EmployeeDelete.FullName} ? (Y,N): ");
                        var answer = Console.ReadLine();
                        if (true)
                        {
                            EmployeeRepositoryDelete.Delete(EmployeeDelete);
                            Console.WriteLine("\n Employee deleted successfully ");
                            Console.ReadKey();
                            Console.Clear();
                            Main(args);
                        }
                        break;

                    // Get All Employee
                    case 4:
                        var EmployeeRepositoryFindAll = new EmployeeRepository();
                        Console.Clear();
                        Console.WriteLine("\n Get All Employees");
                        Console.WriteLine("\n");
                        foreach (var item in EmployeeRepositoryFindAll.FindAll())
                        {

                            Console.WriteLine("Employee ID: " + item.EmployeeId);
                            Console.WriteLine("Employee Name: " + item.FullName);
                            Console.WriteLine("Employee BirthDate:" + item.BirthDate.ToString("dd-MM-yyyy"));
                            Console.WriteLine("\n");

                        }
                        Console.ReadKey();
                        Console.Clear();
                        Main(args);
                        break;

                    // Get Employee by Id
                    case 5:
                        var EmployeeRepositoryFindByID = new EmployeeRepository();
                        Console.Clear();
                        Console.WriteLine("\n Employee ID: \n ");
                        var EmployeeIDConsult = Console.ReadLine();
                        var itemById = EmployeeRepositoryFindByID.FindById(EmployeeIDConsult);
                        if (itemById != null)
                        {
                            Console.WriteLine("Employee ID: " + itemById.EmployeeId);
                            Console.WriteLine("Employee Name: " + itemById.FullName);
                            Console.WriteLine("Employee BirthDate:" + itemById.BirthDate);
                            Console.WriteLine("\n");
                            Console.ReadKey();
                            Console.Clear();
                            Main(args);
                        }
                        else
                        {
                            Console.WriteLine("Employee not found!");
                            Console.ReadKey();
                            Console.Clear();
                            Main(args);
                        }
                        break;
                }

            }
            catch (Exception e)
            {

                Console.WriteLine("\n Error: " + e.Message);
            }
            finally
            {
                Main(args);
            }

        }

        // Check String wether is Null / Empty
        private static string CheckStringNullOrEmpty(string? input)
        {
            if (input == null || input == string.Empty) throw new Exception("Empty Input");
            else return input;
        }

        // Check DateTime Format
        private static DateTime ParseDateTime(string? input)
        {
            DateTime parsedDate;

            string format = "dd-MM-yyyy";
            bool isValidDate = DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);

            if (isValidDate)
            {
                return parsedDate;
            }
            else
            {
                throw new Exception("Invalid date format. Please enter the date in the format dd-MM-yyyy.");
            }
        }
    }

}