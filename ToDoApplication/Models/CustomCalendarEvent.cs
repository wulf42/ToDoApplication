using Newtonsoft.Json;

namespace ToDoApplication.Models
{
    public class CustomCalendarEvent
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("start")]
        public DateTime Start { get; set; }

        [JsonProperty("end")]
        public DateTime End { get; set; }

        [JsonProperty("allDay")]
        public bool AllDay { get; set; } = false;

        [JsonProperty("backgroundColor")]
        public string BackgroundColor { get; set; }

        [JsonProperty("borderColor")]
        public string BorderColor { get; set; }

        [JsonProperty("display")]
        public string Display { get; set; } = "block";

        [JsonProperty("startTime")]
        public TimeOnly? StartTime { get; set; }

        [JsonProperty("endTime")]
        public TimeOnly? EndTime { get; set; }

        [JsonProperty("dow")]
        public int[]? Dow { get; set; }
    }
}