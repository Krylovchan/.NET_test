using TodoApi.Models;

namespace TodoApi.Services
{
    public interface ItestRep
    {
        string Get(long id);
        void GetAll();
        string Add(TodoItemDTO todoDTO);
        string Remove(long id);
        string Edit(long id, TodoItemDTO todoDTO);
    }
}
