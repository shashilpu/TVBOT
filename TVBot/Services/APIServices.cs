using Serilog;
using System.Text;
using System.Text.Json;
using TVBot.Model;
using TVBot.Models;


namespace TVBot.Services
{
    public static class APIServices
    {
        private static readonly string botToken = "7485766478:BBE9SU9K15g09fTE3iAwQdQWz_5ADyvMoG4";
        private static readonly string chatId = "6183697962";
        public static async Task<SearchResponse> Screener(string queryFilePath)
        {
            SearchResponse searchResponse = new SearchResponse();
            var jsonContent = File.ReadAllText(queryFilePath);
            using (HttpClient client = new HttpClient())
            {
                // Set the headers as required
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Origin", "https://in.tradingview.com");
                client.DefaultRequestHeaders.Add("Referer", "https://in.tradingview.com/");
                client.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-site");
                client.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
                client.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/125.0.0.0 Safari/537.36 Edg/125.0.0.0");

                // Set the content type of the request
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                try
                {
                    // Make the POST request
                    HttpResponseMessage response = await client.PostAsync("https://scanner.tradingview.com/india/scan", content);
                    // Ensure we got a successful response
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Error: {response.StatusCode}");

                    }

                    // Read the response content as a string
                    // string results = await response.Content.ReadAsStringAsync();

                    searchResponse = await JsonSerializer.DeserializeAsync<SearchResponse>(response.Content.ReadAsStream(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                }
                catch (Exception ex)
                {
                    Log.Logger.Error(ex, ex.Message);
                }
                return searchResponse;

            }
        }
        // Send message to telegram using Post Working by 30-05-2024
        public static async void SendToTeligram(string name)
        {
            string chatId = "6583697962";
            Message ms = new Message { chat_id = chatId, text = name };
            var jsonContent = JsonSerializer.Serialize(ms);
            using (HttpClient client = new HttpClient())
            {
                // Set the headers as required
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Origin", "https://in.tradingview.com");
                client.DefaultRequestHeaders.Add("Referer", "https://in.tradingview.com/");


                // Set the content type of the request
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                try
                {

                    HttpResponseMessage response = client.PostAsync("https://api.telegram.org/bot7481766478:AAE9SU9K15g09fTE3iAwQdQWz_5ADyvMoG4/sendMessage", content).Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Error: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Log.Logger.Error(ex, ex.Message);

                }



            }
        }
        // Get is generally faster compared to post as in case of post we need to send the data in the body of the request
        //So using Get to send the message to telegram
        public static async void SendToTeligrams(string message)
        {


            string apiUrl = $"https://api.telegram.org/bot{botToken}/sendMessage?chat_id={chatId}&text={Uri.EscapeDataString(message)}&parse_mode=HTML";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Origin", "https://in.tradingview.com");
                client.DefaultRequestHeaders.Add("Referer", "https://in.tradingview.com/");
                try
                {
                    HttpResponseMessage response = client.GetAsync(apiUrl).Result;
                    response.EnsureSuccessStatusCode();
                    //  string responseBody = await response.Content.ReadAsStringAsync();
                    // Console.WriteLine($"Message sent successfully: {responseBody}");
                }
                catch (Exception ex)
                {
                    Log.Logger.Error(ex, ex.Message);
                    SendToTeligram(ex.Message);
                }
            }
        }
        // write a get method to get the data from the api url is https://www.nseindia.com/api/quote-equity?symbol=CIPLA
        public static async Task<TVNews> GetCurrentNews(string url)
        {
          
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Origin", "https://moneycontrol.com");
                client.DefaultRequestHeaders.Add("Referer", "https://moneycontrol.com/");
                try
                {
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    response.EnsureSuccessStatusCode();
                    //string responseBody = await response.Content.ReadAsStringAsync();
                    //return responseBody;
                    var searchResponse = await JsonSerializer.DeserializeAsync<TVNews>(response.Content.ReadAsStream(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return searchResponse;
                }
                catch (Exception ex)
                {
                    Log.Logger.Error(ex, ex.Message);
                    return null;
                }
            }
        }
        public static async Task<FivePaiseCP> GetCurrentPriceFromFivePaise(string tickerName)
        {
            string url = $"https://www.5paisa.com/update-stock-info/{tickerName}";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                //  client.DefaultRequestHeaders.Add("Origin", "https://moneycontrol.com");
                //  client.DefaultRequestHeaders.Add("Referer", "https://moneycontrol.com/");
                try
                {
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    response.EnsureSuccessStatusCode();
                    //string responseBody = await response.Content.ReadAsStringAsync();
                    //return responseBody;
                    var searchResponse = await JsonSerializer.DeserializeAsync<FivePaiseCP>(response.Content.ReadAsStream(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return searchResponse;
                }
                catch (Exception ex)
                {
                    Log.Logger.Error(ex, ex.Message);
                    return null;
                }
            }
        }
        public class Message
        {
            public string chat_id { get; set; }
            public string text { get; set; }
        }
    }
}
