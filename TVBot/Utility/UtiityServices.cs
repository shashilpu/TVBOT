﻿using Serilog;
using TVBot.Model.Entities;
using TVBot.Models;
using TVBot.Services;
using TVBot.Services.Factory;

namespace TVBot.Utility
{
    public static class UtiityServices
    {
        static int bullRepeat = 0;

        public static async void EMA1MReversal(string ema1MQueryFilePath, ISQLServerServiceFactory tradeOpportunityService)
        {

            //await OneMin5_9EMACrossOver(ema1MQueryFilePath, tradeOpportunityService);
            SearchAndSend(ema1MQueryFilePath, "1M_EMA", tradeOpportunityService);
        }

        public static async void EMA5MReversal(string ema5MQueryFilePath, ISQLServerServiceFactory tradeOpportunityService)
        {
            SearchAndSend(ema5MQueryFilePath, "5M_EMA", tradeOpportunityService);
        }

        public static void EMA15MReversal(string ema15MQueryFilePath, ISQLServerServiceFactory tradeOpportunityService)
        {
            SearchAndSend(ema15MQueryFilePath, "15M_EMA", tradeOpportunityService);
        }
        public static void EMA30MReversal(string ema30MQueryFilePath, ISQLServerServiceFactory tradeOpportunityService)
        {
            SearchAndSend(ema30MQueryFilePath, "30M_EMA", tradeOpportunityService);
        }
        public static void EMAOneHourReversal(string ema1HQueryFilePath, ISQLServerServiceFactory tradeOpportunityService)
        {
            SearchAndSend(ema1HQueryFilePath, "1H_EMA", tradeOpportunityService);
        }

        public static void EMATwoHourReversal(string ema2HQueryFilePath, ISQLServerServiceFactory tradeOpportunityService)
        {
            SearchAndSend(ema2HQueryFilePath, "2H_EMA", tradeOpportunityService);
        }
        public static void EMAFourHourReversal(string ema4HQueryFilePath, ISQLServerServiceFactory tradeOpportunityService)
        {
            SearchAndSend(ema4HQueryFilePath, "4H_EMA", tradeOpportunityService);
        }
        public static void EMAOneDayReversal(string emaDQueryFilePath, ISQLServerServiceFactory tradeOpportunityService)
        {
            SearchAndSend(emaDQueryFilePath, "1D_EMA", tradeOpportunityService);
        }
        public static void MacdOneDayReversal(string macdDQueryFilePath, ISQLServerServiceFactory tradeOpportunityService)
        {
            SearchAndSend(macdDQueryFilePath, "1D_MACD", tradeOpportunityService);
        }
        public static void MacdOneHourReversal(string macd1HQueryFilePath, ISQLServerServiceFactory tradeOpportunityService)
        {
            SearchAndSend(macd1HQueryFilePath, "1H_MACD", tradeOpportunityService);
        }
        public static void EMAOneWeekReversal(string emaWQueryFilePath, ISQLServerServiceFactory tradeOpportunityService)
        {
            SearchAndSend(emaWQueryFilePath, "1W_EMA", tradeOpportunityService);
        }
        public static void WeekDarvasBoxBullish(string queryPath, ISQLServerServiceFactory tradeOpportunityService)
        {
            SearchAndSend(queryPath, "52W_Darvas", tradeOpportunityService);
        }
        public static void AllTimeDarvasBoxBullish(string queryPath, ISQLServerServiceFactory tradeOpportunityService)
        {
            SearchAndSend(queryPath, "AllTime_Darvas", tradeOpportunityService);
        }

