using TodoApi.DataModels;
using TodoApi.Models;

namespace TodoApi.Repository;

public interface ITodoRepository
{
    Task<List<Todo>>? GetAllTodos(bool? isCompleted);

    Task AddNewTodo(CreateTodoRequestModel request);

    Task UpdateTodo(UpdateTodoDataModel request);

    Task DeleteTodo(int todoId);
}
