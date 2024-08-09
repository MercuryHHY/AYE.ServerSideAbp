using AYE.Abp.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AYE.ServerSideAbp.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly JwtTokenService _tokenService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, JwtTokenService tokenService)
        {
            _logger = logger;
            _tokenService = tokenService;
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<WeatherForecast> GetWeatherForecastTest1()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }



        [HttpPost("token")]
        public IActionResult GetToken([FromBody] LoginModel loginModel)
        {
            // 验证用户登录信息
            if (IsValidUser(loginModel))
            {
                var token = _tokenService.GenerateToken(loginModel.UserId);
                return Ok(new { Token = token });
            }
            return Unauthorized();
        }

        private bool IsValidUser(LoginModel loginModel)
        {
            // 实现用户验证逻辑
            return true;
        }


    }
}
