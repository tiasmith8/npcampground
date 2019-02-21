using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class SiteSqlDAO : ISiteSqlDAO
    {
        private string ConnectionString { get; }

        public SiteSqlDAO(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public List<Site> GetAvailableSites(int campgroundChoice, DateTime arrivalDateChoice, DateTime departureDateChoice)
        {
            Stack<int> availableSites = new Stack<int>();

            List<Site> returnSites = new List<Site>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    List<Reservation> reservations = new List<Reservation>();
                    conn.Open();

                    //SqlCommand cmd = new SqlCommand("Select reservation.site_id, from_date,to_date From site JOIN reservation ON site.site_id = reservation.reservation_id WHERE campground_id = @campgroundChoice", conn);
                    SqlCommand cmd = new SqlCommand("Select reservation.site_id, reservation.from_date, reservation.to_date " +
                        "From reservation JOIN site ON site.site_id = reservation.reservation_id WHERE campground_id = @campgroundChoice", conn);
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
                    reader.Close();

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
                    string stringOfSites = ConvertSitesToString(availableSites);
                    //(SELECT campground.daily_fee From campground WHERE campground_id = @campgroundChoice) AS 'Cost' " 
                    cmd = new SqlCommand ("Select site.site_id, max_occupancy,accessible,max_rv_length,utilities" +
                        "FROM site JOIN reservation ON site.site_id = reservation.reservation_id WHERE campground_id = campgroundChoice AND site.site_id IN(@stringOfSites)", conn);
                    cmd.Parameters.AddWithValue("@campgroundChoice", campgroundChoice);
                    cmd.Parameters.AddWithValue("@stringOfSites", stringOfSites);

                    SqlDataReader siteReader = cmd.ExecuteReader();

                    while (siteReader.Read())
                    {
                        Site site = ConvertReadertoSite(siteReader);
                        returnSites.Add(site);
                    }

                }
            }

            catch (SqlException ex)
            {
                Console.WriteLine("could not connect to database for site list");
                Console.WriteLine(ex.Message);
                throw;
            }

            return returnSites;
        }

        private Site ConvertReadertoSite(SqlDataReader reader)
        {
            Site site = new Site();

            site.Site_id = Convert.ToInt32(reader["site_id"]);
            site.Max_Occupancy = Convert.ToInt32(reader["max_occupancy"]);
            site.Accessible = Convert.ToBoolean(reader["accessible"]);
            site.Max_Rv_Length = Convert.ToInt32(reader["max_rv_length"]);
            site.Utilities = Convert.ToBoolean(reader["utilities"]);

            return site;
        }

        public string ConvertSitesToString(Stack<int> sites)
        {
            string siteString = string.Empty;

            foreach (int site in sites)
            {
                siteString += site;
                siteString += ",";
            }
            return siteString.Substring(0, siteString.Length - 1);
        }
    }
}
