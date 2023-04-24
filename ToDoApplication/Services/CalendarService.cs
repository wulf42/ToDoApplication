using ToDoApplication.Context;
using ToDoApplication.Models;
using ToDoApplication.Services.Interfaces;

namespace ToDoApplication.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly ToDoApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CalendarService(ToDoApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<CustomCalendarEvent> GetCalendarEvents()
        {
            var userId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            var tasks = _context.TasksToDo.Where(t => t.addedBy == userId).ToList();

            var events = new List<CustomCalendarEvent>();
            foreach (var task in tasks)
            {
                string color;
                switch (task.Category)
                {
                    case Category.DeepWork:
                        color = "#dc3545"; // czerwony
                        break;
                    case Category.ShallowWork:
                        color = "#007bff"; // niebieski
                        break;
                    case Category.Chores:
                        color = "#ffc107"; // żółty
                        break;
                    case Category.Learning:
                        color = "#28a745"; // zielony
                        break;
                    case Category.MindCare:
                        color = "#6f42c1"; // fioletowy
                        break;
                    case Category.BodyCare:
                        color = "#17a2b8"; // turkusowy
                        break;
                    case Category.People:
                        color = "#fd7e14"; // pomarańczowy
                        break;
                    case Category.ShoppingList:
                        color = "#ffc0cb"; // różowy
                        break;
                    default:
                        color = "#6c757d"; // szary
                        break;
                }

                var calendarEvent = new CustomCalendarEvent
                {
                    id = task.TaskId,
                    title = task.Name,
                    start = new DateTime(task.Date.Year, task.Date.Month, task.Date.Day, task.Time.Hour, task.Time.Minute, task.Time.Second),
                    end = new DateTime(task.Date.Year, task.Date.Month, task.Date.Day, task.Time.Hour, task.Time.Minute, task.Time.Second).AddHours(1),
                    backgroundColor = color,
                    borderColor = color,
                };
                if (task.Status == Status.Daily)
                {
                    calendarEvent.end = new DateTime(9999, task.Date.Month, task.Date.Day, task.Time.Hour, task.Time.Minute, task.Time.Second);
                    calendarEvent.startTime = new TimeOnly(task.Time.Hour, task.Time.Minute, task.Time.Second);
                    calendarEvent.endTime = new TimeOnly(task.Time.Hour, task.Time.Minute, task.Time.Second).AddHours(1);
                    calendarEvent.dow = new int[] { 0, 1, 2, 3, 4, 5, 6 };

                }

                events.Add(calendarEvent);
            }
            return events;
        }
        public class CustomCalendarEvent
        {
            public int id { get; set; }
            public string title { get; set; }
            public DateTime start { get; set; }
            public DateTime end { get; set; }

            public bool allDay = false;
            public string backgroundColor { get; set; }
            public string borderColor { get; set; }

            public string display = "block";

            public TimeOnly? startTime;
            public TimeOnly? endTime;
            public int[]? dow;


        }
    }

}

