using TVBot.Models;
using TVBot.Services;

namespace TVBot.Utility
{
    public static class UtiityServices
    {
        static int totalEMA15Minutes = 0;
        static Dictionary<string, string> trackedElementsEMA15Minutes = new Dictionary<string, string>();
        static int totalEMA30Minutes = 0;
        static Dictionary<string, string> trackedElementsEMA30Minutes = new Dictionary<string, string>();
        static int totalEMAOneHour = 0;
        static Dictionary<string, string> trackedElementsEMAOneHour = new Dictionary<string, string>();
        static int totalEMATwoHour = 0;
        static Dictionary<string, string> trackedElementsEMATwoHour = new Dictionary<string, string>();
        static int totalEMAFourHour = 0;
        static Dictionary<string, string> trackedElementsEMAFourHour = new Dictionary<string, string>();
        static int totalEMAOneDay = 0;
        static Dictionary<string, string> trackedElementsEMAOneDay = new Dictionary<string, string>();
        static int totalMACDOneDay = 0;
        static Dictionary<string, string> trackedElementsMACDOneDay = new Dictionary<string, string>();
        public static void EMA15MReversal(string ema15MQueryFilePath)
        {
            SearchAndSend(ema15MQueryFilePath, totalEMA15Minutes, trackedElementsEMA15Minutes, "15M_EMA");
        }
        public static void EMA30MReversal(string ema30MQueryFilePath)
        {
            SearchAndSend(ema30MQueryFilePath, totalEMA30Minutes, trackedElementsEMA30Minutes, "30M_EMA");
        }
        public static void EMAOneHourReversal(string ema1HQueryFilePath)
        {
            SearchAndSend(ema1HQueryFilePath, totalEMAOneHour, trackedElementsEMAOneHour, "1H_EMA");
        }

        public static void EMATwoHourReversal(string ema2HQueryFilePath)
        {
            SearchAndSend(ema2HQueryFilePath, totalEMATwoHour, trackedElementsEMATwoHour, "2H_EMA");
        }
        public static void EMAFourHourReversal(string ema4HQueryFilePath)
        {
            SearchAndSend(ema4HQueryFilePath, totalEMAFourHour, trackedElementsEMAFourHour, "4H_EMA");
        }
        public static void EMAOneDayReversal(string emaDQueryFilePath)
        {
            SearchAndSend(emaDQueryFilePath, totalEMAOneDay, trackedElementsEMAOneDay, "1D_EMA");
        }
        public static void MacdOneDayReversal(string macdDQueryFilePath)
        {
            SearchAndSend(macdDQueryFilePath, totalMACDOneDay, trackedElementsMACDOneDay, "1D_MACD");
        }
        private static void SearchAndSend(string queryPath, int total, Dictionary<string, string> trackedElements, string algoName)
        {
            SearchResponse res = APIServices.Screener(queryPath).Result;
            if (res.totalCount > 0)
            {               
                foreach (var ticker in res.data)
                {
                    if (!trackedElements.ContainsKey(ticker.s))
                    {
                        total++;
                        var tickerName = ticker.s;
                        var price = ticker.d[6]?.ToString();
                        var change = ticker.d[12]?.ToString().Substring(0, 5);
                        var volume = decimal.Parse(ticker.d[13].ToString()) / 1000000;
                        trackedElements.Add(tickerName, price);
                        var Message = algoName + "-- " + tickerName + " P.=" + price + " C.=" + change + "% V.= " + volume + " M. T.= "+total+".."+trackedElements.Count;
                        APIServices.SendToTeligrams(Message);
                        
                    }
                }
            }
        }
    }
}
