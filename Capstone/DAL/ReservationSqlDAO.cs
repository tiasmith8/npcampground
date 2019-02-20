using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL
{
    public class ReservationSqlDAO : IReservationSqlDAO
    {
        private string ConnectionString { get; }

        public ReservationSqlDAO(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
    }
}
