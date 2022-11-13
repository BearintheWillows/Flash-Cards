using Flash_Cards.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flash_Cards.Controllers;
internal class StackController
{
    public void GetMenuChoice()
    {
        var choice = UserInput.GetStackMenuInput();
        switch ( choice.ToLower() )
        {
            case "create a new stack":
                Console.WriteLine(choice);
                break;
                
        }
    }
}
