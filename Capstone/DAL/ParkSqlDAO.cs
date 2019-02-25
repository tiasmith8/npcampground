using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class ParkSqlDAO : IParkSqlDAO
    {
        private string ConnectionString { get; }

        public ParkSqlDAO(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        //Open connection to db and return all parks in the park table
        public IList<Park> GetAllParks()
        {
            IList<Park> parks = new List<Park>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM park", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Park park = ConvertReaderToPark(reader);
                        parks.Add(park);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Unable to connect to park database");
                Console.WriteLine(ex.Message);
                throw;
            }
            return parks;
        }

        //Method to convert a row in the park table to a Park object to add to parks list.
        private Park ConvertReaderToPark(SqlDataReader reader)
        {
            Park park = new Park();
            park.ParkId = Convert.ToInt32(reader["park_id"]);
            park.Name = Convert.ToString(reader["name"]);
            park.Location = Convert.ToString(reader["location"]);
            park.EstablishDate = Convert.ToDateTime(reader["establish_date"]);
            park.Area = Convert.ToInt32(reader["area"]);
            park.Visitors = Convert.ToInt32(reader["visitors"]);
            park.Description = Convert.ToString(reader["description"]);

            return park;
        }

        public Park GetParkInfo(int parkId)
        {
            Park park = new Park();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string sql = "SELECT * FROM park WHERE park_id = @parkId;";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@parkId", parkId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        park = ConvertReaderToPark(reader);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error getting connection to dataase");
                Console.WriteLine(ex.Message);
                throw;
            }

            return park;
        }
    }
}
