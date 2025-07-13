using AiTradingDemo.Models;
using Newtonsoft.Json.Linq;

namespace AiTradingDemo.Services;

public class MarketDataService
{
    private readonly HttpClient _httpClient = new();

    public async Task<List<Candle>> GetCandlesAsync()
    {
        string url = "https://api.binance.com/api/v3/klines?symbol=BTCUSDT&interval=1d&limit=100";
        var json = await _httpClient.GetStringAsync(url);
        var jArray = JArray.Parse(json);

        var candles = new List<Candle>();

        foreach (var item in jArray)
        {
            candles.Add(new Candle
            {
                OpenTime = DateTimeOffset.FromUnixTimeMilliseconds((long)item[0]).DateTime,
                Open = decimal.Parse(item[1].ToString()),
                High = decimal.Parse(item[2].ToString()),
                Low = decimal.Parse(item[3].ToString()),
                Close = decimal.Parse(item[4].ToString())
            });
        }

        return candles;
    }
}