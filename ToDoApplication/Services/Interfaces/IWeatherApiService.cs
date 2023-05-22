using ToDoApplication.Models;

namespace ToDoApplication.Services.Interfaces
{
    public interface IWeatherApiService
    {
        WeatherApiResponse Get(string location, string date, string time);
    }
}