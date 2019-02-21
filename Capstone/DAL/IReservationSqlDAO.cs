using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL
{
    public interface IReservationSqlDAO
    {
        int CreateReservation(int siteId, string name, DateTime from_date, DateTime to_date, DateTime now);
    }
}
