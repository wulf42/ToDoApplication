using GoogleMaps.LocationServices;
using Newtonsoft.Json;
using System.Net;
using ToDoApplication.Models;
using ToDoApplication.Services.Interfaces;

namespace ToDoApplication.Services
{
    public class WeatherApiService : IWeatherApiService
    {
        private const string API_KEY = "YOUR_API_KEY"; // Zmień to na swój klucz API

        public WeatherApiResponse GetWeather(string location, string date, string time)
        {
            var locationService = new GoogleLocationService(apikey: API_KEY);
            if (string.IsNullOrEmpty(location))
            {
                location = "Warsaw";
            }
            var point = locationService.GetLatLongFromAddress(location);

            var latitude = Math.Round(point.Latitude, 2).ToString("F2");
            var longitude = Math.Round(point.Longitude, 2).ToString("F2");

            var url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&hourly=temperature_2m,rain,snowfall&start_date={date}&end_date={date}&timezone=Europe%2FBerlin";

            try
            {
                using (var webClient = new WebClient())
                {
                    var response = webClient.DownloadString(url);

                    var deserializedClass = JsonConvert.DeserializeObject<WeatherApiResponse>(response);

                    var dateAndTime = date + "T" + time;

                    // Find index of date and time in hourly array
                    var index = Array.IndexOf(deserializedClass.hourly.time, dateAndTime);

                    if (index >= 0)
                    {
                        var temperature = deserializedClass.hourly.temperature_2m[index];
                        var rain = deserializedClass.hourly.rain[index];
                        var snow = deserializedClass.hourly.snowfall[index];
                        var timeOutput = deserializedClass.hourly.time[index];

                        // Aktualizuj tablice "hourly" tylko z jednym elementem
                        deserializedClass.hourly.time = new string[] { timeOutput };
                        deserializedClass.hourly.temperature_2m = new float[] { temperature };
                        deserializedClass.hourly.rain = new float[] { rain };
                        deserializedClass.hourly.snowfall = new float[] { snow };
                    }

                    return deserializedClass;
                }
            }
            catch (Exception e)
            {
                // Tutaj można obsłużyć błąd, zalogować go lub zwrócić odpowiedź z błędem.
                // Na przykład, można utworzyć nowy obiekt WeatherApiResponse z informacją o błędzie.
                return new WeatherApiResponse();
            }
        }
    }
}
