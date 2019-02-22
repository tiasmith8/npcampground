using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Capstone.DAL;
using Capstone.Models;

namespace Capstone.Tests
{   
    [TestClass]
    public class ReservationSqlDAOTests : CapstoneDAOTests
    {
        [TestMethod]
        public void SeeIfReservationWasCreated_ReturnID()
        {
            ReservationSqlDAO dao = new ReservationSqlDAO(ConnectionString);

            Reservation reservation = new Reservation();
            reservation.Create_date = Convert.ToDateTime("2019/02/25");
            reservation.From_Date = Convert.ToDateTime("2019/03/03");
            reservation.To_Date = Convert.ToDateTime("2019/03/10");
            reservation.Name = "Noah";
            reservation.Site_Id = SiteId;

            int startingRowCount = GetRowCount("reservation");

            int resID = dao.CreateReservation(reservation.Site_Id, reservation.Name, reservation.From_Date, reservation.To_Date, reservation.Create_date);

            int endingRowCount = GetRowCount("reservation");

            Assert.AreEqual(resID, NewReservationId+1);
            Assert.AreNotEqual(startingRowCount, endingRowCount);
        }
    }
}

