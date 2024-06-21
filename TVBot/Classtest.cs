using System;
using System.IO.Compression;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TVBot.Models.nse;

public class Classtest
{
    public static async Task Mains()
    {
        Console.WriteLine("Hello World!");
        using var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, "https://www.nseindia.com/api/quote-equity?symbol=RELIANCE");

        // Add necessary headers
        request.Headers.Add("Accept", "*/*");
        request.Headers.Add("Accept-Encoding", "gzip, deflate, br, zstd");
        request.Headers.Add("Accept-Language", "en-US,en;q=0.9");
        request.Headers.Add("Referer", "https://www.nseindia.com/get-quotes/equity?symbol=RELIANCE");
        request.Headers.Add("Sec-Ch-Ua", "\"Not/A)Brand\";v=\"8\", \"Chromium\";v=\"126\", \"Google Chrome\";v=\"126\"");
        request.Headers.Add("Sec-Ch-Ua-Mobile", "?0");
        request.Headers.Add("Sec-Ch-Ua-Platform", "\"Windows\"");
        request.Headers.Add("Sec-Fetch-Dest", "empty");
        request.Headers.Add("Sec-Fetch-Mode", "cors");
        request.Headers.Add("Sec-Fetch-Site", "same-origin");
        request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36");

        // Add necessary cookies
        request.Headers.Add("Cookie", "_ga=GA1.1.570382899.1717152815; _ga_QJZ4447QD3=GS1.1.1717400990.7.0.1717400990.0.0.0; AKA_A2=A; _abck=B53385A7A73D8C6153A2D5AC484F29D9~0~YAAQf8MzuGvMvwiQAQAATJfkOgxZTTKZvzKQuFuwNArofNXSZE2Y6lVn3HRPDKvdOfGxFd2wLrynBKUHWBeSfJLLoqznLSs3p22CPIEYwZSwnbPmD1U2ic1eK909iOv1TE+Tij3rwDOWY+J/vctBmTmIyb7kd4freRC3ZVTE2mK22RnH2tYlCBoI89G39nVMjPV4S+R3RZTmmkhL2e+vo0E9wSJSO6+3BmUIS4iG9qt8OvIbVwupMhulOdqWaBNWxL7qzXqpyPIoNDmIiaTiWwOcF8KggKzJkWhRTGPomjYi9cFqQJOpqWsM+ycy4B3D4F8AiwvJUr4l/cws5uOngIbOUhXgLCj3G7F90iWjVmrRVpMwwg5IsqjFJp6/FUhvCx5g859FATUc+tN2wfhpm/Ea8XdPWgp4WqI=~-1~-1~-1; defaultLang=en; nseQuoteSymbols=[{\"symbol\":\"RELIANCE\",\"identifier\":null,\"type\":\"equity\"}]; nsit=ZyV3BA0ZT0-4-pKaAC9NFxZo; nseappid=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJhcGkubnNlIiwiYXVkIjoiYXBpLm5zZSIsImlhdCI6MTcxODk3NDk5MSwiZXhwIjoxNzE4OTgyMTkxfQ.QgBF9LXhtI05ISVpuIECJfLJKKmG0jXuJ4lYl-oNm90;");

        var response = await client.SendAsync(request);
       
        if (response.IsSuccessStatusCode)
        {
            if (response.Content.Headers.ContentEncoding.Any(e => e == "gzip" || e == "deflate"))
            {
                using var decompressionStream = new GZipStream(await response.Content.ReadAsStreamAsync(), CompressionMode.Decompress);
                using var streamReader = new StreamReader(decompressionStream);
                var decompressedContent = await streamReader.ReadToEndAsync();
                var result = JsonSerializer.Deserialize<Root>(decompressedContent);
                Console.WriteLine(result.industryInfo.industry);
                Console.WriteLine(result.priceInfo.lastPrice);
                 Console.WriteLine(result.priceInfo.pChange);
                
            }
            else
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var user = JsonSerializer.Deserialize<Root>(responseContent);
                Console.WriteLine(user.industryInfo.industry);
            }
            Console.WriteLine("response success");
        }
        else
        {
            Console.WriteLine("response failed");
            Console.WriteLine($"Error: {response.StatusCode}");
        }
    }

    private static JsonSerializerOptions GetJsonSerializerOptions()
    {
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        return options;
    }
}

