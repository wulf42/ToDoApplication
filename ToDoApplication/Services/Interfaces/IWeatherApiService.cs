using ToDoApplication.Models;

namespace ToDoApplication.Services.Interfaces
{
    public interface IWeatherApiService
    {
        WeatherApiResponse GetWeather(string location, string date, string time);
    }
}