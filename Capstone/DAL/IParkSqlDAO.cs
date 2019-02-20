using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;
    
namespace Capstone.DAL
{
    public interface IParkSqlDAO
    {
        /// <summary>
        /// Returns a list of all parks
        /// </summary>
        /// <returns></returns>
        IList<Park> GetAllParks();

        /// <summary>
        /// Returns specific park information
        /// </summary>
        /// <param name="parkId"></param>
        /// <returns></returns>
        IList<Park> GetParkInfo(int parkId);
    }
}
