using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
   public interface ITransferDAO
    {
        IList<Transfer> GetTransfers(int userID);

        Transfer GetTransfer(int transferID);

        bool SendTransfer(int userID, int accountTo, decimal amount);

        Transfer CreateTransfer(int transferID); 

    }
}
