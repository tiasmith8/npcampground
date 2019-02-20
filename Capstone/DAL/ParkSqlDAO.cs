using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class ParkSqlDAO : IParkSqlDAO
    {
        public IList<Park> GetAllParks()
        {
            IList<Park> Parks = new List<Park>();

            try
            {
                using ()
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Unable to connect to park database");
                Console.WriteLine(ex.message);
                throw;
            }
        }

        public IList<Park> GetParkInfo(int parkId)
        {
            throw new NotImplementedException();
        }
    }
}
