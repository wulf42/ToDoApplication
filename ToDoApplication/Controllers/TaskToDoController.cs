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
        public IActionResult Index()
        {
            var tasksToDoList = _taskToDoService.GetAll();
            return View(tasksToDoList);

        }
        public IActionResult Add()
        {
            return View();
        }
        public IActionResult Details()
        {
            return View();
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
                return View(body);
            }

            var id = _taskToDoService.Save(body);

            TempData["TaskId"] = id;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _taskToDoService.Delete(id);
            return RedirectToAction("Index");
        }


    }
}
