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





    }
}
