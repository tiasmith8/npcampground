using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Transactions;
using Capstone.Tests;

namespace Capstone.Tests
{
    [TestClass]
    public class CapstoneDAOTests
    {

        protected string ConnectionString { get; } = "Server=.\\SQLEXPRESS;Database=npcampground;Trusted_Connection=True;";

        /// <summary>
        /// Holds the generated reservation id.
        /// </summary>
        protected int NewReservationId { get; private set; }

        protected int NewParkId { get; private set; }

        /// <summary>
        /// The transaction for each test.
        /// </summary>
        private TransactionScope transaction;

        [TestInitialize]
        public void Setup()
        {
            // Begin the transaction
            transaction = new TransactionScope();

            // Run the SQL script
            string sql = File.ReadAllText("test-script.sql");

            // Execute the script
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                // If there is a row to read
                while (reader.Read())
                {
<<<<<<< HEAD
                    this.NewParkId = Convert.ToInt32(reader["parkId"]);
                    this.NewReservationId = Convert.ToInt32(reader["reservationId"]);
                    
=======
                    this.NewReservationId = Convert.ToInt32(reader["reservationId"]);
                    this.NewParkId = Convert.ToInt32(reader["parkId"]);
>>>>>>> c06a20bfd55c8dd682131022674cc2b596048685
                }
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Roll back the transaction
            transaction.Dispose();
        }

        /// <summary>
        /// Gets the row count for a table.
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        protected int GetRowCount(string table)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) FROM {table}", conn);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count;
            }
        }
    }
}
