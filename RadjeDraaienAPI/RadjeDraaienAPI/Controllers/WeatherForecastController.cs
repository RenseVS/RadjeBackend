using Microsoft.AspNetCore.Mvc;

namespace RadjeDraaienAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly SocketService _socketService;
    private readonly WheelData _wheelData;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, SocketService socketService, WheelData wheelData)
    {
        _logger = logger;
        _socketService = socketService;
        _wheelData = wheelData;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        _socketService.SpinWheel(1);
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpGet("{id}")]
    public int Spin(int id)
    {
        _socketService.SpinWheel(id);
        _wheelData.resultColor = "";
        return id;
    }
    [HttpPost("PostResult")]
    public string Post([FromBody] Result result)
    {
        _wheelData.resultColor = result.Color;
        return _wheelData.resultColor;
    }

    [HttpGet("GetResult")]
    public Result getResult()
    {
        Result color = new Result(_wheelData.resultColor);
        return color;
    }
}

