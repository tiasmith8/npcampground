using Capstone.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;

namespace Capstone.Tests
{
    [TestClass]
    public class ParkSqlDAOTests : CapstoneDAOTests
    {

        [TestMethod]
        public void GetAllParksReturnsOnePark()
        {
            //Arrange
            ParkSqlDAO parkDao = new ParkSqlDAO(ConnectionString);
            IList<Park> parks = new List<Park>();

            //Act - Get a list of parks that should be 1 based on test data
            parks = parkDao.GetAllParks();

            //Assert
            Assert.AreEqual(1, parks.Count);
        }

        [TestMethod]
        public void GetAllParkInfo_Returns_OnePark()
        {
            //Arrange - create park data access object
            ParkSqlDAO parkDao = new ParkSqlDAO(ConnectionString);
            Park park = new Park();

            //Act - Get the park created in test-script
            park = parkDao.GetParkInfo(NewParkId);

            //Assert - check fake name matches return name
            Assert.AreEqual("Tia Noah Land", park.Name);
        }
    }
}
