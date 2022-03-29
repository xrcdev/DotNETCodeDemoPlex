using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication_AutoMapper.Model;
using AutoMapper;

namespace WebApplication_AutoMapper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly IMapper mapper;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMapper _mapper
            )
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var author = new AuthorModel();
            author.Id = 1;
            author.FirstName = "Joydip";
            author.LastName = null;
            author.Address1 = "Hyderabad";
            author.CreateTime = DateTime.Now;
            //AutoMapper.Mapper.CreateMap<Order, OrderDto>();
            //Mapper.CreateMap<OrderItem, OrderItemDto>();
            //var model = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderDto>>(orders);
            var authorModel = mapper.Map<AuthorModel>(author);

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
