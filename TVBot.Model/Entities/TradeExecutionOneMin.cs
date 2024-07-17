using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TVBot.Model.Entities
{
    [Table(name: nameof(TradeExecutionOneMin))]
    public class TradeExecutionOneMin
    {
        [Key]
        public int TradeExecutionOneMinId { get; set; }

        [ForeignKey("TradeOpportunityOneMin")]
        public int TradeOpportunityOneMinId { get; set; }
        public TradeOpportunityOneMin TradeOpportunityOneMin { get; set; }

        public DateTime ExecutionDateTime { get; set; } = DateTime.Now;
        public DateTime? TradeCloseDateTime { get; set; } = null;
        public decimal ExecutionPrice { get; set; }
        public decimal Quantity { get; set; }
        public bool InTrade { get; set; }
        public string TradeType { get; set; }
        public string? Status { get; set; }
        public decimal? ProfitLoss { get; set; }
        public decimal? PercentProfitLoss { get; set; }
        public decimal TrargetPrice { get; set; }
        public decimal? TradeClosePrice { get; set; }
        public decimal TargetPercentGain { get; set; }
        public string Ticker { get; set; }
        public decimal? ExecutionFee { get; set; }
        public string? Notes { get; set; }
    }
}
