namespace TodoApi.DataModels;

public class UpdateTodoDataModel
{
    public int TodoId { get; set; }

    public string Name { get; set; }

    public bool IsCompleted { get; set; }
}
