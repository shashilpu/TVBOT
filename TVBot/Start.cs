using TVBot.Services.Factory;
using TVBot.Utility;

namespace TVBot
{
    internal static class Start
    {
        public static async void Begin(ISQLServerServiceFactory tradeOpportunityService)
        {
            int count = 0;
            var appPath = AppDomain.CurrentDomain.BaseDirectory;
            var queryFolderPath = Path.Combine(appPath, "JsonQuery");
            var macdDQueryFilePath = Path.Combine(queryFolderPath, "macdDQuery.json");
            var ema4HQueryFilePath = Path.Combine(queryFolderPath, "ema4HQuery.json");
            var ema1HQueryFilePath = Path.Combine(queryFolderPath, "ema1HQuery.json");
            var ema15MQueryFilePath = Path.Combine(queryFolderPath, "ema15MQuery.json");
            var ema30MQueryFilePath = Path.Combine(queryFolderPath, "ema30MQuery.json");
            var ema2HQueryFilePath = Path.Combine(queryFolderPath, "ema2HQuery.json");
            var emaDQueryFilePath = Path.Combine(queryFolderPath, "emaDQuery.json");
            //Bearish Query Path
            var BearishQueryFilePath = Path.Combine(queryFolderPath, "BearishCross");
            var macdDQueryFilePathBearish = Path.Combine(BearishQueryFilePath, "macdDQuery.json");
            var ema4HQueryFilePathBearish = Path.Combine(BearishQueryFilePath, "ema4HQuery.json");
            var ema1HQueryFilePathBearish = Path.Combine(BearishQueryFilePath, "ema1HQuery.json");
            var ema15MQueryFilePathBearish = Path.Combine(BearishQueryFilePath, "ema15MQuery.json");
            var ema30MQueryFilePathBearish = Path.Combine(BearishQueryFilePath, "ema30MQuery.json");
            var ema2HQueryFilePathBearish = Path.Combine(BearishQueryFilePath, "ema2HQuery.json");
            var emaDQueryFilePathBearish = Path.Combine(BearishQueryFilePath, "emaDQuery.json");

            while (true)
            {
                UtiityServices.EMA15MReversal(ema15MQueryFilePath, tradeOpportunityService);
                Thread.Sleep(10000);
                DownCrossing.EMA15MReversal(ema15MQueryFilePathBearish, tradeOpportunityService);
                Thread.Sleep(10000);
                UtiityServices.EMA30MReversal(ema30MQueryFilePath, tradeOpportunityService);
                Thread.Sleep(10000);
               // DownCrossing.EMA30MReversal(ema30MQueryFilePathBearish, tradeOpportunityService);
              //  Thread.Sleep(10000);
                UtiityServices.EMAOneHourReversal(ema1HQueryFilePath, tradeOpportunityService);
                Thread.Sleep(10000);
              //  DownCrossing.EMAOneHourReversal(ema1HQueryFilePathBearish, tradeOpportunityService);
//Thread.Sleep(10000);
                UtiityServices.EMATwoHourReversal(ema2HQueryFilePath, tradeOpportunityService);
                Thread.Sleep(10000);
              //  DownCrossing.EMATwoHourReversal(ema2HQueryFilePathBearish, tradeOpportunityService);
              //  Thread.Sleep(10000);
                UtiityServices.EMAFourHourReversal(ema4HQueryFilePath, tradeOpportunityService);
                Thread.Sleep(10000);
               // DownCrossing.EMAFourHourReversal(ema4HQueryFilePathBearish, tradeOpportunityService);
              //  Thread.Sleep(10000);
                UtiityServices.EMAOneDayReversal(emaDQueryFilePath, tradeOpportunityService);
                Thread.Sleep(10000);
              //  DownCrossing.EMAOneDayReversal(emaDQueryFilePathBearish, tradeOpportunityService);
              //  Thread.Sleep(10000);
                UtiityServices.MacdOneDayReversal(macdDQueryFilePath, tradeOpportunityService);
                Thread.Sleep(10000);
              //  DownCrossing.MacdOneDayReversal(macdDQueryFilePathBearish, tradeOpportunityService);
              //  Thread.Sleep(10000);
                await Status.FetchCurrentPriceAsync(tradeOpportunityService);
                count++;
                Console.WriteLine(count);
            }
        }
    }
}
