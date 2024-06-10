using TVBot.Model.Entities;
using TVBot.Repository.SqlServer;

namespace TVBot.Services.SqlServer
{
    public class TradeOpportunityService : ITradeOpportunityService
    {
        private readonly ISQLServer<TradeOpportunity> _repository;
        public TradeOpportunityService(ISQLServer<TradeOpportunity> repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<TradeOpportunity>> GetAllTradeOpportunities()
        {
            return await _repository.GetAll();
        }

        public async Task<TradeOpportunity> GetTradeOpportunityById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task AddTradeOpportunity(TradeOpportunity tradeOpportunity)
        {
            await _repository.Add(tradeOpportunity);
        }

        public void UpdateTradeOpportunity(TradeOpportunity tradeOpportunity)
        {
            _repository.Update(tradeOpportunity);
        }

        public void DeleteTradeOpportunity(TradeOpportunity tradeOpportunity)
        {
            _repository.Delete(tradeOpportunity);
        }
    }
}
