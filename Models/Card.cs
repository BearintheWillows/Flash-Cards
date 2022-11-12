using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Card
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int StackPosition { get; set; }
        public int StackId { get; set; }
        public Stack Stack { get; set; }

    }
}