using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    public interface ISiteSqlDAO
    {
        IList<Site> GetAvailableSites(int campgroundID, DateTime arrivalDateChoice, DateTime departureDateChoice);

        IList<Reservation> GetReservationsNext30Days(int campgroundChoice);

        decimal GetSiteFee();
    }
}
