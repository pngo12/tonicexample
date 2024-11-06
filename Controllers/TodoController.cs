using Microsoft.AspNetCore.Mvc;
using TodoApi.Exceptions;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private readonly ILogger<TodoController> _logger;
    private readonly ITodoService _todoService;

    public TodoController(ILogger<TodoController> logger, ITodoService todoService)
    {
        _logger = logger;
        _todoService = todoService;
    }

    [HttpGet()]
    public async Task<IActionResult> GetAllTodos()
    {
        try
        {
            return Ok(await _todoService.GetAllTodosAsync(null));
        }
        catch (Exception e)
        {
            _logger.LogWarning(e, "Error getting all todos");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("outstanding")]
    public async Task<IActionResult> GetOutstandingTodos()
    {
        try
        {
            return Ok(await _todoService.GetAllTodosAsync(false));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("completed")]
    public async Task<IActionResult> GetCompletedTodos()
    {
        try
        {
            return Ok(await _todoService.GetAllTodosAsync(true));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost()]
    public async Task<IActionResult> AddTodo([FromBody] CreateTodoRequestModel request)
    {
        try
        {
            if (string.IsNullOrEmpty(request.Name))
                return StatusCode(StatusCodes.Status400BadRequest, "Name is null or empty");

            await _todoService.CreateNewTodo(request);

            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut()]
    public async Task<IActionResult> UpdateTodo([FromBody] UpdateTodoRequestModel request)
    {
        try
        {
            if (request.TodoId == 0)
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Todo Id");

            await _todoService.UpdateTodo(request);
            return Ok();
        }
        catch (TodoNotFoundException e)
        {
            return StatusCode(StatusCodes.Status404NotFound, e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete("{todoId}")]
    public async Task<IActionResult> DeleteTodo([FromRoute] int todoId)
    {
        try
        {
            if (todoId == 0)
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Todo Id");

            await _todoService.DeleteTodo(todoId);
            return Ok();
        }
        catch (TodoNotFoundException e)
        {
            return StatusCode(StatusCodes.Status404NotFound, e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
