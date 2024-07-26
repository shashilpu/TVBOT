using Microsoft.Extensions.Logging;
using TVBot.Services.Factory;
using TVBot.Utility;

namespace TVBot
{
    internal static class Start
    {
        public static async void Begin(ISQLServerServiceFactory tradeOpportunityService, ILogger logger)
        {

            int count = 400;
            var appPath = AppDomain.CurrentDomain.BaseDirectory;
            var queryFolderPath = Path.Combine(appPath, "JsonQuery");
            var macdDQueryFilePath = Path.Combine(queryFolderPath, "macdDQuery.json");
            var ema4HQueryFilePath = Path.Combine(queryFolderPath, "ema4HQuery.json");
            var ema1HQueryFilePath = Path.Combine(queryFolderPath, "ema1HQuery.json");
            var ema15MQueryFilePath = Path.Combine(queryFolderPath, "ema15MQuery.json");
            var ema30MQueryFilePath = Path.Combine(queryFolderPath, "ema30MQuery.json");
            var ema2HQueryFilePath = Path.Combine(queryFolderPath, "ema2HQuery.json");
            var emaDQueryFilePath = Path.Combine(queryFolderPath, "emaDQuery.json");
            var emaWQueryFilePath = Path.Combine(queryFolderPath, "emaWBullishQuery.json");
            var macd1HQueryFilePath = Path.Combine(queryFolderPath, "macd1HBullishQuery.json");
            var WeekDarvasBoxBullishQueryFilePath = Path.Combine(queryFolderPath, "WeekDarvasBoxBullishQuery.json");
            var AllTimeDarvasBoxBullishQueryFilePath = Path.Combine(queryFolderPath, "AllTimeDarvasBoxBullishQuery.json");
            var ema1MQueryFilePath = Path.Combine(queryFolderPath, "ema1MBullishQuery.json");
            var ema5MQueryFilePath = Path.Combine(queryFolderPath, "ema5MBullishQuery.json");
            //Bearish Query Path
            var BearishQueryFilePath = Path.Combine(queryFolderPath, "BearishCross");
            var ema1MQueryFilePathBearish = Path.Combine(BearishQueryFilePath, "ema1MBearishQuery.json");
            var macdDQueryFilePathBearish = Path.Combine(BearishQueryFilePath, "macdDQuery.json");
            var ema4HQueryFilePathBearish = Path.Combine(BearishQueryFilePath, "ema4HQuery.json");
            var ema1HQueryFilePathBearish = Path.Combine(BearishQueryFilePath, "ema1HQuery.json");
            var ema15MQueryFilePathBearish = Path.Combine(BearishQueryFilePath, "ema15MQuery.json");
            var ema30MQueryFilePathBearish = Path.Combine(BearishQueryFilePath, "ema30MQuery.json");
            var ema2HQueryFilePathBearish = Path.Combine(BearishQueryFilePath, "ema2HQuery.json");
            var emaDQueryFilePathBearish = Path.Combine(BearishQueryFilePath, "emaDQuery.json");

            var allNSEStockQueryFilePath = Path.Combine(queryFolderPath, "AllNSEStock");
            var allNSEStockPriceQuery = Path.Combine(allNSEStockQueryFilePath, "allNSEStockPriceQuery.json");

            // run the below code in a loop when time is between 9:15 to 15:30
            while (true)
            {
                var currentTime = DateTime.Now.TimeOfDay;
                var startTime = new TimeSpan(9, 08, 0);
                var endTime = new TimeSpan(15, 30, 0);
                var tradeCurrentTime = DateTime.Now.TimeOfDay;
                var tradeStartTime = new TimeSpan(9, 15, 10);
                var tradeEndTime = new TimeSpan(14, 15, 0);
                try
                {                    

                    if (currentTime >= startTime && currentTime <= endTime)
                    {
                        UtiityServices.GetCurrentPriceAllNSEStockAndCloseOpenTrades(tradeOpportunityService, allNSEStockPriceQuery);

                        if (count % 2 == 0)
                        {
                            UtiityServices.EMA5MReversal(ema5MQueryFilePath, tradeOpportunityService);
                        }
                        if (count % 3 == 0)
                        {
                            UtiityServices.EMA15MReversal(ema15MQueryFilePath, tradeOpportunityService);
                        }

                        if (count % 4 == 0)
                        {
                            UtiityServices.EMA30MReversal(ema30MQueryFilePath, tradeOpportunityService);
                        }
                        if (count % 5 == 0)
                        {
                            UtiityServices.EMAOneHourReversal(ema1HQueryFilePath, tradeOpportunityService);
                        }
                        if (count % 6 == 0)
                        {
                            UtiityServices.MacdOneHourReversal(macd1HQueryFilePath, tradeOpportunityService);
                        }
                        if (count % 7 == 0)
                        {
                            UtiityServices.EMATwoHourReversal(ema2HQueryFilePath, tradeOpportunityService);

                        }
                        if (count % 8 == 0)
                        {
                            UtiityServices.EMAFourHourReversal(ema4HQueryFilePath, tradeOpportunityService);

                        }
                        if (count % 9 == 0)
                        {
                            UtiityServices.EMAOneDayReversal(emaDQueryFilePath, tradeOpportunityService);

                        }
                        if (count % 10 == 0)
                        {
                            UtiityServices.MacdOneDayReversal(macdDQueryFilePath, tradeOpportunityService);
                        }
                        if (count % 60 == 0)
                        {
                            UtiityServices.EMAOneWeekReversal(emaWQueryFilePath, tradeOpportunityService);
                        }
                        if (count % 120 == 0)
                        {
                            UtiityServices.WeekDarvasBoxBullish(WeekDarvasBoxBullishQueryFilePath, tradeOpportunityService);
                        }
                        if (count % 180 == 0)
                        {
                            UtiityServices.AllTimeDarvasBoxBullish(AllTimeDarvasBoxBullishQueryFilePath, tradeOpportunityService);
                        }




                        // DownCrossing.EMA30MReversal(ema30MQueryFilePathBearish, tradeOpportunityService);



                        // DownCrossing.EMAOneHourReversal(ema1HQueryFilePathBearish, tradeOpportunityService);



                        // DownCrossing.EMATwoHourReversal(ema2HQueryFilePathBearish, tradeOpportunityService);


                        // DownCrossing.EMAFourHourReversal(ema4HQueryFilePathBearish, tradeOpportunityService);


                        // DownCrossing.EMAOneDayReversal(emaDQueryFilePathBearish, tradeOpportunityService);


                        // DownCrossing.MacdOneDayReversal(macdDQueryFilePathBearish, tradeOpportunityService);
                        if (tradeCurrentTime >= tradeStartTime && tradeCurrentTime <= tradeEndTime)
                        {
                            UtiityServices.EMA1MReversal(ema1MQueryFilePath, tradeOpportunityService);
                          
                        }
                      //  UtiityServices.GetCurrentPriceAllNSEStockAndCloseOpenTrades(tradeOpportunityService, allNSEStockPriceQuery);
                      //  UtiityServices.OneMin5_9EMADownwardCrossOver(ema1MQueryFilePathBearish, tradeOpportunityService);


                    }

                }
                catch (Exception ex)
                {
                    logger.LogError(ex, ex.Message.ToString());
                }
                count++;
                Thread.Sleep(10000);
                Console.WriteLine(count);
            }
        }
    }
}
