using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace TenmoClient.Data
{
    public class TransferData
    {
        public int accountFrom { get; set; }
        public int accountTo { get; set; }
        public decimal amount { get; set; }
    }
}