        private static async void SearchAndSend(string queryPath, string algoName, ISQLServerServiceFactory tradeOpportunityService)
        {
            try
            {
                var minBeta = 0.5m;
                SearchResponse res = APIServices.Screener(queryPath).Result;
                if (res != null & res.totalCount > 0)
                {
                    foreach (var ticker in res.data)
                    {
                        var lastTradeOpportunity = new TradeOpportunity();
                        bool negativeOneMinBullOpportunity = false;
                        var _message = "";
                        var isTradeFromPastBullCross = false;
                        var beta = 0.0m;
                        var analystRatings = "-";
                        var percentVolalityOneWeek = 0.0m;
                        var tickerName = ticker.s;
                        var price = decimal.Parse(ticker.d[6]?.ToString());
                        var change = Math.Round(decimal.Parse(ticker.d[12]?.ToString()), 3);
                        var volume = Math.Round(decimal.Parse(ticker.d[13].ToString()) / 1000000, 3);
                        if (ticker.d[24] != null)
                        {
                            // Add the following code inside the method where the selected code is located
                            var analystRating = 0.0m;
                            decimal.TryParse(ticker.d[24].ToString(), out analystRating);

                            if (analystRating > 2.75m && analystRating <= 3.0m)
                            {
                                analystRatings = "Strong Sell";
                            }
                            else if (analystRating > 2.25m && analystRating <= 2.75m)
                            {
                                analystRatings = "Sell";
                            }
                            else if (analystRating > 1.75m && analystRating <= 2.25m)
                            {
                                analystRatings = "Neutral";
                            }
                            else if (analystRating > 1.25m && analystRating <= 1.75m)
                            {
                                analystRatings = "Buy";
                            }
                            else if (analystRating >= 1.0m && analystRating <= 1.25m)
                            {
                                analystRatings = "Strong Buy";
                            }
                            else
                            {
                                analystRatings = "No Rating: " + ticker.d[24];
                            }

                        }
                        if (ticker.d[27] != null)
                            decimal.TryParse(ticker.d[27]?.ToString(), out beta);
                        if (ticker.d[28] != null)
                            decimal.TryParse(ticker.d[28].ToString(), out percentVolalityOneWeek);
                        var tickerLink = "https://in.tradingview.com/chart/?symbol=" + tickerName;
                        var Message = $" Bullish: {algoName} -- <a href=\"{tickerLink}\">{tickerName}</a> P.={price} C.={change}% V.={volume} Beta.={beta} Volality.={percentVolalityOneWeek} AR.={analystRatings}";
                        //If change is between -2 and 3 %, no trade opportunity in last 60 minutes and it has past bullish cross record then create a trade opportunity

                        var lastTradeOpportunityExceptTodays = (await tradeOpportunityService.Create<TradeOpportunity>().GetAll()).Where(
                               x => x.Ticker == tickerName && !(x.AlgoName == "1M_EMA" || x.AlgoName == "5M_EMA" || x.AlgoName == "15M_EMA") && x.CrossOverDateTime < DateTime.Now.AddMinutes(-60))
                               .OrderByDescending(x => x.CrossOverDateTime).FirstOrDefault();
                        if (lastTradeOpportunityExceptTodays != null & change > -2 & change < 0)
                            negativeOneMinBullOpportunity = true;


                        if ((change > 0 && change < 5) && (beta > minBeta || percentVolalityOneWeek > 3))
                        {
                            if (algoName == "1M_EMA")
                            {
                                lastTradeOpportunity = (await tradeOpportunityService.Create<TradeOpportunity>().GetAll())
                              .Where(x => x.Ticker == tickerName && x.CrossOverDateTime > DateTime.Now.AddMinutes(-15))
                              .OrderByDescending(x => x.CrossOverDateTime)
                              .FirstOrDefault();
                            }
                            else if (algoName == "5M_EMA")
                            {
                                lastTradeOpportunity = (await tradeOpportunityService.Create<TradeOpportunity>().GetAll())
                             .Where(x => x.Ticker == tickerName && x.CrossOverDateTime > DateTime.Now.AddMinutes(-30))
                             .OrderByDescending(x => x.CrossOverDateTime)
                             .FirstOrDefault();
                            }
                            else if (algoName == "15M_EMA")
                            {
                                lastTradeOpportunity = (await tradeOpportunityService.Create<TradeOpportunity>().GetAll())
                             .Where(x => x.Ticker == tickerName && x.CrossOverDateTime > DateTime.Now.AddMinutes(-60))
                             .OrderByDescending(x => x.CrossOverDateTime)
                             .FirstOrDefault();
                            }
                            else if (algoName == "30M_EMA")
                            {
                                lastTradeOpportunity = (await tradeOpportunityService.Create<TradeOpportunity>().GetAll())
                             .Where(x => x.Ticker == tickerName && x.CrossOverDateTime > DateTime.Now.AddMinutes(-90))
                             .OrderByDescending(x => x.CrossOverDateTime)
                             .FirstOrDefault();
                            }
                            else if (algoName == "1H_EMA")
                            {
                                lastTradeOpportunity = (await tradeOpportunityService.Create<TradeOpportunity>().GetAll())
                             .Where(x => x.Ticker == tickerName && x.CrossOverDateTime > DateTime.Now.AddMinutes(-120))
                             .OrderByDescending(x => x.CrossOverDateTime)
                             .FirstOrDefault();
                            }
                            else
                            {
                                lastTradeOpportunity = (await tradeOpportunityService.Create<TradeOpportunity>().GetAll()).Where(x => x.Ticker == tickerName && x.CrossOverDateTime >= DateTime.Now.Date).OrderByDescending(x => x.CrossOverDateTime).FirstOrDefault();
                            }
                            if (lastTradeOpportunity == null)
                            {
                                // check if crossover happen eariler also except today if so get the last crossover date, price and algoname and send to telegram
                                bool priceDownBwOneAndFourPercent = false;
                                var lastTradeOpportunityExceptToday = (await tradeOpportunityService.Create<TradeOpportunity>().GetAll()).Where(
                                    x => x.Ticker == tickerName && !(x.AlgoName == "1M_EMA" || x.AlgoName == "5M_EMA" || x.AlgoName == "15M_EMA") && x.CrossOverDateTime < DateTime.Now.AddMinutes(-60))
                                    .OrderByDescending(x => x.CrossOverDateTime).FirstOrDefault();
                                if (lastTradeOpportunityExceptToday != null)
                                {
                                    _message = " Last CrossOver.= " + lastTradeOpportunityExceptToday.CrossOverDateTime + " Price.= " + lastTradeOpportunityExceptToday.Price + " Vol.= " + lastTradeOpportunityExceptToday.Volume + " M AlgoN.= " + lastTradeOpportunityExceptToday.AlgoName + " TO.Id.= " + lastTradeOpportunityExceptToday.Id;
                                    Message += _message;
                                    // check if price is bewtween 2 and 5 % down of lastTradeOpportunityExceptToday.Price(last crossover price).
                                    if (price >= lastTradeOpportunityExceptToday.Price * 0.90m && price <= lastTradeOpportunityExceptToday.Price * 0.93m)
                                    {
                                        Message += " #<b>Probable Bottom Pattern</b>#";
                                        Message = "💚" + Message;
                                        priceDownBwOneAndFourPercent = true;
                                        APIServices.SendToTeligrams(Message);
                                    }

                                    isTradeFromPastBullCross = true;
                                    //if (algoName != "1M_EMA" || priceDownBwOneAndFourPercent)
                                    //{
                                    //    APIServices.SendToTeligrams(Message);
                                    //}

                                }
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
                                    AnalystRating = analystRatings,
                                    PercentVolalityOneWeek = percentVolalityOneWeek
                                };

                                await tradeOpportunityService.Create<TradeOpportunity>().Add(tradeOpportunityBull);
                                var tradeOpportunityOneMinId = tradeOpportunityBull.Id;
                                if (!await IsTradeOpen(tickerName, tradeOpportunityService))
                                {
                                    await ExecuteTrade(tradeOpportunityOneMinId, tickerName, price, isTradeFromPastBullCross, _message, tradeOpportunityService);
                                }
                            }
                            else
                            {

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
                var trade = (await tradeOpportunityService.Create<TradeExecution>().GetAll()).OrderBy(x => x.ExecutionDateTime).FirstOrDefault(x => x.Ticker == tickerName && x.Status == "Open");
                return trade != null;
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, ex.Message, ex.InnerException);
                return false;
            }

        }

        public static async Task ExecuteTrade(int tradeOpportunityId, string tickerName, decimal price, bool isTradeFromPastBull, string pastBullCrossInfo, ISQLServerServiceFactory tradeOpportunityService)
        {
            var tradeCurrentTime = DateTime.Now.TimeOfDay;
            var tradeStartTime = new TimeSpan(9, 15, 01);
            var tradeEndTime = new TimeSpan(15, 29, 30);
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
                        IsTradeFromPastBullCross = isTradeFromPastBull,
                        PastBullCrossInfo = pastBullCrossInfo,
                        TradeType = "Buy",
                        Status = "Open",
                        ProfitLoss = 0,
                        ExecutionFee = 0,
                        Notes = "Initial Buy",
                        Ticker = tickerName,
                        TrargetPrice = price * 1.005m,
                        StopLossPrice = price * 1.003m,
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

        public static async Task CloseTrade(string ticker, string closedFrom, decimal price, ISQLServerServiceFactory tradeOpportunityService)
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
        public static async Task UpdatePrice(string ticker, decimal price, ISQLServerServiceFactory tradeOpportunityService)
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

        public static async Task GetCurrentPriceAllNSEStockAndCloseOpenTrades(ISQLServerServiceFactory tradeOpportunityService, string queryPath)
        {
            try
            {
                SearchResponse res = await APIServices.Screener(queryPath);
                if (res != null && res.totalCount > 0)
                {

                    var openTradesBull = (await tradeOpportunityService.Create<TradeExecution>().GetAll()).ToList().Where(x => x.Status == "Open");
                    if (openTradesBull != null)
                    {
                        foreach (var trade in openTradesBull)
                        {
                            var ticker = res.data.FirstOrDefault(t => t.s == trade.Ticker);
                            if (ticker != null)
                            {
                                var currentPrice = decimal.Parse(ticker.d[6]?.ToString());
                                var change = Math.Round(decimal.Parse(ticker.d[12]?.ToString()), 3);
                                var volume = Math.Round(decimal.Parse(ticker.d[13].ToString()) / 1000000, 3);
                                var executionPrice = trade.ExecutionPrice;
                                var pointTwoPercentPrice = executionPrice * 1.002m;
                                if (currentPrice > pointTwoPercentPrice)
                                {

                                    bullRepeat++;
                                    // await CloseTrade(trade.Ticker, "Price All NSE Stock And Close", currentPrice, tradeOpportunityService);
                                    await UpdatePriceAndCheckStopLoss(trade.Ticker, currentPrice, tradeOpportunityService);

                                }
                                else
                                {
                                    await UpdatePrice(trade.Ticker, currentPrice, tradeOpportunityService);

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
        public static async Task UpdatePriceAndCheckStopLoss(string ticker, decimal price, ISQLServerServiceFactory tradeOpportunityService)
        {
            try
            {
                var trade = (await tradeOpportunityService.Create<TradeExecution>().GetAll()).FirstOrDefault(x => x.Ticker == ticker && x.Status == "Open");
                if (trade != null)
                {
                    var executionPrice = trade.ExecutionPrice;
                    var lastStopLossPrice = trade.StopLossPrice;
                    var firstStopLossPrice = executionPrice * 1.003m;
                    var initialStopLossPrice = 0.0m;
                    var lastTargetPrice = trade.TrargetPrice;
                    var currentPrice = price;                  
                    
                    if (trade.ExecutionDateTime.Date < DateTime.Today)
                        initialStopLossPrice = executionPrice * 1.0085m;
                    else
                        initialStopLossPrice = executionPrice * 1.003m;

                    if (currentPrice > lastTargetPrice)
                    {
                        trade.TrargetPrice = price * 1.002m; 
                        trade.StopLossPrice = price * 0.998m;
                        tradeOpportunityService.Create<TradeExecution>().Update(trade);
                        await UpdatePrice(ticker, currentPrice, tradeOpportunityService);
                    }
                    else if (lastStopLossPrice != firstStopLossPrice && currentPrice < lastStopLossPrice && lastStopLossPrice > initialStopLossPrice)
                    {
                        await CloseTrade(trade.Ticker, "Stop Loss Exit", currentPrice, tradeOpportunityService);                       
                    }
                    else
                    {
                        await UpdatePrice(ticker, currentPrice, tradeOpportunityService);
                    }
                   
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, ex.Message, ex.InnerException);
            }
        }
        public async static Task SendReport(ISQLServerServiceFactory tradeOpportunityService)
        {
            var tradeExecution = (await tradeOpportunityService.Create<TradeExecution>().GetAll()).ToList();
            var result = new
            {
                TotalTrades = tradeExecution.Count(),
                RepeatedTrades = tradeExecution.Count(t => t.IsRepeatedTrade == true),
                NonRepeatedTrades = tradeExecution.Count(t => t.IsRepeatedTrade == false),
                TotalInvestAmount = (tradeExecution.Count(t => t.IsRepeatedTrade == false)) * 10000,
                PercentRepeatedTrades = (tradeExecution.Count(t => t.IsRepeatedTrade == true) * 100.0 / tradeExecution.Count()),
                RunningProfitLoss = tradeExecution.Where(t => t.InTrade == true).Sum(t => t.CurrentProfitLossOnTrade),
                BookedProfit = tradeExecution.Where(t => t.InTrade == false).Sum(t => t.ProfitLoss),
                RunningTrade = tradeExecution.Count(t => t.InTrade == true),
                ClosedTrade = tradeExecution.Count(t => t.InTrade == false)
            };
            APIServices.SendToTeligrams("Total Trades: " + result.TotalTrades + " Repeated Trades: " + result.RepeatedTrades + " Non Repeated Trades: " + result.NonRepeatedTrades + " Total Invested Amount: " + result.TotalInvestAmount + " Running Profit Loss: " + result.RunningProfitLoss + " Booked Profit: " + result.BookedProfit + " Running Trades: " + result.RunningTrade + " Closed Trades: " + result.ClosedTrade);

        }
    }
}

