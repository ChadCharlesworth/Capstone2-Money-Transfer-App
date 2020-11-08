using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace TenmoClient.Data
{
    public class TransferData
    {
        public int TransferId { get; set; }
        public int AccountFrom { get; set; }
        public int AccountTo { get; set; }
        public decimal Amount { get; set; }
        public int TransferTypeId { get; set; } = 2;
        public int TransferStatusId { get; set; } = 2;
    }
}
