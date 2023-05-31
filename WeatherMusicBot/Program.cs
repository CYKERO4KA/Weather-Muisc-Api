using Microsoft.EntityFrameworkCore;
using SpotifyAPI.Web;
using WeatherMusicBot;
using WeatherMusicBot.Services;

var builder = WebApplication.CreateBuilder(args);

IConfiguration _configuration = new ConfigurationManager();
// Add services to the container.
builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(_configuration.GetConnectionString("Db")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<ISpotifyAccountServices, SpotifyAccountService>();
builder.Services.AddHttpClient<IOpenWeatherMapService, OpenWeatherMapClient>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();