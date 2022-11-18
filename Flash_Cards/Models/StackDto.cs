using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flash_Cards.Models;
internal class StackDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public int Count {get; set;} = 0;
}
