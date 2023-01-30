using Newtonsoft.Json;
using System;

namespace SmartFinance.Models
{
    public class MainAccountVM
    {
        public int Id { get; set; }
        public int LineId { get; set; }
        public int UserId { get; set; }
        public string LineDay { get; set; }
        public DateTime Date { get; set; }
        public decimal OldBalance { get; set; }
        public decimal Investment { get; set; }
        public string Coments { get; set; }
        public decimal Payments { get; set; }
        public decimal Collection { get; set; }
        public decimal Expenses { get; set; }
        public decimal ChittFund { get; set; }
        public decimal Withdraw { get; set; }
        public decimal WeekInterest { get; set; }
        public decimal TotalInterest { get; set; }
        public decimal Total { get; set; }
        [JsonIgnore]
        public bool IsDeleteVisible { get; set; } = false;
        [JsonIgnore]
        public bool IsBalanceTransfer { get; set; } = false;
        [JsonIgnore]
        public decimal BalanceTransferAmount { get; set; }
        [JsonIgnore]
        public string BalanceTransferComments { get; set; }
        [JsonIgnore]
        public bool IsRefundTransfer { get; set; } = false;

    }
}
