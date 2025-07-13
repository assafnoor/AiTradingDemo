// Indicators/MovingAverage.cs
using AiTradingDemo.Models;

namespace AiTradingDemo.Indicators;

public class MovingAverage
{
    public static void Calculate(List<Candle> candles, int period = 14)
    {
        for (int i = period - 1; i < candles.Count; i++)
        {
            var avg = candles.Skip(i - period + 1).Take(period).Average(c => c.Close);
            candles[i].MovingAverage = avg;
        }
    }
}