using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_EATM.Models
{
    public class TransactionHistory
    {
        public int Id { get; set; }
        public int accountNo { get; set; }
        public int withdrawlAmount { get; set; }
        public DateTime transactionTime { get; set; }
    }
}