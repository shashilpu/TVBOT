using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;


namespace TVBot.Model.Entities
{
    [Table(name:nameof(TradeOpportunity))]
    public class TradeOpportunity
    {
        [Key]       
        public int Id { get; set; }
        /// <summary>
        /// The date and time (UTC) the request was added to the db.
        /// </summary>
        public DateTime CrossOverDateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// Organization Identifier
        /// </summary>
        public string Ticker { get; set; }
        /// <summary>
        /// Store Location Identifier
        /// </summary>
        public decimal PercentChange { get; set; }
        /// <summary>
        /// Correlation identifier
        /// </summary>
        public decimal? Price { get; set; }
        public decimal? BetaOneYear { get; set; }
        public decimal? PercentVolalityOneWeek { get; set; }
        /// <summary>
        /// File content type
        /// </summary>
        public string AlgoName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal Volume { get; set; }
        public String CrossOverType { get; set; } 
    }
}