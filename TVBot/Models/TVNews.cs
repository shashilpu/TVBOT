using Newtonsoft.Json;
using System;
namespace TVBot.Model
{    
    public class Item
    {
        public string id { get; set; }
        public string title { get; set; }
        public string provider { get; set; }
        public string sourceLogoId { get; set; }
        public int published { get; set; }
        public string source { get; set; }
        public int urgency { get; set; }
        public string permission { get; set; }
        public List<RelatedSymbol> relatedSymbols { get; set; }
        public string storyPath { get; set; }
        public string link { get; set; }
    }

    public class RelatedSymbol
    {
        public string symbol { get; set; }
        public string logoid { get; set; }

        [JsonProperty("currency-logoid")]
        public string currencylogoid { get; set; }

        [JsonProperty("base-currency-logoid")]
        public string basecurrencylogoid { get; set; }
    }

    public class TVNews
    {
        public List<Item> items { get; set; }
        public Streaming streaming { get; set; }
        public List<Section> sections { get; set; }
    }

    public class Section
    {
        public string id { get; set; }
        public string title { get; set; }
    }

    public class Streaming
    {
        public string channel { get; set; }
    }


}
