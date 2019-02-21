using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;
using System.Data.SqlClient

namespace Capstone.DAL
{
    public class SiteSqlDAO : ISiteSqlDAO
    {
        private string ConnectionString { get; }

        public SiteSqlDAO(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IList<Site> GetAvailableSites(int campgroundChoice, DateTime arrivalDateChoice, DateTime departureDateChoice)
        {
            List<Site> sites = new List<Site>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    List<Reservation> reservations = new List<Reservation>();
                    conn.Open();

                    //SqlCommand cmd = new SqlCommand("Select reservation.site_id, from_date,to_date From site JOIN reservation ON site.site_id = reservation.reservation_id WHERE campground_id = @campgroundChoice", conn);
                    SqlCommand cmd = new SqlCommand("Select reservation.site_id, reservation.from_date, reservation.to_date " +
                        "From reservation JOIN site ON site.site_id = reservation.reservation_id WHERE campground_id = 1", conn);

                    cmd.Parameters.AddWithValue("@campgroundChoice", campgroundChoice);

                    SqlDataReader reader = cmd.ExecuteReader();

                    //Creates list of all reservations with dates and siteIds
                    while (reader.Read())
                    {
                        Reservation reservation = new Reservation()
                        {
                            Site_Id = Convert.ToInt32(reader["site_id"]),
                            From_Date = Convert.ToDateTime(reader["from_date"]),
                            To_Date = Convert.ToDateTime(reader["to_date"])
                        };
                        reservations.Add(reservation);
                    }

                    Stack<int> availableSites = new Stack<int>();

                    //Loop through the reservations list
                    foreach(Reservation reservation in reservations)
                    {
                        //Create a list of ints with sites that are available
                        if (!availableSites.Contains(reservation.Site_Id) && ((arrivalDateChoice < reservation.From_Date 
                            && departureDateChoice < reservation.From_Date) ||(arrivalDateChoice > reservation.To_Date)))
                            {
                                availableSites.Push(reservation.Site_Id); //Add to list of available sites
                            }
                        else if (availableSites.Contains(reservation.Site_Id))
                        {
                            if ((arrivalDateChoice >= reservation.From_Date && arrivalDateChoice <= reservation.To_Date))
                            {
                                availableSites.Pop();
                            }
                        }
                    }

                    //List is created, look at each siteid and see if available
                    for (int i = 1; i < 52; i++)
                    {
                        if(reservations[i].Site_Id == i)
                        {

                        }
                    }


                }
            }

            catch (SqlException ex)
            {
                Console.WriteLine("could not connect to database for site list");
                Console.WriteLine(ex.Message);
                throw;
            }

            return sites;
        }
    }
}
