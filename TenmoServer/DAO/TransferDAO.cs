using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TenmoServer.Models; 

namespace TenmoServer.DAO
{
    public class TransferDAO : ITransferDAO
    {
        private readonly string connectionString; 
        public TransferDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public IList<Transfer> GetTransfers(int userID)
        {
            try
            {
                IList<Transfer> output = new List<Transfer>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlText = "select * from transfers where account_from in (select account_id from accounts where user_id = @userid) or account_to in (select account_id from accounts where user_id = @userid)";
                    SqlCommand command = new SqlCommand(sqlText, connection);
                    command.Parameters.AddWithValue("@user_id", userID);
                    SqlDataReader reader = command.ExecuteReader(); 
                    while (reader.Read())
                    {
                        output.Add(GetTransferFromReader(reader)); 
                    }
                }
                return output; 
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public bool SendTransfer(int userID, decimal amount)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("UPDATE accounts set balance = (balance + @amount) where user_id = @userID ");
                    command.Parameters.AddWithValue("@amount", amount);
                    if(amount > 0)
                    {
                        rowsAffected = command.ExecuteNonQuery();
                    }

                    return (rowsAffected > 0);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Transfer ViewTransferDetails(int transferID, string from, string to, string type, string status, decimal amount)
        {
            throw new NotImplementedException();
        }

        private Transfer GetTransferFromReader(SqlDataReader reader)
        {
            Transfer transfer = new Transfer();
            transfer.TransferId = Convert.ToInt32(reader["transfer_id"]);
            transfer.TransferTypeId = Convert.ToInt32(reader["transfer_type_id"]);
            transfer.TransferStatusId = Convert.ToInt32(reader["transfer_status_id"]);
            transfer.Amount = Convert.ToDecimal(reader["amount"]);
            transfer.AccountFrom = Convert.ToInt32(reader["account_from"]);
            transfer.AccountTo = Convert.ToInt32(reader["account_to"]); 
            return transfer;
        }
            
    }
}
