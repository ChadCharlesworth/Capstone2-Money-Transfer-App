using System.Collections.Generic;
using TenmoServer.Models; 

namespace TenmoServer.DAO
{
   public  interface IAccountDAO
    {
        Account GetAccount(int userID);

        decimal GetBalance(int userID);

        bool SubtractBalance(Account account); 
        decimal UpdateBalance(int user_id,decimal amountChanged);

        bool CreateAccount(Account account);
        bool UpdateBalance(Account account); 
    }
}
