using System;
namespace TVBot.Models
{
    public class SearchResponse
    {
        public int? totalCount { get; set; }
        public List<Data>? data { get; set; }
    }

    public class Data
    {
        public string? s { get; set; }
        public List<object>? d { get; set; }
    }
}
