using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;
using Capstone.DAL;

namespace Capstone.Tests
{
    [TestClass]
    public class SiteSqlDAOTests : CampgroundSqlDAOTests
    {
        [TestMethod]
        public void GetAvailableSites_Returns_One_Site()
        {
            //Arrange
            SiteSqlDAO siteDao = new SiteSqlDAO(ConnectionString);

            //Act - returns one site added in test-script.sql
            IList<Site> sites = siteDao.GetAvailableSites(NewCampgroundId, DateTime.Now.AddDays(2.0), DateTime.Now);

            //Assert
            Assert.AreEqual(1, sites.Count);
        }
    }
}
