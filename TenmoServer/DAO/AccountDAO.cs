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
        public AccountDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public Account GetAccount(int userID)
        {
            Account output = new Account();

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
                         output = GetAccountFromReader(reader);
                        return output;
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

        public decimal GetBalance(int user_id)
        {
            Account userAccount = new Account();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlText = "select * from accounts where user_id = @user_id";
                    SqlCommand command = new SqlCommand(sqlText, connection);
                    command.Parameters.AddWithValue("@user_id", user_id);
                   SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                       userAccount  = GetAccountFromReader(reader);
                        
                    }
                    return userAccount.Balance;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateBalance(Account account)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlText = "update accounts set balance = @balance where user_id = @user_id";
                    SqlCommand command = new SqlCommand(sqlText, connection);
                    command.Parameters.AddWithValue("@user_id", account.UserId);
                    command.Parameters.AddWithValue("@balance", account.Balance);
                    return command.ExecuteNonQuery()> 0;
                    
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
