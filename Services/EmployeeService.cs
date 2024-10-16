using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Module07DataAccess.Model;
using System.Data;

namespace Module07DataAccess.Services
{
    public class EmployeeService
    {
        private readonly string _connectionString;

        public EmployeeService()
        {
            var dbService = new DatabaseConnectionServices();
            _connectionString = dbService.GetConnectionString();
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            var employeeService = new List<Employee>();

            // Correct instantiation of MySqlConnection
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var cmd = new MySqlCommand("SELECT * FROM tblemployee", conn);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        employeeService.Add(new Employee
                        {
                            EmployeeID = reader.GetInt32("EmployeeID"),
                            Name = reader.GetString("Name"),
                            Address = reader.GetString("Address"),
                            email = reader.GetString("email"),
                            ContactNo = reader.GetString("ContactNo"),
                        });
                    }
                }
                return employeeService;
            }
        }
    }
}
