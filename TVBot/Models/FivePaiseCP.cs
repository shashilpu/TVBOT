using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVBot.Models
{
    public class FivePaiseCP
    {
        public Stocks stocks { get; set; }
        public string method { get; set; }
        public int status { get; set; }
    }
    public class Stocks
    {
        public string price { get; set; }
        public string d_price { get; set; }
        public string color { get; set; }
        public string color_icon { get; set; }
        public double change { get; set; }
        public double change_p { get; set; }
        public string stock_time { get; set; }
    }
}
