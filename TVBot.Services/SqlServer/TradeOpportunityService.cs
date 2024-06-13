using TVBot.Model.Entities;
using TVBot.Repository.SqlServer;

namespace TVBot.Services.SqlServer
{
    public class TradeOpportunityService<T> : ITradeOpportunityService<T> where T:class
    {
        private readonly ISQLServer<T> _repository;
        public TradeOpportunityService(ISQLServer<T> repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<T>> GetAllTradeOpportunities()
        {
            return await _repository.GetAll();
        }

        public async Task<T> GetTradeOpportunityById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task AddTradeOpportunity(T tradeOpportunity)
        {
            await _repository.Add(tradeOpportunity);
        }

        public void UpdateTradeOpportunity(T tradeOpportunity)
        {
            _repository.Update(tradeOpportunity);
        }

        public void DeleteTradeOpportunity(T tradeOpportunity)
        {
            _repository.Delete(tradeOpportunity);
        }
    }
}
