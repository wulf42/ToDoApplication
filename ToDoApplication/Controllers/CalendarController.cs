using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApplication.Services.Interfaces;

namespace ToDoApplication.Controllers
{
    public class CalendarController : Controller
    {
        private readonly ICalendarService _calendarService;

        public CalendarController(ICalendarService calendarService)
        {
            _calendarService = calendarService;
        }

        [Authorize]
        public IActionResult Index()
        {
            var calendarEvents = _calendarService.GetCalendarEvents();
            ViewBag.Events = calendarEvents;
            return View();
        }
    }
}