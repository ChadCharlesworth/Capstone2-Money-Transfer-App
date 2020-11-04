using System.Collections.Generic;
using TenmoServer.Models; 

namespace TenmoServer.DAO
{
    interface IAccountDAO
    {
        IList<Account> GetAccounts(int userID);

        decimal GetBalance(int userID); 
    }
}
