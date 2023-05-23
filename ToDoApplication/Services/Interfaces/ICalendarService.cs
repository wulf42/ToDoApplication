using ToDoApplication.Models;

namespace ToDoApplication.Services.Interfaces
{
    public interface ICalendarService
    {
        List<CustomCalendarEvent> GetCalendarEvents();
    }
}