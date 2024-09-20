using Dapper;
using Microsoft.Data.SqlClient;

namespace Employees.Repositories
{
    public class EmployeeRepository 
    {

        private string _connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public void Alter(Employee obj)
        {
            var query = @"
            UPDATE Employee 
            SET
                EmployeeId = @EmployeeId,
                FullName = @FullName,
                BirthDate = @BirthDate
            WHERE
                EmployeeId = @EmployeeId
            ";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, obj); //executing sql update command
            }
        }

        public void Delete(Employee obj)
        {
            var query = @"
            DELETE FROM Employee
            WHERE
                EmployeeId = @EmployeeId
            ";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, obj); //executing sql delete command
            }
        }

        public void Insert(Employee obj)
        {
            var query = @"
                INSERT INTO Employee(
                    EmployeeId,
                    FullName,
                    BirthDate)
                VALUES(
                    @EmployeeId,
                    @FullName,
                    @BirthDate )
                   ";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, obj); //executing sql insert command
            }
        }
        public List<Employee> FindAll()
        {
            var query = @"
            SELECT * FROM Employee
            ORDER BY EmployeeId
            ";
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Employee>(query).ToList(); //executing sql insert command
            }
        }

        public Employee? FindById(string id)
        {
            var query = @"
            SELECT * FROM Employee
            WHERE EmployeeId = @Id
            ";
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Employee>(query, new { id }).FirstOrDefault(); //executing sql insert command
            }
        }
    }
}