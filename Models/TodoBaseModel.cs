using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models;

public abstract class TodoBaseModel
{
    public int TodoId { get; set; }
}
