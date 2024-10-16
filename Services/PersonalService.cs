using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Module07DataAccess.Model;
using System.Data;

namespace Module07DataAccess.Services
{
    public class PersonalService
    {
        private readonly string _connectionString;

        public PersonalService()
        {
            var dbService = new DatabaseConnectionServices();
            _connectionString = dbService.GetConnectionString();
        }


        //Retrieves all data
        public async Task<List<Personal>> GetAllPersonalsAsync()
        {
            var personalService = new List<Personal>();

            // Correct instantiation of MySqlConnection
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var cmd = new MySqlCommand("SELECT * FROM tblpersonal", conn);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        personalService.Add(new Personal
                        {
                            ID = reader.GetInt32("ID"),
                            Name = reader.GetString("Name"),
                            Gender = reader.GetString("Gender"),
                            ContactNo = reader.GetString("ContactNo"),
                        });
                    }
                }
                return personalService;
            }
        }
    }
}
