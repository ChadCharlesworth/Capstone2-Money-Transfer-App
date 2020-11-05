using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Transactions;

namespace TenmoServerTests
{
    [TestClass]
    public class TestDatabaseBuild
    {
        

        private TransactionScope transaction;
        protected string ConnectionString { get; } = "Server=.\\SQLEXPRESS;Database=tenmo;Trusted_Connection=True;";
        [TestInitialize]
        public void SetUp()
        {
            transaction = new TransactionScope();

            string sql = File.ReadAllText("testgoeshere.sql");

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sql, conn);
                SqlDataReader reader = command.ExecuteReader();

            }
        }
        [TestCleanup]
        public void Cleanup()
        {
            transaction.Dispose();
        }
    }
}
