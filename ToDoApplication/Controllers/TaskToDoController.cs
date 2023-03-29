using Microsoft.AspNetCore.Mvc;

namespace ToDoApplication.Controllers
{
    public class TaskToDoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
