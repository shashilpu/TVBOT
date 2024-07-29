using Serilog;
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
        static int oneMinRepeat = 0;
        static int bullRepeat = 0;


        public static async void EMA1MReversal(string ema1MQueryFilePath, ISQLServerServiceFactory tradeOpportunityService)
        {

            await OneMin5_9EMACrossOver(ema1MQueryFilePath, tradeOpportunityService);
        }

        public static async void EMA5MReversal(string ema5MQueryFilePath, ISQLServerServiceFactory tradeOpportunityService)
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

        private static async void SearchAndSend(string queryPath, Dictionary<string, decimal> trackedElements, string algoName, ISQLServerServiceFactory tradeOpportunityService)
        {
            try
            {

                SearchResponse res = APIServices.Screener(queryPath).Result;
                if (res != null & res.totalCount > 0)
                {
                    foreach (var ticker in res.data)
                    {
                        if (!trackedElements.ContainsKey(ticker.s))
                        {
                            var analystRating = 0.0m;
                            var beta = 0.0m;
                            var percentVolalityOneWeek = 0.0m;
                            var tickerName = ticker.s;
                            var price = decimal.Parse(ticker.d[6]?.ToString());
                            var change = Math.Round(decimal.Parse(ticker.d[12]?.ToString()), 3);
                            var volume = Math.Round(decimal.Parse(ticker.d[13].ToString()) / 1000000, 3);
                            if (ticker.d[24] != null)
                            {
                                decimal.TryParse(ticker.d[24].ToString(), out analystRating);
                            }
                            if (ticker.d[27] != null)
                                decimal.TryParse(ticker.d[27]?.ToString(), out beta);
                            if (ticker.d[28] != null)
                                decimal.TryParse(ticker.d[28].ToString(), out percentVolalityOneWeek);
                            var tickerLink = "https://in.tradingview.com/chart/?symbol=" + tickerName;
                            var Message = $"Bullish: {algoName} -- <a href=\"{tickerLink}\">{tickerName}</a> P.={price} C.={change}% V.={volume} Beta.={beta} Volality.={percentVolalityOneWeek} AR.={analystRating}";
                            if (beta > 1 || percentVolalityOneWeek > 5 || volume > 10)
                            {
                                // APIServices.SendToTeligrams(Message);
                                //avoid adding tradeOpportunity if tradeOpportunity with same Ticker added to table today
                                var lastTradeOpportunity = (await tradeOpportunityService.Create<TradeOpportunity>().GetAll()).Where(x => x.Ticker == tickerName && x.CrossOverDateTime >= DateTime.Now.Date).OrderByDescending(x => x.CrossOverDateTime).FirstOrDefault();
                                if (lastTradeOpportunity == null)
                                {
                                    var tradeOpportunityBull = new TradeOpportunity
                                    {
                                        CrossOverDateTime = DateTime.Now,
                                        Ticker = tickerName,
                                        PercentChange = change,
                                        Price = price,
                                        AlgoName = algoName,
                                        Volume = volume,
                                        CrossOverType = "Bullish",
                                        BetaOneYear = beta,
                                        PercentVolalityOneWeek = percentVolalityOneWeek
                                    };
                                    // check if crossover happen eariler also except today if so get the last crossover date, price and algoname and send to telegram
                                    var lastTradeOpportunityExceptToday = (await tradeOpportunityService.Create<TradeOpportunity>().GetAll()).Where(x => x.Ticker == tickerName && x.CrossOverDateTime < DateTime.Now.Date).OrderByDescending(x => x.CrossOverDateTime).FirstOrDefault();
                                    if (lastTradeOpportunityExceptToday != null)
                                    {
                                        Message += " Last CrossOver.= " + lastTradeOpportunityExceptToday.CrossOverDateTime + " Price.= " + lastTradeOpportunityExceptToday.Price + " AlgoN.= " + lastTradeOpportunityExceptToday.AlgoName;

                                    }
                                    APIServices.SendToTeligrams(Message);
                                    await tradeOpportunityService.Create<TradeOpportunity>().Add(tradeOpportunityBull);
                                    var tradeOpportunityOneMinId = tradeOpportunityBull.Id;
                                    if (!await IsTradeOpenBull(tickerName, tradeOpportunityService))
                                    {
                                        await ExecuteTrade(tradeOpportunityOneMinId, tickerName, price, tradeOpportunityService);
                                    }
                                }
                                else
                                {

                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, ex.Message, ex.InnerException);
            }
        }
        //impliment a method if 1 min 9,20 ema crossover happen if so add data to tradeopportunity table and call one min execute trade methode
        public static async Task OneMin5_9EMACrossOver(string queryPath, ISQLServerServiceFactory tradeOpportunityService)
        {
            try
            {
                SearchResponse res = await APIServices.Screener(queryPath);
                if (res != null && res.totalCount > 0)
                {
                    foreach (var ticker in res.data)
                    {
                        var analystRating = 0.0m;
                        var beta = 0.0m;
                        var percentVolalityOneWeek = 0.0m;
                        var tickerName = ticker.s;
                        var price = decimal.Parse(ticker.d[6]?.ToString());
                        var change = Math.Round(decimal.Parse(ticker.d[12]?.ToString()), 3);
                        var volume = Math.Round(decimal.Parse(ticker.d[13].ToString()) / 1000000, 3);
                        if (ticker.d[24] != null)
                        {
                            decimal.TryParse(ticker.d[24].ToString(), out analystRating);
                        }
                        if (ticker.d[27] != null)
                            decimal.TryParse(ticker.d[27]?.ToString(), out beta);
                        if (ticker.d[28] != null)
                            decimal.TryParse(ticker.d[28].ToString(), out percentVolalityOneWeek);
                        if (beta > 1 || percentVolalityOneWeek > 5 || volume > 10)
                        {
                            // avoid adding tradeOpportunity if tradeOpportunity with same Ticker added to table 15 min ago
                            var lastTradeOpportunity = (await tradeOpportunityService.Create<TradeOpportunityOneMin>().GetAll())
                                .Where(x => x.Ticker == tickerName && x.CrossOverDateTime > DateTime.Now.AddMinutes(-15))
                                .OrderByDescending(x => x.CrossOverDateTime)
                                .FirstOrDefault();
                            if (lastTradeOpportunity == null)
                            {
                                var tradeOpportunity = new TradeOpportunityOneMin
                                {
                                    CrossOverDateTime = DateTime.Now,
                                    Ticker = tickerName,
                                    PercentChange = change,
                                    Price = price,
                                    AlgoName = "1M_5_9_EMA",
                                    Volume = volume,
                                    CrossOverType = "Bullish",
                                    BetaOneYear = beta,
                                    PercentVolalityOneWeek = percentVolalityOneWeek

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
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, ex.Message, ex.InnerException);
            }
        }

        public static async Task<bool> IsTradeOpen(string tickerName, ISQLServerServiceFactory tradeOpportunityService)
        {
            try
            {
                var trade = (await tradeOpportunityService.Create<TradeExecutionOneMin>().GetAll()).OrderBy(x => x.ExecutionDateTime).FirstOrDefault(x => x.Ticker == tickerName && x.Status == "Open");
                return trade != null;
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, ex.Message, ex.InnerException);
                return false;
            }

        }
        public static async Task<bool> IsTradeOpenBull(string tickerName, ISQLServerServiceFactory tradeOpportunityService)
        {
            try
            {
                var trade = (await tradeOpportunityService.Create<TradeExecution>().GetAll()).OrderBy(x => x.ExecutionDateTime).FirstOrDefault(x => x.Ticker == tickerName && x.Status == "Open");
                return trade != null;
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, ex.Message, ex.InnerException);
                return false;
            }

        }
        public static async Task OneMinExecuteTrade(int tradeOpportunityId, string tickerName, decimal price, ISQLServerServiceFactory tradeOpportunityService)
        {

            // calculate here quantity(absolute) based on 10000 inr devided by current price and total investment amount by multiplying  quantity(absolute) * current price 

            int quantity = (int)Math.Abs(10000 / price);
            var totalInvestment = quantity * price;
            var currentVale = price * quantity;
            bool isRepeatedTrade = false;
            if (oneMinRepeat > 0)
            {
                oneMinRepeat--;
                isRepeatedTrade = true;
            }
            else
            {
                isRepeatedTrade = false;
            }
            try
            {
                await tradeOpportunityService.Create<TradeExecutionOneMin>().Add(new TradeExecutionOneMin
                {
                    TradeOpportunityOneMinId = tradeOpportunityId,
                    ExecutionDateTime = DateTime.Now,
                    ExecutionPrice = price,
                    Quantity = quantity,
                    InTrade = true,
                    TradeType = "Buy",
                    Status = "Open",
                    ProfitLoss = 0,
                    ExecutionFee = 0,
                    Notes = "Initial Buy",
                    Ticker = tickerName,
                    TrargetPrice = price + (price * 0.01m),
                    InvestedAmount = totalInvestment,
                    IsRepeatedTrade = isRepeatedTrade,
                    CurrentProfitLossOnTrade = currentVale - totalInvestment


                });
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, ex.Message, ex.InnerException);
            }

        }
        public static async Task ExecuteTrade(int tradeOpportunityId, string tickerName, decimal price, ISQLServerServiceFactory tradeOpportunityService)
        {
            var tradeCurrentTime = DateTime.Now.TimeOfDay;
            var tradeStartTime = new TimeSpan(9, 15, 10);
            var tradeEndTime = new TimeSpan(14, 15, 0);
            try
            {

                if (tradeCurrentTime >= tradeStartTime && tradeCurrentTime <= tradeEndTime)
                {
                    int quantity = (int)Math.Abs(10000 / price);
                    var totalInvestment = quantity * price;
                    var currentVale = price * quantity;
                    bool isRepeatedTrade = false;
                    if (bullRepeat > 0)
                    {
                        bullRepeat--;
                        isRepeatedTrade = true;
                    }
                    else
                    {
                        isRepeatedTrade = false;
                    }
                    await tradeOpportunityService.Create<TradeExecution>().Add(new TradeExecution
                    {
                        TradeOpportunityId = tradeOpportunityId,
                        ExecutionDateTime = DateTime.Now,
                        ExecutionPrice = price,
                        Quantity = quantity,
                        InTrade = true,
                        TradeType = "Buy",
                        Status = "Open",
                        ProfitLoss = 0,
                        ExecutionFee = 0,
                        Notes = "Initial Buy",
                        Ticker = tickerName,
                        TrargetPrice = price + (price * 0.01m),
                        InvestedAmount = totalInvestment,
                        IsRepeatedTrade = isRepeatedTrade,
                        CurrentProfitLossOnTrade = currentVale - totalInvestment

                    });
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, ex.Message, ex.InnerException);
            }

        }
        //implement a methode to close trade and update trade execution table
        public static async void OneMinCloseTrade(string ticker, string closedFrom, decimal price, ISQLServerServiceFactory tradeOpportunityService)
        {
            try

            {
                var trade = (await tradeOpportunityService.Create<TradeExecutionOneMin>().GetAll()).Where(x => x.Ticker == ticker && x.Status == "Open").OrderBy(x => x.ExecutionDateTime).FirstOrDefault();
                if (trade != null)
                {
                    var currentVale = price * trade.Quantity;
                    var totalInvestment = trade.ExecutionPrice * trade.Quantity;
                    var totalProfitLoss = currentVale - totalInvestment;
                    trade.TradeCloseDateTime = DateTime.Now;
                    trade.TradeClosePrice = price;
                    trade.InTrade = false;
                    trade.Status = "Closed";
                    trade.ProfitLoss = totalProfitLoss;
                    trade.PercentProfitLoss = (totalProfitLoss / totalInvestment) * 100;
                    trade.ExecutionFee = 0;
                    trade.Notes = "Closed Trade from " + closedFrom;
                    tradeOpportunityService.Create<TradeExecutionOneMin>().Update(trade);
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, ex.Message, ex.InnerException);
            }

        }
        public static async void CloseTrade(string ticker, string closedFrom, decimal price, ISQLServerServiceFactory tradeOpportunityService)
        {
            try
            {

                var trade = (await tradeOpportunityService.Create<TradeExecution>().GetAll()).Where(x => x.Ticker == ticker && x.Status == "Open").OrderBy(x => x.ExecutionDateTime).FirstOrDefault();
                if (trade != null)
                {
                    var currentVale = price * trade.Quantity;
                    var totalInvestment = trade.ExecutionPrice * trade.Quantity;
                    var totalProfitLoss = currentVale - totalInvestment;
                    trade.TradeCloseDateTime = DateTime.Now;
                    trade.TradeClosePrice = price;
                    trade.InTrade = false;
                    trade.Status = "Closed";
                    trade.ProfitLoss = totalProfitLoss;
                    trade.PercentProfitLoss = (totalProfitLoss / totalInvestment) * 100;
                    trade.ExecutionFee = 0;
                    trade.Notes = "Closed Trade from " + closedFrom;
                    tradeOpportunityService.Create<TradeExecution>().Update(trade);
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, ex.Message, ex.InnerException);
            }

        }
        public static async void UpdatePrice(string ticker, decimal price, ISQLServerServiceFactory tradeOpportunityService)
        {
            try
            {

                var trade = (await tradeOpportunityService.Create<TradeExecution>().GetAll()).Where(x => x.Ticker == ticker && x.Status == "Open").OrderBy(x => x.ExecutionDateTime).FirstOrDefault();
                if (trade != null)
                {
                    var currentVale = price * trade.Quantity;
                    var totalInvestment = trade.ExecutionPrice * trade.Quantity;
                    var totalProfitLoss = currentVale - totalInvestment;
                    trade.CurrentPrice = price;
                    trade.CurrentProfitLossOnTrade = totalProfitLoss;
                    tradeOpportunityService.Create<TradeExecution>().Update(trade);
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, ex.Message, ex.InnerException);
            }

        }
        public static async void OneMInUpdatePrice(string ticker, decimal price, ISQLServerServiceFactory tradeOpportunityService)
        {
            try
            {

                var trade = (await tradeOpportunityService.Create<TradeExecutionOneMin>().GetAll()).Where(x => x.Ticker == ticker && x.Status == "Open").OrderBy(x => x.ExecutionDateTime).FirstOrDefault();
                if (trade != null)
                {
                    var currentVale = price * trade.Quantity;
                    var totalInvestment = trade.ExecutionPrice * trade.Quantity;
                    var totalProfitLoss = currentVale - totalInvestment;
                    trade.CurrentPrice = price;
                    trade.CurrentProfitLossOnTrade = totalProfitLoss;
                    tradeOpportunityService.Create<TradeExecutionOneMin>().Update(trade);
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, ex.Message, ex.InnerException);
            }

        }
        //impliment a method to check if 1 min 5,9 ema downward crossover hanppen if so close the trade
        public static async void OneMin5_9EMADownwardCrossOver(string queryPath, ISQLServerServiceFactory tradeOpportunityService)
        {
            try
            {


                SearchResponse res = await APIServices.Screener(queryPath);
                if (res != null && res.totalCount > 0)
                {
                    foreach (var ticker in res.data)
                    {
                        var beta = 0.0m;
                        var percentVolalityOneWeek = 0.0m;
                        var tickerName = ticker.s;
                        var price = decimal.Parse(ticker.d[6]?.ToString());
                        var change = Math.Round(decimal.Parse(ticker.d[12]?.ToString()), 3);
                        var volume = Math.Round(decimal.Parse(ticker.d[13].ToString()) / 1000000, 3);
                        if (ticker.d[27] != null)
                            decimal.TryParse(ticker.d[27]?.ToString(), out beta);
                        if (ticker.d[28] != null)
                            decimal.TryParse(ticker.d[28].ToString(), out percentVolalityOneWeek);

                        if (await IsTradeOpen(tickerName, tradeOpportunityService))
                        {
                            var trade = (await tradeOpportunityService.Create<TradeExecutionOneMin>().GetAll()).Where(x => x.Ticker == tickerName && x.Status == "Open").OrderBy(x => x.ExecutionDateTime).FirstOrDefault();
                            if ((price - trade?.ExecutionPrice) > 0)
                            {
                                // check if price is greater than 1% of execution price
                                if ((price - trade.ExecutionPrice) / trade.ExecutionPrice * 100 >= 1)
                                {
                                    OneMinCloseTrade(tickerName, "EMA Crossover", price, tradeOpportunityService);
                                }
                            }

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, ex.Message, ex.InnerException);
            }
        }
        // impliment a method to get all open trades and check if price is greater than 0.5% of execution price if so close the trade
        public static async void GetCurrentPriceAndCloseOpenTrades(ISQLServerServiceFactory tradeOpportunityService)
        {
            try
            {

                var openTrades = (await tradeOpportunityService.Create<TradeExecutionOneMin>().GetAll()).Where(x => x.Status == "Open");
                foreach (var trade in openTrades)
                {
                    var result = await APIServices.GetCurrentPriceFromFivePaise(trade.Ticker.Split(':')[1]);
                    if (result != null)
                    {
                        var currentPrice = decimal.Parse(result.stocks.price + "." + result.stocks.d_price);
                        if ((currentPrice - trade.ExecutionPrice) > 0)
                        {
                            // check if price is greater than 1% of execution price
                            if ((currentPrice - trade.ExecutionPrice) / trade.ExecutionPrice * 100 >= 1)
                            {
                                OneMinCloseTrade(trade.Ticker, "GetCurrentPriceAndCloseOpenTrades", currentPrice, tradeOpportunityService);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, ex.Message, ex.InnerException);
            }
        }
        public static async void GetCurrentPriceAllNSEStockAndCloseOpenTrades(ISQLServerServiceFactory tradeOpportunityService, string queryPath)
        {
            try
            {
                SearchResponse res = await APIServices.Screener(queryPath);
                if (res != null && res.totalCount > 0)
                {

                    var openTrades = (await tradeOpportunityService.Create<TradeExecutionOneMin>().GetAll()).ToList().Where(x => x.Status == "Open");
                    if (openTrades != null)
                    {
                        foreach (var trade in openTrades)
                        {
                            var ticker = res.data.FirstOrDefault(t => t.s == trade.Ticker);
                            var currentPrice = decimal.Parse(ticker.d[6]?.ToString());
                            var change = Math.Round(decimal.Parse(ticker.d[12]?.ToString()), 3);
                            var volume = Math.Round(decimal.Parse(ticker.d[13].ToString()) / 1000000, 3);
                            var executionPrice = trade.ExecutionPrice;
                            var onePercentOfEP = executionPrice * 0.01m;
                            var ePWithOnePercentIncrease = trade.ExecutionPrice + onePercentOfEP;
                            if (currentPrice > ePWithOnePercentIncrease)
                            {

                                oneMinRepeat++;
                                OneMinCloseTrade(trade.Ticker, "GetCurrentPriceAndCloseOpenTrades", currentPrice, tradeOpportunityService);


                            }
                            else
                            {
                                OneMInUpdatePrice(trade.Ticker, currentPrice, tradeOpportunityService);
                            }

                        }
                    }
                    var openTradesBull = (await tradeOpportunityService.Create<TradeExecution>().GetAll()).ToList().Where(x => x.Status == "Open");
                    if (openTradesBull != null)
                    {
                        foreach (var trade in openTradesBull)
                        {
                            var ticker = res.data.FirstOrDefault(t => t.s == trade.Ticker);
                            var currentPrice = decimal.Parse(ticker.d[6]?.ToString());
                            var change = Math.Round(decimal.Parse(ticker.d[12]?.ToString()), 3);
                            var volume = Math.Round(decimal.Parse(ticker.d[13].ToString()) / 1000000, 3);
                            var executionPrice = trade.ExecutionPrice;
                            var onePercentOfEP = executionPrice * 0.01m;
                            var ePWithOnePercentIncrease = trade.ExecutionPrice + onePercentOfEP;
                            if (currentPrice > ePWithOnePercentIncrease)
                            {

                                bullRepeat++;
                                CloseTrade(trade.Ticker, "GetCurrentPriceAndCloseOpenTrades", currentPrice, tradeOpportunityService);

                            }
                            else
                            {

                                UpdatePrice(trade.Ticker, currentPrice, tradeOpportunityService);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, ex.Message, ex.InnerException);
            }
        }
    }
}

