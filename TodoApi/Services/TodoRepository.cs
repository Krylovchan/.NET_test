using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace TodoApi.Services;

public class TodoRepository : ITodoRepository
{
    private readonly TodoContext _context;

    public TodoRepository(TodoContext context)
    {
        _context = context;
    }
    
    public  List<TodoItem> GetAll()
    {
        return  _context.TodoItems.ToList();
    }
    
    public async Task<TodoItem> Get(long id)
    {
        return await  _context.TodoItems.FindAsync(id);
    }
    
    public async Task<EntityEntry<TodoItem>> Add(TodoItem todoDTO)
    {
        _context.TodoItems.Add(todoDTO);
        await _context.SaveChangesAsync();

        return  _context.TodoItems.Add(todoDTO);
    }

    public async Task<int> Edit(long id, TodoItem todoDTO)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);
        
        todoItem.Name = todoDTO.Name;
        todoItem.IsComplete = todoDTO.IsComplete;
        

        return  await _context.SaveChangesAsync();
        
    }

    public async Task<int> Remove(long id)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);
        _context.TodoItems.Remove(todoItem);
        return await _context.SaveChangesAsync();
    }
}