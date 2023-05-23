using ToDoApplication.Models;

namespace ToDoApplication.Services.Interfaces
{
    public interface IWeatherApiService
    {
        Task<WeatherApiResponse> GetWeather(string location, string date, string time);
    }
}