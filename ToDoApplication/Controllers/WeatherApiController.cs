using Microsoft.AspNetCore.Mvc;
using ToDoApplication.Services.Interfaces;

namespace ToDoApplication.Controllers
{
    public class WeatherApiController : Controller
    {
        private readonly IWeatherApiService _weatherApiService;

        public WeatherApiController(IWeatherApiService weatherApiService)
        {
            _weatherApiService = weatherApiService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Index(string location, string date, string time)
        {
            var response = _weatherApiService.Get(location, date, time);
            return View(response);
        }
    }
}