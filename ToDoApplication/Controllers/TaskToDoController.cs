using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApplication.Models;
using ToDoApplication.Services.Interfaces;

namespace ToDoApplication.Controllers
{
    public class TaskToDoController : Controller
    {
        private readonly ITaskToDoService _taskToDoService;

        public TaskToDoController(ITaskToDoService taskToDoService)
        {
            _taskToDoService = taskToDoService;
        }
        [Authorize]
        public IActionResult Index()
        {
            //Nie wyświetlaj listy jeżeli nie jesteś zalogowany, przekieruj na stronę wylogowania (po restarcie apki jesteś zalogowany ale nie ma Cię w sesji)
            if (HttpContext.Session.GetString("UserId") == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            var tasksToDoList = _taskToDoService.GetAll();
            return View(tasksToDoList);
        }

        public IActionResult Details()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            var task = _taskToDoService.Get(id);
            return View(task);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var task = _taskToDoService.Get(id);
            if (task == null)
                return NotFound();
            return View(task);
        }

        [HttpPost]

        public IActionResult Add(TaskToDo body)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");   //WiP - Add validation
            }
            body.addedBy = HttpContext.Session.GetString("UserId");

            if (body.addedBy == null)
            {
                return RedirectToAction("Index");
            }

            var id = _taskToDoService.Save(body);


            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _taskToDoService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(int id, TaskToDo body)
        {
            _taskToDoService.Edit(id, body);
            return RedirectToAction("Index");
        }
    }
}