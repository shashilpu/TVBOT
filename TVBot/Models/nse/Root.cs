namespace TVBot.Models.nse
{
    public class Ato
    {
        public int buy { get; set; }
        public int sell { get; set; }
    }

    public class IndustryInfo
    {
        public string macro { get; set; }
        public string sector { get; set; }
        public string industry { get; set; }
        public string basicIndustry { get; set; }
    }

    public class Info
    {
        public string symbol { get; set; }
        public string companyName { get; set; }
        public string industry { get; set; }
        public List<string> activeSeries { get; set; }
        public List<object> debtSeries { get; set; }
        public bool isFNOSec { get; set; }
        public bool isCASec { get; set; }
        public bool isSLBSec { get; set; }
        public bool isDebtSec { get; set; }
        public bool isSuspended { get; set; }
        public List<object> tempSuspendedSeries { get; set; }
        public bool isETFSec { get; set; }
        public bool isDelisted { get; set; }
        public string isin { get; set; }
        public bool isMunicipalBond { get; set; }
        public bool isTop10 { get; set; }
        public string identifier { get; set; }
    }

    public class IntraDayHighLow
    {
        public double min { get; set; }
        public double max { get; set; }
        public int value { get; set; }
    }

    public class Metadata
    {
        public string series { get; set; }
        public string symbol { get; set; }
        public string isin { get; set; }
        public string status { get; set; }
        public string listingDate { get; set; }
        public string industry { get; set; }
        public string lastUpdateTime { get; set; }
        public double pdSectorPe { get; set; }
        public double pdSymbolPe { get; set; }
        public string pdSectorInd { get; set; }
    }

    public class Preopen
    {
        public double price { get; set; }
        public int buyQty { get; set; }
        public int sellQty { get; set; }
        public bool? iep { get; set; }
    }

    public class PreOpenMarket
    {
        public List<Preopen> preopen { get; set; }
        public Ato ato { get; set; }
        public double IEP { get; set; }
        public int totalTradedVolume { get; set; }
        public double finalPrice { get; set; }
        public int finalQuantity { get; set; }
        public string lastUpdateTime { get; set; }
        public int totalBuyQuantity { get; set; }
        public int totalSellQuantity { get; set; }
        public int atoBuyQty { get; set; }
        public int atoSellQty { get; set; }
        public double Change { get; set; }
        public double perChange { get; set; }
        public double prevClose { get; set; }
    }

    public class PriceInfo
    {
        public int lastPrice { get; set; }
        public double change { get; set; }
        public double pChange { get; set; }
        public double previousClose { get; set; }
        public double open { get; set; }
        public double close { get; set; }
        public double vwap { get; set; }
        public string lowerCP { get; set; }
        public string upperCP { get; set; }
        public string pPriceBand { get; set; }
        public double basePrice { get; set; }
        public IntraDayHighLow intraDayHighLow { get; set; }
        public WeekHighLow weekHighLow { get; set; }
        public object iNavValue { get; set; }
        public bool checkINAV { get; set; }
    }

    public class Root
    {
        public Info info { get; set; }
        public Metadata metadata { get; set; }
        public SecurityInfo securityInfo { get; set; }
        public SddDetails sddDetails { get; set; }
        public PriceInfo priceInfo { get; set; }
        public IndustryInfo industryInfo { get; set; }
        public PreOpenMarket preOpenMarket { get; set; }
    }

    public class SddDetails
    {
        public string SDDAuditor { get; set; }
        public string SDDStatus { get; set; }
    }

    public class SecurityInfo
    {
        public string boardStatus { get; set; }
        public string tradingStatus { get; set; }
        public string tradingSegment { get; set; }
        public string sessionNo { get; set; }
        public string slb { get; set; }
        public string classOfShare { get; set; }
        public string derivatives { get; set; }
        public Surveillance surveillance { get; set; }
        public int faceValue { get; set; }
        public long issuedSize { get; set; }
    }

    public class Surveillance
    {
        public object surv { get; set; }
        public object desc { get; set; }
    }

    public class WeekHighLow
    {
        public double min { get; set; }
        public string minDate { get; set; }
        public int max { get; set; }
        public string maxDate { get; set; }
        public int value { get; set; }
    }
}

