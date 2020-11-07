using System.Collections.Generic;
using TenmoServer.Models; 

namespace TenmoServer.DAO
{
   public  interface IAccountDAO
    {
        IList<Account> GetAccounts(int userID);

        decimal GetBalance(int userID);

        decimal UpdateBalance(int userID, decimal amountChanged); 
    }
}
