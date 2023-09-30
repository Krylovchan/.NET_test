using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoItemsController : ControllerBase
{
    private readonly TodoContext _context;
    private readonly ITodoRepository _todoRepository;

    public TodoItemsController(TodoContext context, ITodoRepository todoRepository)
    {
        //_context = context;
        _todoRepository = todoRepository;
    }

    // GET: api/TodoItems
    [HttpGet]
    public Task<List<TodoItem>> GetTodoItems()
    {
        return  Task.FromResult(_todoRepository.GetAll());
    }

    // GET: api/TodoItems/5
    // <snippet_GetByID>
    [HttpGet("{id}")]
    public async Task<TodoItem> GetTodoItem(long id)
    {
        var todoItem = await _todoRepository.Get(id);
        
        return todoItem;
    }
    // </snippet_GetByID>

    // PUT: api/TodoItems/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    // <snippet_Update>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTodoItem(long id, TodoItemDTO todoDTO)
    {
        TodoItem todoItem = await _todoRepository.Get(id);
        
        if (id != todoDTO.Id)
        {
            return BadRequest();
        }
        
        if (todoItem == null)
        {
            return NotFound();
        }

        todoItem.Name = todoDTO.Name;
        todoItem.IsComplete = todoDTO.IsComplete;

        await _todoRepository.Edit(id, todoItem);
        return NoContent();
    }
    // </snippet_Update>

    // POST: api/TodoItems
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    // <snippet_Create>
    [HttpPost]
    public async Task<ActionResult<TodoItemDTO>> PostTodoItem(TodoItem todoDTO)
    {
        await _todoRepository.Add(todoDTO);
       
        return CreatedAtAction(nameof(GetTodoItem), new { id = todoDTO.Id }, todoDTO);

    }
    // </snippet_Create>

    // DELETE: api/TodoItems/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(long id)
    {
        var todoItem = await _todoRepository.Get(id);
        
        if (todoItem == null)
        {
            return NotFound();
        }

        await _todoRepository.Remove(id);
        await _context.SaveChangesAsync();

        return NoContent();
    }

   /* private bool TodoItemExists(long id)
    {
        return _context.TodoItems.Any(e => e.Id == id);
    }*/

    /*private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
       new TodoItemDTO
       {
           Id = todoItem.Id,
           Name = todoItem.Name,
           IsComplete = todoItem.IsComplete
       };*/
}