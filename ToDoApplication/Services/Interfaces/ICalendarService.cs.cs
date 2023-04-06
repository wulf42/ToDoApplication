using static ToDoApplication.Services.CalendarService;

namespace ToDoApplication.Services.Interfaces
{
    public interface ICalendarService
    {
        List<CustomCalendarEvent> GetCalendarEvents();
    }
}

