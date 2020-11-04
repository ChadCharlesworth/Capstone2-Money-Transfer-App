using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class AccountDAO : IAccountDAO
    {
        private readonly string connectionString;

        public decimal GetBalance(int userID)
        {
            decimal balance = 0;
            try
            {
                IList<Account> output = GetAccounts(userID);
                foreach (Account account in output)
                {
                    balance += account.Balance;
                }

            }
            catch (Exception)
            {

                throw;
            }
            return balance;
        }

        public IList<Account> GetAccounts(int userID)
        {
            List<Account> output = new List<Account>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlText = "select * from accounts where user_id = @userid";
                    SqlCommand cmd = new SqlCommand(sqlText, conn);
                    cmd.Parameters.AddWithValue("@userid", userID);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Account a = GetAccountFromReader(reader);
                        output.Add(a);
                    }

                }

            }
            catch (Exception e)
            {

                throw;
            }
            return output;
        }

        private Account GetAccountFromReader(SqlDataReader reader)
        {
            Account account = new Account();
            account.AccountId = Convert.ToInt32(reader["account_id"]);
            account.UserId = Convert.ToInt32(reader["user_id"]);
            account.Balance = Convert.ToDecimal(reader["balance"]);
            return account;
        }
    }
}
