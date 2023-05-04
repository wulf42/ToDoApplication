using Microsoft.AspNetCore.Mvc;
using ToDoApplication.Models;
using ToDoApplication.Services.Interfaces;

namespace ToDoApplication.Controllers
{
    public class ShoppingProductController : Controller
    {
        private readonly IShoppingProductService _shoppingProductService;
        public ShoppingProductController(IShoppingProductService shoppingProductService)
        {

            _shoppingProductService = shoppingProductService;

        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Save(int taskId)
        {
            var model = new ShoppingProduct();
            model.TaskToDoId = taskId;
            return PartialView("_addShoppingProduct", model);
        }

        [HttpPost]
        public IActionResult Save(ShoppingProduct product)
        {
            _shoppingProductService.Save(product);

            return RedirectToAction("Edit", "TaskToDo", new { id = product.TaskToDoId });
        }
        [HttpPost]
        public IActionResult Delete(int productId)
        {
            var taskId = _shoppingProductService.Delete(productId);
            return RedirectToAction("Edit", "TaskToDo", new { id = taskId });
        }

    }
}
