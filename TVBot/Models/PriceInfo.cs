using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVBot.Models
{
    public class LatestPrice
    {
        public string companyName { get; set; }
        public string lastPrice { get; set; }
        public string perChange { get; set; }
        public string marketCap { get; set; }
        public string scTtm { get; set; }
        public string perform1yr { get; set; }
        public string priceBook { get; set; }
    }

    public class PriceInfo
    {
        public int success { get; set; }
        public List<LatestPrice> data { get; set; }
    }
}
