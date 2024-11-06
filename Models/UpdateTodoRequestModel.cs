using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models;

public class UpdateTodoRequestModel : TodoBaseModel
{
    public string Name { get; set; }

    public bool IsCompleted { get; set; }
}
