using TVBot.Model.Entities;

namespace TVBot.Services.SqlServer
{
    public interface ITradeOpportunityService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllTradeOpportunities();
        Task<T> GetTradeOpportunityById(int id);
        Task AddTradeOpportunity(T tradeOpportunity);
        void UpdateTradeOpportunity(T tradeOpportunity);
        void DeleteTradeOpportunity(T tradeOpportunity);
    }
}
