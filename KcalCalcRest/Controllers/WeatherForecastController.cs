using Microsoft.AspNetCore.Mvc;
using KcalCalcRest.Data;

namespace KcalCalcRest.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase {
	private static readonly string[] Summaries = new[]
	{
		"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	};

	private readonly ILogger<WeatherForecastController> _logger;
	private readonly ApplicationDbContext _dbContext;

	public WeatherForecastController(ILogger<WeatherForecastController> logger, ApplicationDbContext dbContext) {
		_logger = logger;
		_dbContext = dbContext;
	}

	[HttpGet(Name = "GetWeatherForecast")]
	public IEnumerable<WeatherForecast> Get() {
		return Enumerable.Range(1, 5).Select(index => new WeatherForecast {
			Date = DateTime.Now.AddDays(index),
			TemperatureC = Random.Shared.Next(-20, 55),
			Summary = Summaries[Random.Shared.Next(Summaries.Length)]
		})
		.ToArray();
	}
}