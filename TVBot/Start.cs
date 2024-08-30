using Microsoft.Extensions.Logging;
using TVBot.Services.Factory;
using TVBot.Utility;

namespace TVBot
{
    internal static class Start
    {
        public static async Task Begin(ISQLServerServiceFactory tradeOpportunityService, ILogger logger)
        {

            int count = 200;
            var appPath = AppDomain.CurrentDomain.BaseDirectory;
            var queryFolderPath = Path.Combine(appPath, "JsonQuery");
            var macdDQueryFilePath = Path.Combine(queryFolderPath, "macdDQuery.json");
            var ema4HQueryFilePath = Path.Combine(queryFolderPath, "ema4HQuery.json");
            var ema1HQueryFilePath = Path.Combine(queryFolderPath, "ema1HQuery.json");
            var ema15MQueryFilePath = Path.Combine(queryFolderPath, "ema15MQuery.json");
            var macd3MQueryFilePath = Path.Combine(queryFolderPath, "macd3MQuery.json");
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
            try
            {
                await UtiityServices.SendReport(tradeOpportunityService);
                await UtiityServices.UpdateStopLoss(tradeOpportunityService);
                // run the below code in a loop when time is between 9:15 to 15:30
                while (true)
                {
                    var currentTime = DateTime.Now.TimeOfDay;
                    var startTime = new TimeSpan(9, 15, 01);
                    var endTime = new TimeSpan(15, 29, 30);
                    var tradeCurrentTime = DateTime.Now.TimeOfDay;
                    var tradeStartTime = new TimeSpan(11, 00, 01);
                    var tradeEndTime = new TimeSpan(15, 29, 30);

                    if (currentTime >= startTime && currentTime <= endTime && DateTime.Today.DayOfWeek != DayOfWeek.Saturday && DateTime.Today.DayOfWeek != DayOfWeek.Sunday)
                    {
                        if (tradeCurrentTime >= tradeStartTime && tradeCurrentTime <= tradeEndTime)
                        {
                            // Thread.Sleep(10000);
                            UtiityServices.EMA1MReversal(ema1MQueryFilePath, tradeOpportunityService);
                        }
                        if(count % 2 == 0)
                        {
                            await Task.Delay(10000);
                            UtiityServices.MACD_3MReversal(macd3MQueryFilePath, tradeOpportunityService);
                        }
                        if (count % 3 == 0)
                        {
                            await Task.Delay(10000);
                            UtiityServices.EMA5MReversal(ema5MQueryFilePath, tradeOpportunityService);
                        }
                        if (count % 5 == 0)
                        {
                            await Task.Delay(10000);
                            UtiityServices.EMA15MReversal(ema15MQueryFilePath, tradeOpportunityService);
                        }                       
                        if (count % 13 == 0)
                        {
                            await Task.Delay(10000);
                            UtiityServices.EMAOneHourReversal(ema1HQueryFilePath, tradeOpportunityService);
                        }
                        if (count % 17 == 0)
                        {
                            await Task.Delay(10000);
                            UtiityServices.MacdOneHourReversal(macd1HQueryFilePath, tradeOpportunityService);
                        }
                        if (count % 19 == 0)
                        {
                            await Task.Delay(10000);
                            UtiityServices.EMATwoHourReversal(ema2HQueryFilePath, tradeOpportunityService);

                        }
                        if (count % 23 == 0)
                        {
                            await Task.Delay(10000);
                            UtiityServices.EMAFourHourReversal(ema4HQueryFilePath, tradeOpportunityService);

                        }
                        if (count % 29 == 0)
                        {
                            await Task.Delay(10000);
                            UtiityServices.EMAOneDayReversal(emaDQueryFilePath, tradeOpportunityService);
                            await UtiityServices.SendTodayTradeCloseReport(tradeOpportunityService);

                        }
                        if (count % 33 == 0)
                        {
                            await Task.Delay(10000);
                            UtiityServices.MacdOneDayReversal(macdDQueryFilePath, tradeOpportunityService);
                            await UtiityServices.SendTodayTradeExecutinReport(tradeOpportunityService);
                        }
                        if (count % 57 == 0)
                        {
                            await Task.Delay(10000);
                            UtiityServices.EMAOneWeekReversal(emaWQueryFilePath, tradeOpportunityService);
                            await UtiityServices.SendReport(tradeOpportunityService);
                        }
                        if (count % 93 == 0)
                        {
                            await Task.Delay(10000);
                            UtiityServices.WeekDarvasBoxBullish(WeekDarvasBoxBullishQueryFilePath, tradeOpportunityService);
                            await UtiityServices.SendReport(tradeOpportunityService);
                        }
                        if (count % 117 == 0)
                        {
                            await Task.Delay(10000);
                            UtiityServices.AllTimeDarvasBoxBullish(AllTimeDarvasBoxBullishQueryFilePath, tradeOpportunityService);
                            await UtiityServices.SendReport(tradeOpportunityService);                           
                        }




                        // DownCrossing.EMA30MReversal(ema30MQueryFilePathBearish, tradeOpportunityService);



                        // DownCrossing.EMAOneHourReversal(ema1HQueryFilePathBearish, tradeOpportunityService);



                        // DownCrossing.EMATwoHourReversal(ema2HQueryFilePathBearish, tradeOpportunityService);


                        // DownCrossing.EMAFourHourReversal(ema4HQueryFilePathBearish, tradeOpportunityService);


                        // DownCrossing.EMAOneDayReversal(emaDQueryFilePathBearish, tradeOpportunityService);


                        // DownCrossing.MacdOneDayReversal(macdDQueryFilePathBearish, tradeOpportunityService);

                        await Task.Delay(10000);
                        await UtiityServices.GetCurrentPriceAllNSEStockAndCloseOpenTrades(tradeOpportunityService, allNSEStockPriceQuery);
                        //  UtiityServices.OneMin5_9EMADownwardCrossOver(ema1MQueryFilePathBearish, tradeOpportunityService);



                    }
                    await Task.Delay(10000);
                    count++;
                  //  Console.WriteLine("Count: " + count);

                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message.ToString(), ex.InnerException);
            }


        }
    }
}

