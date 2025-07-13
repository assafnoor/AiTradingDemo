namespace AiTradingDemo.Models;

public class Candle
{
    public int Id { get; set; }
    public DateTime OpenTime { get; set; }
    public decimal Open { get; set; }
    public decimal High { get; set; }
    public decimal Low { get; set; }
    public decimal Close { get; set; }

    public decimal MovingAverage { get; set; }
    public float RSI { get; set; }
}