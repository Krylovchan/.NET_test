using TodoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
