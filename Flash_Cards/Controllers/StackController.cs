using Flash_Cards.Data;
using Flash_Cards.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flash_Cards.Controllers;
internal class StackController
{
    public void GetMenuChoice(FlashCardContext db)
    {
        var choice = UserInput.GetStackMenuInput();
        switch ( choice.ToLower() )
        {
            case "create a new stack":
                var ReturnedStack = UserInput.GetNewStackInput();
                db.AddStack( ReturnedStack );
                break;
                
        }
    }
}
