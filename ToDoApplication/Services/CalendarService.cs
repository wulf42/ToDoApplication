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
            if (_httpContextAccessor.HttpContext == null)
            {
                return new List<CustomCalendarEvent>();
            }
            var userId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            var tasks = _context.TasksToDo.Where(t => t.addedBy == userId).ToList();

            var events = new List<CustomCalendarEvent>();
            foreach (var task in tasks)
            {
                if (task.Name == null)
                {
                    continue;
                }
                var color = GetCategoryColor(task.Category);

                var calendarEvent = new CustomCalendarEvent
                {
                    Id = task.TaskId,
                    Title = task.Name,
                    Start = new DateTime(task.Date.Year, task.Date.Month, task.Date.Day) + task.Time.ToTimeSpan(),
                    End = (new DateTime(task.Date.Year, task.Date.Month, task.Date.Day) + task.Time.ToTimeSpan()).AddHours(1),
                    BackgroundColor = color,
                    BorderColor = color,
                };
                if (task.Status == Status.Daily)
                {
                    calendarEvent.StartTime = task.Time;
                    calendarEvent.EndTime = task.Time.AddHours(1);
                    calendarEvent.Dow = new int[] { 0, 1, 2, 3, 4, 5, 6 };
                    calendarEvent.End = new DateTime(9999, task.Date.Month, task.Date.Day) + task.Time.ToTimeSpan();
                }

                events.Add(calendarEvent);
            }

            return events;
        }

        private static string GetCategoryColor(Category category)
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


    }
}