using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TenmoClient.Data
{
    public class UserAccount
    {
        public int AccountId { get; set; }
        public int UserID { get; set; }
        [Range(0,double.MaxValue)]
        public decimal Balance { get; set; }
    }
}
