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
                var color = GetCategoryColor(task.Category);

                var calendarEvent = new CustomCalendarEvent
                {
                    id = task.TaskId,
                    title = task.Name,
                    start = new DateTime(task.Date.Year, task.Date.Month, task.Date.Day) + task.Time.ToTimeSpan(),
                    end = (new DateTime(task.Date.Year, task.Date.Month, task.Date.Day) + task.Time.ToTimeSpan()).AddHours(1),
                    backgroundColor = color,
                    borderColor = color,
                    startTime = task.Time,
                    endTime = task.Time.AddHours(1),
                    dow = task.Status == Status.Daily ? new int[] { 0, 1, 2, 3, 4, 5, 6 } : null
                };

                if (task.Status == Status.Daily)
                {
                    calendarEvent.end = new DateTime(9999, task.Date.Month, task.Date.Day) + task.Time.ToTimeSpan();
                }

                events.Add(calendarEvent);
            }

            return events;
        }

        private string GetCategoryColor(Category category)
        {
            return category switch
            {
                Category.DeepWork => "#dc3545",     // czerwony
                Category.ShallowWork => "#007bff",  // niebieski
                Category.Chores => "#ffc107",       // żółty
                Category.Learning => "#28a745",     // zielony
                Category.MindCare => "#6f42c1",     // fioletowy
                Category.BodyCare => "#17a2b8",     // turkusowy
                Category.People => "#fd7e14",       // pomarańczowy
                Category.ShoppingList => "#ffc0cb", // różowy
                _ => "#6c757d"                      // szary
            };
        }

        public class CustomCalendarEvent
        {
            public int id { get; set; }
            public string title { get; set; }
            public DateTime start { get; set; }
            public DateTime end { get; set; }
            public bool allDay { get; set; } = false;
            public string backgroundColor { get; set; }
            public string borderColor { get; set; }
            public string display { get; set; } = "block";
            public TimeOnly? startTime { get; set; }
            public TimeOnly? endTime { get; set; }
            public int[]? dow { get; set; }
        }
    }
}
