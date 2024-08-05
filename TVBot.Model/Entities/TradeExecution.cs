using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TVBot.Model.Entities
{
    [Table(name: nameof(TradeExecution))]
    public class TradeExecution
    {
        [Key]
        public int TradeExecutionId { get; set; }
        public string Ticker { get; set; }
        public DateTime ExecutionDateTime { get; set; } = DateTime.Now;
        public DateTime? TradeCloseDateTime { get; set; } = null;
        public decimal ExecutionPrice { get; set; }
        public decimal? CurrentPrice { get; set; }
        public decimal? InvestedAmount { get; set; }
        public decimal? CurrentProfitLossOnTrade { get; set; }
        public decimal Quantity { get; set; }
        public decimal TrargetPrice { get; set; }
        public decimal? TradeClosePrice { get; set; }
        public decimal? ProfitLoss { get; set; }
        public decimal? PercentProfitLoss { get; set; }
        public string PastBullCrossInfo { get; set; } = string.Empty;
        public bool InTrade { get; set; }
        public bool IsTradeFromPastBullCross { get; set; }=false;
        public string TradeType { get; set; }
        public string? Status { get; set; }
        public decimal TargetPercentGain { get; set; }
        public decimal? ExecutionFee { get; set; }
        public string? Notes { get; set; }
        public bool IsRepeatedTrade { get; set; }=false;
        [ForeignKey("TradeOpportunity")]
        public int TradeOpportunityId { get; set; }
        public TradeOpportunity TradeOpportunity { get; set; }
    }
}
