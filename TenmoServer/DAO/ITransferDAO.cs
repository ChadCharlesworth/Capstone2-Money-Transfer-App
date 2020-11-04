using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    interface ITransferDAO
    {
        IList<Transfer> GetTransfers(int userID);

        Transfer ViewTransferDetails(int transferID, string from, string to, string type, string status, decimal amount);

        Transfer SendTransfer(int userID, string name, decimal amount); 

    }
}
