using System;
namespace TVBot.Model
{
    public class MCPriceInfo
    {
       public int success { get; set; }
        public List<Data> data { get; set; }
    }
     public class Data
    {
        public string companyName { get; set; }
        public string lastPrice { get; set; }
        public string perChange { get; set; }
        public string marketCap { get; set; }
        public string scTtm { get; set; }
        public string perform1yr { get; set; }
        public string priceBook { get; set; }
    }
}