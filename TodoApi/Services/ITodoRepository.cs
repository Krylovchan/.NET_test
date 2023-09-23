using TodoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TodoApi.Services
{
    public interface ITodoRepository
    {
        Task<TodoItem> Get(long id);
        List<TodoItem> GetAll();
        Task<EntityEntry<TodoItem>> Add(TodoItem todoDTO);
        Task<int> Remove(long id);
        Task<int> Edit(long id, TodoItemDTO todoDTO);


    }
}
