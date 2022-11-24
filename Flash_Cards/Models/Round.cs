using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flash_Cards.Models;
internal class Round
{
    public int Id { get; set; }
    public int Correct { get; set; }
    public int CardId { get; set; }

    public int SessionId { get; set; }

    public int RoundNumber { get; set; }
}
