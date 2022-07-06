
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyBBSWebApi.Dal.Core;
using MyBBSWebApi.Dal.Factorys;
using MyBBSWebApi.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBBSWebApi.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Post> Get()
        {
            // var rng = new Random();
            // return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            // {
            //     Date = DateTime.Now.AddDays(index),
            //     TemperatureC = rng.Next(-20, 55),
            //     Summary = Summaries[rng.Next(Summaries.Length)]
            // })
            // .ToArray();

            //不再使用using
            var context = DbContextFactory.GetDbContext();
            var res = context.Posts.ToList();

            // using var context = new MyBBSDbContext(); 
            // var res = context.Posts.ToList();
            return res;
        }
    }
}
