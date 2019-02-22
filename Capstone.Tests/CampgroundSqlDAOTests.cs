using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Capstone.DAL;
using Capstone.Models;

namespace Capstone.Tests
{
    [TestClass]
    public class CampgroundSqlDAOTests : CapstoneDAOTests
    {

        [TestMethod]
        public void GetAllCampground_ReturnsNumberOfCampgrounds()
        {
            CampgroundSqlDAO dao = new CampgroundSqlDAO(ConnectionString);
            IList<Campground> campgrounds = dao.GetAllCampgrounds(NewParkId);
            Assert.AreEqual(1, campgrounds.Count);
        }
    }
}
