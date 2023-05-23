using AutoMapper;
using ToDoApplication.Context;
using ToDoApplication.Models;
using ToDoApplication.Services.Interfaces;
using ToDoApplication.ViewModels;

namespace ToDoApplication.Services
{
    public class TaskToDoService : ITaskToDoService
    {
        private readonly ToDoApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IShoppingProductService _shoppingProductService;
        private readonly IMapper _mapper;

        public TaskToDoService(
            ToDoApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IShoppingProductService shoppingProductService,
            IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _shoppingProductService = shoppingProductService;
            _mapper = mapper;
        }

        public TaskDetailsViewModel Get(int id)
        {
            var taskToDo = _context.TasksToDo.Find(id);
            var shoppingProducts = _context.ShoppingProducts
                .Where(x => x.TaskToDoId == id)
                .ToList();

            var shoppingProductViewModels = _mapper.Map<List<ShoppingProductViewModel>>(shoppingProducts);

            var taskToDoViewModel = _mapper.Map<TaskToDoViewModel>(taskToDo);
            taskToDoViewModel.ShoppingProducts = shoppingProductViewModels;

            var viewModel = new TaskDetailsViewModel
            {
                TaskToDo = taskToDoViewModel,
                ShoppingProducts = shoppingProductViewModels
            };

            return viewModel;
        }

        public List<TaskToDo> GetAll()
        {
            if (_httpContextAccessor.HttpContext == null)
            {
                return new List<TaskToDo>();
            }

            var userId = _httpContextAccessor.HttpContext.Session.GetString("UserId");

            var tasksToDoList = _context.TasksToDo
                                    .Where(t => t.AddedBy == userId)
                                    .OrderBy(t => t.Date)
                                    .ThenBy(t => t.Time)
                                    .ToList();

            return tasksToDoList;
        }

        public int Save(TaskToDo taskToDo)
        {
            _context.TasksToDo.Add(taskToDo);
            _context.SaveChanges();

            return taskToDo.TaskId;
        }

        public int Delete(int id)
        {
            var taskToDo = _context.TasksToDo.Find(id);

            if (taskToDo == null)
            {
                return -1;
            }

            RemoveShoppingProducts(taskToDo);

            _context.TasksToDo.Remove(taskToDo);
            _context.SaveChanges();

            return taskToDo.TaskId;
        }

        public int Edit(int id, TaskDetailsViewModel body)
        {
            var taskToUpdate = _context.TasksToDo.Find(id);

            if (taskToUpdate == null)
            {
                return -1;
            }

            EditTaskFormData(taskToUpdate, body.TaskToDo);
            MoveToNextCategory(taskToUpdate, body.TaskToDo);
            EditShoppingProducts(taskToUpdate, body.ShoppingProducts);
            UpdateTask(taskToUpdate);

            return taskToUpdate.TaskId;
        }

        private static void EditTaskFormData(TaskToDo task, TaskToDoViewModel taskViewModel)
        {
            if (taskViewModel != null && taskViewModel.TaskId != 0)
            {
                task.Name = taskViewModel.Name;
                task.Description = taskViewModel.Description;
                task.Category = taskViewModel.Category;
                task.Date = taskViewModel.Date;
                task.Time = taskViewModel.Time;
            }

            if (taskViewModel?.Status == Status.Daily)
            {
                task.LastDone = taskViewModel.LastDone;
            }
        }

        private static void MoveToNextCategory(TaskToDo task, TaskToDoViewModel taskViewModel)
        {
            if (taskViewModel == null)
            {
                task.Status++;
            }
        }

        private void EditShoppingProducts(TaskToDo task, List<ShoppingProductViewModel> shoppingProducts)
        {
            if (shoppingProducts?.Count > 0)
            {
                foreach (var shoppingProductViewModel in shoppingProducts)
                {
                    var shoppingProduct = _mapper.Map<ShoppingProduct>(shoppingProductViewModel);
                    shoppingProduct.TaskToDoId = task.TaskId;
                    shoppingProduct.TaskToDo = task;

                    _shoppingProductService.Edit(shoppingProduct.ProductId, shoppingProduct);
                }
            }
        }

        private void RemoveShoppingProducts(TaskToDo task)
        {
            var shoppingProducts = _context.ShoppingProducts
                .Where(sp => sp.TaskToDoId == task.TaskId)
                .ToList();

            _context.ShoppingProducts.RemoveRange(shoppingProducts);
        }

        private void UpdateTask(TaskToDo task)
        {
            _context.TasksToDo.Update(task);
            _context.SaveChanges();
        }
    }
}