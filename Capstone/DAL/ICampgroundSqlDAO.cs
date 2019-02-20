using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    public interface ICampgroundSqlDAO
    {
        /// <summary>
        /// Return all campgrounds at park
        /// </summary>
        /// <param name="parkId"></param>
        /// <returns></returns>
        IList<Campground> GetAllCampgrounds(int parkId);
    }
}
