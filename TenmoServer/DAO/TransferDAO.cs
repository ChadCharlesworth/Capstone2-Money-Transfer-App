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

<<<<<<< HEAD
        

        public bool SendTransfer(int userID,int accountTo, decimal amount)

=======
        public bool SendTransfer(int transferID)
>>>>>>> 11512fdd0d76e75d2570ed11b7bba484335248dc
        {
            Transfer input = GetTransfer(transferID);
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("UPDATE accounts set balance = (balance - @amount) where user_id = @userID");
                    command.Parameters.AddWithValue("@amount", input.Amount);
                    command.Parameters.AddWithValue("@userID", input.AccountFrom);
                    if (input.Amount > 0)
                    {
                        rowsAffected = command.ExecuteNonQuery();
                        command = new SqlCommand("UPDATE accounts set balance = (balance + @amount) where user_id = @accountTo ");
                        command.Parameters.AddWithValue("@accountTo", input.AccountTo);
                        rowsAffected += command.ExecuteNonQuery();
                    }
                    
                    return (rowsAffected > 1);

                }
            }
            catch (Exception)
            {




                throw;
            }
        }

        public bool CreateTransfer(int accountFrom,int accountTo, decimal amount)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("insert into transfers(account_from,account_to,transfer_type_id, transfer_status_id) values(@accountFrom, @accountTo, 2, 2); select scope_identity(); ", conn);
                    command.Parameters.AddWithValue("@accountFrom", accountFrom);
                    command.Parameters.AddWithValue("@accountTo", accountTo);
                    command.Parameters.AddWithValue("@amount", amount);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public Transfer GetTransfer(int transferID)
        {
            Transfer transfer = new Transfer();
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("select * from transfers where transfer_id = @transferID");
                    command.Parameters.AddWithValue("@transferID", transferID);
                    SqlDataReader reader = command.ExecuteReader();
                    while(reader.Read())
                    {
                        transfer = GetTransferFromReader(reader);
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return transfer;
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
