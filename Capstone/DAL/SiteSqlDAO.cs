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

        protected int CampgroundChoice { get; set; }

        public SiteSqlDAO(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IList<Site> GetAvailableSites(int campgroundChoice, DateTime arrivalDateChoice, DateTime departureDateChoice)
        {
            this.CampgroundChoice = campgroundChoice;
            IList<Site> returnSites = new List<Site>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("Select TOP 5 site.site_id, max_occupancy, accessible, max_rv_length, utilities " +
                        "FROM site " +
                        "WHERE campground_id = @campgroundChoice AND site.site_id not IN(SELECT site.site_id from reservation r " +
                        "JOIN site ON r.site_id = site.site_id Where to_date > @arrivalDateChoice AND from_date < @departureDateChoice AND site.campground_id = @campgroundChoice)", conn);
                    cmd.Parameters.AddWithValue("@campgroundChoice", campgroundChoice);
                    cmd.Parameters.AddWithValue("@arrivalDateChoice", arrivalDateChoice);
                    cmd.Parameters.AddWithValue("@departureDateChoice", departureDateChoice);

                    SqlDataReader reader = cmd.ExecuteReader();

                    //Creates list of all reservations with dates and siteIds
                    while (reader.Read())
                    {
                        Site site = ConvertReadertoSite(reader);
                        returnSites.Add(site);
                    }
                    reader.Close();
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

        public decimal GetSiteFee()
        {
            decimal siteFee = 0;

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT campground.daily_fee FROM campground " +
                            "JOIN site ON campground.campground_id = site.campground_id " +
                            "WHERE campground.campground_id = @campgroundChoice;", conn);
                cmd.Parameters.AddWithValue("@campgroundChoice", CampgroundChoice);

                siteFee = Convert.ToDecimal(cmd.ExecuteScalar());
            }
            return siteFee;
        }
    }
}
