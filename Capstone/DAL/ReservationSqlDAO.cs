using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class ReservationSqlDAO : IReservationSqlDAO
    {
        private string ConnectionString { get; }

        public ReservationSqlDAO(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public int CreateReservation(int siteId, string name, DateTime from_date, DateTime to_date, DateTime now)
        {
            int reservationID = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO reservation VALUES (@siteId,@name,@from_date,@to_date,@DateTime)", conn);
                    cmd.Parameters.AddWithValue("@siteId",siteId);
                    cmd.Parameters.AddWithValue("@name",name);
                    cmd.Parameters.AddWithValue("@from_date", from_date);
                    cmd.Parameters.AddWithValue("@to_date", to_date);
                    cmd.Parameters.AddWithValue("@DateTime", now);

                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("SELECT MAX(reservation_id) From reservation", conn);
                    reservationID = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Could not create reservation");
                Console.WriteLine(ex.Message);
                throw;
            }
            return reservationID;
        }
    }
}
