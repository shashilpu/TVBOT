using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TVBot.Model.Entities
{
    [Table(name: nameof(TradeExecution))]
    public class TradeExecution
    {
        [Key]
        public int TradeExecutionId { get; set; }

        [ForeignKey("TradeOpportunity")]
        public int TradeOpportunityId { get; set; }
        public TradeOpportunity TradeOpportunity { get; set; }

        public DateTime ExecutionDateTime { get; set; } = DateTime.Now;
        public decimal ExecutionPrice { get; set; }
        public decimal Quantity { get; set; }
        public bool InTrade { get; set; }
        public string TradeType { get; set; }        
        public string? Status { get; set; }
        public decimal? ProfitLoss { get; set; }
        public decimal? ExecutionFee { get; set; }
        public string? Notes { get; set; }        
    }
}
