namespace TodoApi.DataModels;

public class CreateTodoDataModel
{
    public string Name { get; set; }
    public bool IsCompleted { get; set; } = false;
}
