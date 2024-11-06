using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models;

public class TodoResponseModel
{
    public List<Todo> Todos { get; set; } = new();
}
