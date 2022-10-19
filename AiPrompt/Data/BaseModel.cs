using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiPrompt.Data;

public class BaseModel
{
    [PrimaryKey]
    public string Id { get; set; }
}
