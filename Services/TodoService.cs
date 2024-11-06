using TodoApi.DataModels;
using TodoApi.Models;
using TodoApi.Repository;
using TodoApi.Services;

namespace TodoApi.TodoService;
public class TodoService : ITodoService
{
    private readonly ILogger<TodoService> _logger;
    private readonly ITodoRepository _repository;

    public TodoService(ILogger<TodoService> logger, ITodoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<TodoResponseModel?> GetAllTodosAsync(bool? isCompleted)
    {
        try
        {
            var todos = await _repository.GetAllTodos(isCompleted);

            if (todos == null)
                return null;

            var response = new TodoResponseModel();

            foreach (var todo in todos)
            {
                response.Todos.Add(new Todo
                {
                    TodoId = todo.TodoId,
                    Name = todo.Name,
                    IsCompleted = todo.IsCompleted
                });
            }

            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all todos");
            return null;
        }
    }

    public async Task CreateNewTodo(CreateTodoRequestModel request)
    {
        try
        {
            await _repository.AddNewTodo(
            new CreateTodoRequestModel
            {
                Name = request.Name
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating todo");
            throw;
        }

    }

    public async Task DeleteTodo(int todoId)
    {
        try
        {
            await _repository.DeleteTodo(todoId);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting todo");
            throw;
        }

    }

    public async Task UpdateTodo(UpdateTodoRequestModel request)
    {
        try
        {
            await _repository.UpdateTodo(new UpdateTodoDataModel
            {
                TodoId = request.TodoId,
                Name = request.Name,
                IsCompleted = request.IsCompleted
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error updating todo: {request.TodoId}");
            throw;
        }
    }
}
