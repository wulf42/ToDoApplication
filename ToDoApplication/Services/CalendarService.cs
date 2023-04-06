using ToDoApplication.Context;
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
                var calendarEvent = new CustomCalendarEvent
                {
                    id = task.TaskId,
                    title = task.Name,
                    start = new DateTime(task.Date.Year, task.Date.Month, task.Date.Day, task.Time.Hour, task.Time.Minute, task.Time.Second),
                    end = new DateTime(task.Date.Year, task.Date.Month, task.Date.Day, task.Time.Hour, task.Time.Minute, task.Time.Second).AddHours(1),
                    allDay = false,

                };
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
            public bool allDay { get; set; }
            public string color { get; set; }
        }
    }

}

