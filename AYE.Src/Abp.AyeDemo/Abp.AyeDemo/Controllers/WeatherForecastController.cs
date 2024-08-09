using AyeDemo.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using Volo.Abp.Domain.Repositories;

namespace Abp.AyeDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IRepository<BookAggregateRoot, Guid> _repository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IRepository<BookAggregateRoot, Guid> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        //[Authorize]
        [HttpPost(Name = "IRepositoryTest")]
        public async Task<string> GetIRepositoryTest()
        {
            try
            {
                await _repository.InsertAsync(new BookAggregateRoot { Name = "HHY", PublishDate = DateTime.Now, Price = 100 });


            }
            catch (Exception e)
            {
                _logger.LogDebug(e.ToString());
                //throw;
            }
            
            return "hhy";
        }


    }
}
