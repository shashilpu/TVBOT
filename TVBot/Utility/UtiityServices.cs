﻿using TVBot.Model.Entities;
using TVBot.Models;
using TVBot.Services;
using TVBot.Services.SqlServer;

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
        public static void EMA15MReversal(string ema15MQueryFilePath, ITradeOpportunityService<TradeOpportunity> tradeOpportunityService)
        {
            SearchAndSend(ema15MQueryFilePath,  trackedElementsEMA15Minutes, "15M_EMA", tradeOpportunityService);
        }
        public static void EMA30MReversal(string ema30MQueryFilePath, ITradeOpportunityService<TradeOpportunity> tradeOpportunityService)
        {
            SearchAndSend(ema30MQueryFilePath,  trackedElementsEMA30Minutes, "30M_EMA", tradeOpportunityService);
        }
        public static void EMAOneHourReversal(string ema1HQueryFilePath, ITradeOpportunityService<TradeOpportunity> tradeOpportunityService)
        {
            SearchAndSend(ema1HQueryFilePath,  trackedElementsEMAOneHour, "1H_EMA", tradeOpportunityService);
        }

        public static void EMATwoHourReversal(string ema2HQueryFilePath, ITradeOpportunityService<TradeOpportunity> tradeOpportunityService)
        {
            SearchAndSend(ema2HQueryFilePath,  trackedElementsEMATwoHour, "2H_EMA", tradeOpportunityService);
        }
        public static void EMAFourHourReversal(string ema4HQueryFilePath, ITradeOpportunityService<TradeOpportunity> tradeOpportunityService)
        {
            SearchAndSend(ema4HQueryFilePath,  trackedElementsEMAFourHour, "4H_EMA", tradeOpportunityService);
        }
        public static void EMAOneDayReversal(string emaDQueryFilePath, ITradeOpportunityService<TradeOpportunity> tradeOpportunityService)
        {
            SearchAndSend(emaDQueryFilePath,  trackedElementsEMAOneDay, "1D_EMA", tradeOpportunityService);
        }
        public static void MacdOneDayReversal(string macdDQueryFilePath, ITradeOpportunityService<TradeOpportunity> tradeOpportunityService)
        {
            SearchAndSend(macdDQueryFilePath,  trackedElementsMACDOneDay, "1D_MACD", tradeOpportunityService);
        }
        private static void SearchAndSend(string queryPath, Dictionary<string, decimal> trackedElements, string algoName, ITradeOpportunityService<TradeOpportunity> tradeOpportunityService)
        {
            SearchResponse res = APIServices.Screener(queryPath).Result;
            if (res.totalCount > 0)
            {
                foreach (var ticker in res.data)
                {
                    if (!trackedElements.ContainsKey(ticker.s))
                    {                        
                        var tickerName = ticker.s;
                        var price = decimal.Parse(ticker.d[6]?.ToString());
                        var change = decimal.Parse(ticker.d[12]?.ToString().Substring(0, 3));
                        var volume = decimal.Parse(ticker.d[13].ToString()) / 1000000;
                        trackedElements.Add(tickerName, price);
                        var Message = algoName + "-- " + tickerName + " P.=" + price + " C.=" + change + "% V.= " + volume + " M T.= " +  trackedElements.Count;
                        APIServices.SendToTeligrams(Message);
                        tradeOpportunityService.AddTradeOpportunity(new TradeOpportunity
                        {
                            CrossOverDateTime = DateTime.Now,
                            Ticker = tickerName,
                            PercentChange = change,
                            Price = price,
                            AlgoName = algoName,
                            Volume = volume
                        });                       
                    }
                }
            }
        }
    }
}
