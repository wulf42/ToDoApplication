﻿using ToDoApplication.Context;
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


        public TaskToDoService(ToDoApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IShoppingProductService shoppingProductService)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _shoppingProductService = shoppingProductService;
        }

        public TaskDetailsViewModel Get(int id)
        {
            var taskToDo = _context.TasksToDo.Find(id);
            var shoppingProducts = _context.ShoppingProducts
            .Where(x => x.TaskToDoId == id)
            .ToList();


            var viewModel = new TaskDetailsViewModel
            {
                TaskToDo = taskToDo,
                ShoppingProducts = shoppingProducts
            };


            return viewModel;
        }

        public List<TaskToDo> GetAll()
        {
            var userId = _httpContextAccessor.HttpContext.Session.GetString("UserId");

            var tasksToDoList = _context.TasksToDo
                .Where(t => t.addedBy == userId)
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

            //remove all shopping products related to task
            var shoppingProducts = _context.ShoppingProducts
                .Where(sp => sp.TaskToDoId == taskToDo.TaskId);
            _context.ShoppingProducts.RemoveRange(shoppingProducts);

            //remove task
            _context.TasksToDo.Remove(taskToDo);
            _context.SaveChanges();
            return taskToDo.TaskId;
        }

        public int Edit(int id, TaskDetailsViewModel body)
        {
            var taskToUpdate = _context.TasksToDo.Find(id);
            //Edit task form data
            if (body.TaskToDo != null && body.TaskToDo.TaskId != 0)
            {
                taskToUpdate.Name = body.TaskToDo.Name;
                taskToUpdate.Description = body.TaskToDo.Description;
                taskToUpdate.Category = body.TaskToDo.Category;
                taskToUpdate.Date = body.TaskToDo.Date;
                taskToUpdate.Time = body.TaskToDo.Time;
            }
            //Move to next category (passed only int id, TaskToDo body is empty)
            if (body.TaskToDo == null)
            {
                taskToUpdate.Status += 1;
            }
            if (body.TaskToDo != null && body.ShoppingProducts != null && body.ShoppingProducts.Count > 0)
            {
                foreach (var shoppingProduct in body.ShoppingProducts)
                {
                    _shoppingProductService.Edit(shoppingProduct.productId, shoppingProduct);
                }
            }
            if (body.TaskToDo != null && body.TaskToDo.Status == Status.Daily)
            {
                taskToUpdate.LastDone = body.TaskToDo.LastDone;
            }
            _context.TasksToDo.Update(taskToUpdate);
            _context.SaveChanges();
            return taskToUpdate.TaskId;
        }
    }
}