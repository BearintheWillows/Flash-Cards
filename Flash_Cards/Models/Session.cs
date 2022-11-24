using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flash_Cards.Models;
internal class Session
{
    public int Id { get; set; }
    public int StackId { get; set; }
    public int Total { get; set; }
    public DateTime SessionDate { get; set; }
}
