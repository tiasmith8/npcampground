using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class MainMenu
    {
        private string ConnectionString { get;}

        public MainMenu(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("Select from parks to view details");

            }
        }
    }
}
