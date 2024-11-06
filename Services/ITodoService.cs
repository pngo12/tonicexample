using TodoApi.Models;

namespace TodoApi.Services;

public interface ITodoService
{
    Task<TodoResponseModel?> GetAllTodosAsync(bool? isCompleted);

    Task CreateNewTodo(CreateTodoRequestModel request);

    Task UpdateTodo(UpdateTodoRequestModel request);

    Task DeleteTodo(int todoId);
}
