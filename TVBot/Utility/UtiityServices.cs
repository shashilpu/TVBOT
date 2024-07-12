using TVBot.Model.Entities;
using TVBot.Models;
using TVBot.Services;
using TVBot.Services.Factory;

namespace TVBot.Utility
{
    public static class UtiityServices
    {

        static Dictionary<string, decimal> trackedElementsEMA15Minutes = new Dictionary<string, decimal>();

        static Dictionary<string, decimal> trackedElementsEMA30Minutes = new Dictionary<string, decimal>();

        static Dictionary<string, decimal> trackedElementsEMAOneHour = new Dictionary<string, decimal>();

        static Dictionary<string, decimal> trackedElementsEMATwoHour = new Dictionary<string, decimal>();

        static Dictionary<string, decimal> trackedElementsEMAFourHour = new Dictionary<string, decimal>();

        static Dictionary<string, decimal> trackedElementsEMAOneDay = new Dictionary<string, decimal>();

        static Dictionary<string, decimal> trackedElementsMACDOneDay = new Dictionary<string, decimal>();
        static Dictionary<string, decimal> trackedElementsMACDOneHour = new Dictionary<string, decimal>();
        static Dictionary<string, decimal> trackedElementsEMAOneWeek = new Dictionary<string, decimal>();
        static Dictionary<string, decimal> trackedElementsWeekDarvas = new Dictionary<string, decimal>();
        static Dictionary<string, decimal> trackedElementsAllTimeDarvas = new Dictionary<string, decimal>();
        static Dictionary<string, decimal> trackedElementsEMA1Minutes = new Dictionary<string, decimal>();
        static Dictionary<string, decimal> trackedElementsEMA5Minutes = new Dictionary<string, decimal>();



        public static void EMA1MReversal(string ema1MQueryFilePath, ISQLServerServiceFactory tradeOpportunityService)
        {
          //  SearchAndSend(ema1MQueryFilePath, trackedElementsEMA1Minutes, "1M_EMA", tradeOpportunityService);
            OneMin5_9EMACrossOver(ema1MQueryFilePath, tradeOpportunityService);
        }

        public static void EMA5MReversal(string ema5MQueryFilePath, ISQLServerServiceFactory tradeOpportunityService)
        {
            SearchAndSend(ema5MQueryFilePath, trackedElementsEMA5Minutes, "5M_EMA", tradeOpportunityService);
        }

        public static void EMA15MReversal(string ema15MQueryFilePath, ISQLServerServiceFactory tradeOpportunityService)
        {
            SearchAndSend(ema15MQueryFilePath, trackedElementsEMA15Minutes, "15M_EMA", tradeOpportunityService);
        }
        public static void EMA30MReversal(string ema30MQueryFilePath, ISQLServerServiceFactory tradeOpportunityService)
        {
            SearchAndSend(ema30MQueryFilePath, trackedElementsEMA30Minutes, "30M_EMA", tradeOpportunityService);
        }
        public static void EMAOneHourReversal(string ema1HQueryFilePath, ISQLServerServiceFactory tradeOpportunityService)
        {
            SearchAndSend(ema1HQueryFilePath, trackedElementsEMAOneHour, "1H_EMA", tradeOpportunityService);
        }

        public static void EMATwoHourReversal(string ema2HQueryFilePath, ISQLServerServiceFactory tradeOpportunityService)
        {
            SearchAndSend(ema2HQueryFilePath, trackedElementsEMATwoHour, "2H_EMA", tradeOpportunityService);
        }
        public static void EMAFourHourReversal(string ema4HQueryFilePath, ISQLServerServiceFactory tradeOpportunityService)
        {
            SearchAndSend(ema4HQueryFilePath, trackedElementsEMAFourHour, "4H_EMA", tradeOpportunityService);
        }
        public static void EMAOneDayReversal(string emaDQueryFilePath, ISQLServerServiceFactory tradeOpportunityService)
        {
            SearchAndSend(emaDQueryFilePath, trackedElementsEMAOneDay, "1D_EMA", tradeOpportunityService);
        }
        public static void MacdOneDayReversal(string macdDQueryFilePath, ISQLServerServiceFactory tradeOpportunityService)
        {
            SearchAndSend(macdDQueryFilePath, trackedElementsMACDOneDay, "1D_MACD", tradeOpportunityService);
        }
        public static void MacdOneHourReversal(string macd1HQueryFilePath, ISQLServerServiceFactory tradeOpportunityService)
        {
            SearchAndSend(macd1HQueryFilePath, trackedElementsMACDOneHour, "1H_MACD", tradeOpportunityService);
        }
        public static void EMAOneWeekReversal(string emaWQueryFilePath, ISQLServerServiceFactory tradeOpportunityService)
        {
            SearchAndSend(emaWQueryFilePath, trackedElementsEMAOneWeek, "1W_EMA", tradeOpportunityService);
        }
        public static void WeekDarvasBoxBullish(string queryPath, ISQLServerServiceFactory tradeOpportunityService)
        {
            SearchAndSend(queryPath, trackedElementsWeekDarvas, "52W_Darvas", tradeOpportunityService);
        }
        public static void AllTimeDarvasBoxBullish(string queryPath, ISQLServerServiceFactory tradeOpportunityService)
        {
            SearchAndSend(queryPath, trackedElementsAllTimeDarvas, "AllTime_Darvas", tradeOpportunityService);
        }

