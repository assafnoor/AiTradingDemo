using AiTradingDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace AiTradingDemo.Data;

public class AppDbContext : DbContext
{
    public DbSet<Candle> Candles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=marketdata.db");
}