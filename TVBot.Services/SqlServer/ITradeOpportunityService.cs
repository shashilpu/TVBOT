using TVBot.Model.Entities;

namespace TVBot.Services.SqlServer
{
    public interface ITradeOpportunityService
    {
        Task<IEnumerable<TradeOpportunity>> GetAllTradeOpportunities();
        Task<TradeOpportunity> GetTradeOpportunityById(int id);
        Task AddTradeOpportunity(TradeOpportunity tradeOpportunity);
        void UpdateTradeOpportunity(TradeOpportunity tradeOpportunity);
        void DeleteTradeOpportunity(TradeOpportunity tradeOpportunity);
    }
}
