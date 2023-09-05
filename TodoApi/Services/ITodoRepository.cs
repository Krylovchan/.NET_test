using TodoApi.Models;

namespace TodoApi.Services
{
    public interface ITodoRepository
    {
        void Get(long id);
        void GetAll();
        void Add(TodoItemDTO todoDTO);
        void Remove(long id);
        void Edit(long id, TodoItemDTO todoDTO);

    }
}
