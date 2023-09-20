using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using Microsoft.AspNetCore.Mvc;


namespace TodoApi.Services;

    [Route("api/[controller]")]
    [ApiController]
    public class TodoRepository : ControllerBase
    {
        private readonly TodoContext _context;
        private readonly ITodoRepository _repository;

        public TodoRepository(TodoContext context)
        {
            _context = context;
        }
        public TodoRepository(ITodoRepository repository)
        {
            _repository = repository;
        }

        public string Get(long id)
        {
           // var todoItem = _context.TodoItems.FindAsync(id);
            var todoItem = _repository.TodoItems.FindAsync(id);

        if (todoItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(todoItem);
        }


        [HttpGet]
        public void GetAll()
        {
            return _context.TodoItems
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }
        public void Add(TodoItemDTO todoDTO)
        {
            var todoItem = new TodoItem
            {
                IsComplete = todoDTO.IsComplete,
                Name = todoDTO.Name
            };

            _context.TodoItems.Add(todoItem);
            _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(Get),
                new { id = todoItem.Id },
                ItemToDTO(todoItem));
        }
        public void Remove(long id)
        {
            var todoItem = _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            _context.SaveChangesAsync();

            return NoContent();
        }

        public void Edit(long id, TodoItemDTO todoDTO)
        {
            if (id != todoDTO.Id)
            {
                return BadRequest();
            }

            var todoItem = _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.Name = todoDTO.Name;
            todoItem.IsComplete = todoDTO.IsComplete;

            try
            {
                _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        private bool TodoItemExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }

        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
           new TodoItemDTO
           {
               Id = todoItem.Id,
               Name = todoItem.Name,
               IsComplete = todoItem.IsComplete
           };
    }

