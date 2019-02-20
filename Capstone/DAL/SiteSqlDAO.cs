using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL
{
    public class SiteSqlDAO : ISiteSqlDAO
    {
        private string ConnectionString { get; }

        public SiteSqlDAO(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
    }
}
