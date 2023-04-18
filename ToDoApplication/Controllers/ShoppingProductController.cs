using Microsoft.AspNetCore.Mvc;

namespace ToDoApplication.Controllers
{
    public class ShoppingProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
