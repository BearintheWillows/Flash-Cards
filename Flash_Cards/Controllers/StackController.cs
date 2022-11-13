using Flash_Cards.Data;
using Flash_Cards.UI;
using Flash_Cards.View;
using Models;
using Spectre.Console;
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
            case "view all stacks":
                var stacks = db.GetAllStacks();
                UserView.DisplayStacks( stacks );
                break;
            case "delete a stack":
                var stack = UserInput.GetStackToDelete();
                if ( GetStackById( stack, db ) != null )
                {
                    UserView.DisplayStack( stack );
                }
                
               

                break;

        }

    }
    
    public static Stack GetStackById( int id, FlashCardContext db)
    {
        var stack;
        if ( stack = db.GetStackById( id ) )
        {
            return stack;
        }
        else
        {
            AnsiConsole.MarkupLine( "[red]Stack not found.[/]" );
            return null;
        }

    }
}
