using Microsoft.EntityFrameworkCore;
using WeatherMusicBot.Entity;

namespace WeatherMusicBot;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }
    
    public DbSet<User> Users { get; set; }
}