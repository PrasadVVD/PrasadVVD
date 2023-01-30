using System;

namespace SmartFinance.Models
{
    public class BalanceTransferVM
    {
        public int Id { get; set; }
        public int FromRecord { get; set; }
        public int ToRecord { get; set; }
        public DateTime TransferDate { get; set; }
        public decimal Amount { get; set; }
        public int LineId { get; set; }
        public int UserId { get; set; }
        public string FromDay { get; set; }
        public string ToDay { get; set; }
        public string Comments { get; set; }
    }
}
