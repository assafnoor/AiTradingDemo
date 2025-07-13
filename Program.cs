using AiTradingDemo.Data;
using AiTradingDemo.Indicators;
using AiTradingDemo.Services;

internal class Program
{
    private static async Task Main(string[] args)
    {
        using var db = new AppDbContext();
        db.Database.EnsureDeletedAsync();
        db.Database.EnsureCreated();
        Console.WriteLine("📁 DB path: " + Path.Combine(Directory.GetCurrentDirectory(), "marketdata.db"));

        var marketDataService = new MarketDataService();
        var candles = await marketDataService.GetCandlesAsync();

        MovingAverage.Calculate(candles);

        foreach (var candle in candles)
        {
            db.Candles.Add(candle);
        }

        db.SaveChanges();
        var predictor = new PricePredictionService();
        predictor.TrainAndPredict(candles);
        Console.WriteLine("✅ Done. Candles saved with moving averages.");
    }
}