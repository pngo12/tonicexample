using Microsoft.EntityFrameworkCore;
using TodoApi.DatabaseContext;
using TodoApi.DataModels;
using TodoApi.Exceptions;
using TodoApi.Models;

namespace TodoApi.Repository;

public class TodoRepository : ITodoRepository
{
    private readonly TodoContext _todoContext;

    public TodoRepository(TodoContext todoContext)
    {
        _todoContext = todoContext;
    }

    public async Task<List<Todo>>? GetAllTodos(bool? isCompleted)
    {
        return isCompleted == null
        ? await _todoContext.Todos.AsNoTracking().ToListAsync()
        : await _todoContext.Todos.Where(x => x.IsCompleted == isCompleted).AsNoTracking().ToListAsync()
        ;
    }

    public async Task AddNewTodo(CreateTodoRequestModel request)
    {
        await _todoContext.AddAsync(
        new Todo
        {
            Name = request.Name,
            IsCompleted = false,
        });

        await _todoContext.SaveChangesAsync();
    }

    public async Task DeleteTodo(int todoId)
    {
        var todo = await _todoContext.Todos.Where(x => x.TodoId == todoId).FirstOrDefaultAsync();

        if (todo == null)
            throw new TodoNotFoundException("Todo not found");

        _todoContext.Todos.Remove(todo);

        await _todoContext.SaveChangesAsync();
    }

    public async Task UpdateTodo(UpdateTodoDataModel request)
    {
        var todo = await _todoContext.Todos.Where(x => x.TodoId == request.TodoId).FirstOrDefaultAsync();

        if (todo == null)
            throw new TodoNotFoundException("Todo not found");


        todo.Name = request.Name;
        todo.IsCompleted = request.IsCompleted;

        await _todoContext.SaveChangesAsync();
    }
}
