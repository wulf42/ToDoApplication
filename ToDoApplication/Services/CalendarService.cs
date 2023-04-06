using ToDoApplication.Context;
using ToDoApplication.Services.Interfaces;

namespace ToDoApplication.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly ToDoApplicationDbContext _context;


        public CalendarService(ToDoApplicationDbContext context)
        {
            _context = context;
        }

        public List<CustomCalendarEvent> GetCalendarEvents()
        {
            var tasks = _context.TasksToDo.ToList();
            var events = new List<CustomCalendarEvent>();
            foreach (var task in tasks)
            {
                var calendarEvent = new CustomCalendarEvent
                {
                    Id = task.TaskId,
                    Title = task.Name,
                    Start = new DateTime(task.Date.Year, task.Date.Month, task.Date.Day, task.Time.Hour, task.Time.Minute, task.Time.Second),
                    End = new DateTime(task.Date.Year, task.Date.Month, task.Date.Day, task.Time.Hour, task.Time.Minute, task.Time.Second).AddHours(1),
                    AllDay = false,

                };
                events.Add(calendarEvent);
            }
            return events;
        }
        public class CustomCalendarEvent
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
            public bool AllDay { get; set; }
            public string Color { get; set; }
        }
    }

}