        private static void SearchAndSend(string queryPath, Dictionary<string, decimal> trackedElements, string algoName, ISQLServerServiceFactory tradeOpportunityService)
        {
            SearchResponse res = APIServices.Screener(queryPath).Result;
            if (res != null & res.totalCount > 0)
            {
                foreach (var ticker in res.data)
                {
                    if (!trackedElements.ContainsKey(ticker.s))
                    {
                        var tickerName = ticker.s;
                        var price = decimal.Parse(ticker.d[6]?.ToString());
                        var change = Math.Round(decimal.Parse(ticker.d[12]?.ToString()), 3);
                        var volume = Math.Round(decimal.Parse(ticker.d[13].ToString()) / 1000000, 3);
                        trackedElements.Add(tickerName, price);
                        var Message = "Bullish: " + algoName + "-- " + tickerName + " P.=" + price + " C.=" + change + "% V.= " + volume + " M T.= " + trackedElements.Count;
                        APIServices.SendToTeligrams(Message);
                        tradeOpportunityService.Create<TradeOpportunity>().Add(new TradeOpportunity
                        {
                            CrossOverDateTime = DateTime.Now,
                            Ticker = tickerName,
                            PercentChange = change,
                            Price = price,
                            AlgoName = algoName,
                            Volume = volume,
                            CrossOverType = "Bullish"
                        });
                        //tradeOpportunityService.Create<TradeExecution>().Add(new TradeExecution
                        //{
                        //    TradeOpportunityId = 367,
                        //    ExecutionDateTime = DateTime.Now,
                        //    ExecutionPrice = price,
                        //    Quantity = 1,
                        //    InTrade = true,
                        //    TradeType = "Buy",
                        //    Status = "Open",
                        //    ProfitLoss = 0,
                        //    ExecutionFee = 0,
                        //    Notes = "Initial Buy"
                        //});
                        // var tickerData = tickerName.Split(':');
                        //var dataa = APIServices.GetCurrentPrices("");
                        //foreach (var item in dataa.Result.data)
                        //{
                        //    Console.WriteLine(item.companyName +","+decimal.Parse(item.lastPrice.Replace(",","")) + "," + decimal.Parse(item.perChange.Replace(",", "")));
                        //}
                    }
                }
            }
        }
        //impliment a method if 1 min 9,20 ema crossover happen if so add data to tradeopportunity table and call one min execute trade methode
        public static async Task OneMin5_9EMACrossOver(string queryPath, ISQLServerServiceFactory tradeOpportunityService)
        {
            SearchResponse res = await APIServices.Screener(queryPath);
            if (res != null && res.totalCount > 0)
            {
                foreach (var ticker in res.data)
                {
                    var tickerName = ticker.s;
                    var price = decimal.Parse(ticker.d[6]?.ToString());
                    var change = Math.Round(decimal.Parse(ticker.d[12]?.ToString()), 3);
                    var volume = Math.Round(decimal.Parse(ticker.d[13].ToString()) / 1000000, 3);
                    var tradeOpportunity = new TradeOpportunityOneMin
                    {
                        CrossOverDateTime = DateTime.Now,
                        Ticker = tickerName,
                        PercentChange = change,
                        Price = price,
                        AlgoName = "1M_5_9_EMA",
                        Volume = volume,
                        CrossOverType = "Bullish"
                    };
                    await tradeOpportunityService.Create<TradeOpportunityOneMin>().Add(tradeOpportunity);

                    var tradeOpportunityOneMinId = tradeOpportunity.TradeOpportunityOneMinId;
                    if (!await IsTradeOpen(tickerName, tradeOpportunityService))
                    {
                        await OneMinExecuteTrade(tradeOpportunityOneMinId, tickerName, price, tradeOpportunityService);
                    }
                }
            }
        }

        public static async Task<bool> IsTradeOpen(string ticker, ISQLServerServiceFactory tradeOpportunityService)
        {
            var trade = (await tradeOpportunityService.Create<TradeExecutionOneMin>().GetAll()).FirstOrDefault(x => x.Ticker == ticker && x.Status == "Open");
            return trade != null;
        }

        public static async Task OneMinExecuteTrade(int tradeOpportunityId, string ticker, decimal price, ISQLServerServiceFactory tradeOpportunityService)
        {
            await tradeOpportunityService.Create<TradeExecutionOneMin>().Add(new TradeExecutionOneMin
            {
                TradeOpportunityOneMinId = tradeOpportunityId,
                ExecutionDateTime = DateTime.Now,
                ExecutionPrice = price,
                Quantity = 1,
                InTrade = true,
                TradeType = "Buy",
                Status = "Open",
                ProfitLoss = 0,
                ExecutionFee = 0,
                Notes = "Initial Buy",
                Ticker = ticker

            });
        }

        //implement a methode to close trade and update trade execution table
        public static async void OneMinCloseTrade(string ticker, decimal price, ISQLServerServiceFactory tradeOpportunityService)
        {
            var trade =  tradeOpportunityService.Create<TradeExecutionOneMin>().GetAll().Result.ToList<TradeExecutionOneMin>().Where(x => x.Ticker == ticker && x.Status == "Open").FirstOrDefault();
            if (trade != null)
            {
                trade.ExecutionDateTime = DateTime.Now;
                trade.ExecutionPrice = price;
                trade.InTrade = false;
                trade.Status = "Closed";
                trade.ProfitLoss = price - trade.ExecutionPrice;
                trade.ExecutionFee = 0;
                trade.Notes = "Closed Trade";
                tradeOpportunityService.Create<TradeExecutionOneMin>().Update(trade);
            }
        }
    }
}
