using TVBot.Model.Entities;
using TVBot.Services;
using TVBot.Services.Factory;
namespace TVBot.Utility
{
    public static class Status
    {

        public static async Task FetchCurrentPriceAsync(ISQLServerServiceFactory sQLServiceFactory)
        {


            var openTrades = sQLServiceFactory.Create<TradeExecution>().GetAll().Result.ToList<TradeExecution>().Where(x => x.Status == "Open");

            // var result =await  APIServices.GetCurrentPrices(scIdList,scId);
            foreach (var trade in openTrades)
            {
                var result = await APIServices.GetCurrentPrices(trade.MCTicker, trade.MCTicker);
                foreach (var cp in result.data)
                {
                    var lastPrice = decimal.Parse(cp.lastPrice.Replace(",", ""));
                    var percentChangeFromExecutionPrice = (lastPrice - trade.ExecutionPrice) / trade.ExecutionPrice * 100;
                    if (percentChangeFromExecutionPrice >= trade.TargetPercentGain)
                    {
                        var tradeOpportunity = await sQLServiceFactory.Create<TradeOpportunity>().GetById(trade.TradeOpportunityId);
                        if (tradeOpportunity != null)
                        {
                            var algoName = tradeOpportunity.AlgoName;
                            var tickerName = tradeOpportunity.Ticker;
                            var price = lastPrice;
                            var change =Math.Round(decimal.Parse(cp.perChange.Replace(",", "")),3);
                            var marketCap =Math.Round(decimal.Parse(cp.marketCap.Replace(",", "")) / 1000000,2);
                            var companyName = cp.companyName;
                            var performace1Yr = cp.perform1yr;
                            //var targetPrice=trade.ExecutionPrice*1.05m;
                            var targetPercentGain = trade.TargetPercentGain;

                            var Message = "Open Trade Crosses Target Percent: " + targetPercentGain + "--" + algoName + "-- " + tickerName + " P.=" + price + " C.=" + change + "% M.CAP= " + marketCap + " M "
                                + "ExecutionDateTime: " + trade.ExecutionDateTime + " ExecutionPrice: " + trade.ExecutionPrice + " ProfitLoss: " + percentChangeFromExecutionPrice + "%"
                                + " Company Name: " + companyName + " 1Yr Performance: " + performace1Yr;
                            APIServices.SendToTeligrams(Message);


                        }
                    }
                }

            }


        }

    }
}