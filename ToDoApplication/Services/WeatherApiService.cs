using GoogleMaps.LocationServices;
using Newtonsoft.Json;
using System.Globalization;
using ToDoApplication.Models;
using ToDoApplication.Services.Interfaces;

namespace ToDoApplication.Services
{
    public class WeatherApiService : IWeatherApiService
    {
        private const string API_KEY = "AIzaSyCdwkDGJIILpflUYeX4sLihwDFLgBr0_Yk";

        public async Task<WeatherApiResponse> GetWeather(string location, string date, string time)
        {
            var locationService = new GoogleLocationService(apikey: API_KEY);
            if (string.IsNullOrEmpty(location))
            {
                location = "Warsaw";
            }
            var point = locationService.GetLatLongFromAddress(location);

            var latitude = point.Latitude.ToString("F2", CultureInfo.InvariantCulture);
            var longitude = point.Longitude.ToString("F2", CultureInfo.InvariantCulture);

            var url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&hourly=temperature_2m,rain,snowfall&start_date={date}&end_date={date}&timezone=Europe%2FBerlin";
            try
            {
                using var httpClient = new HttpClient();
                var response = await httpClient.GetStringAsync(url);

                var deserializedClass = JsonConvert.DeserializeObject<WeatherApiResponse>(response);

                var dateAndTime = date + "T" + time;

                if (deserializedClass == null || deserializedClass.hourly == null || deserializedClass.hourly.time == null)
                {
                    return new WeatherApiResponse();
                }
                // Find index of date and time in hourly array

                var index = Array.IndexOf(deserializedClass.hourly.time, dateAndTime);

                if (index >= 0)
                {
                    if (deserializedClass.hourly.temperature_2m != null)
                    {
                        var temperature = deserializedClass.hourly.temperature_2m[index];
                        deserializedClass.hourly.temperature_2m = new float[] { temperature };
                    }
                    if (deserializedClass.hourly.rain != null)
                    {
                        var rain = deserializedClass.hourly.rain[index];
                        deserializedClass.hourly.rain = new float[] { rain };
                    }
                    if (deserializedClass.hourly.snowfall != null)
                    {
                        var snow = deserializedClass.hourly.snowfall[index];
                        deserializedClass.hourly.snowfall = new float[] { snow };
                    }
                    if (deserializedClass.hourly.time != null)
                    {
                        var timeOutput = deserializedClass.hourly.time[index];
                        deserializedClass.hourly.time = new string[] { timeOutput };
                    }
                }

                return deserializedClass;
            }
            catch (Exception)
            {
                return new WeatherApiResponse();
            }
        }
    }
}