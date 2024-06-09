using TVBot.Utility;

namespace TVBot
{
    internal static class Start
    {
        public static void Begin()
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

            while (true)
            {               
                UtiityServices.EMA15MReversal(ema15MQueryFilePath);
                Thread.Sleep(10000);
                UtiityServices.EMA30MReversal(ema30MQueryFilePath);
                Thread.Sleep(10000);
                UtiityServices.EMAOneHourReversal(ema1HQueryFilePath);
                Thread.Sleep(10000);
                UtiityServices.EMATwoHourReversal(ema2HQueryFilePath);
                Thread.Sleep(10000);
                UtiityServices.EMAFourHourReversal(ema4HQueryFilePath);
                Thread.Sleep(10000);
                UtiityServices.EMAOneDayReversal(emaDQueryFilePath);
                Thread.Sleep(10000);
                UtiityServices.MacdOneDayReversal(macdDQueryFilePath);
                Thread.Sleep(10000);
                count++;
                Console.WriteLine(count);
            }
        }
    }
}
