using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class CampgroundSqlDAO : ICampgroundSqlDAO
    { 
        private string ConnectionString { get; }

        public CampgroundSqlDAO(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
    
        public IList<Campground> GetAllCampgrounds(int parkId)
        {
            List<Campground> campgrounds = new List<Campground>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM campground WHERE park_id = @parkID", conn);
                    cmd.Parameters.AddWithValue("@parkID", parkId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Campground campground = ConvertReaderToCampgrounds(reader);
                        campgrounds.Add(campground);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("could not connect to database");
                Console.WriteLine(ex.Message);
                throw;
            }

            return campgrounds;
        }

        private Campground ConvertReaderToCampgrounds(SqlDataReader reader)
        {
            Campground campground = new Campground();
            campground.CampgroundId = Convert.ToInt32(reader["campground_id"]);
            campground.ParkId = Convert.ToInt32(reader["park_id"]);
            campground.Name = Convert.ToString(reader["name"]);
            campground.OpenMonth = Convert.ToInt32(reader["open_from_mm"]);
            campground.ClosedMonth = Convert.ToInt32(reader["open_to_mm"]);
            campground.DailyFee = Convert.ToDecimal(reader["daily_fee"]);

            return campground;

        }
    }
}
