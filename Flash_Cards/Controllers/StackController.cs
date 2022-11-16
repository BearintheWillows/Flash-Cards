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
                GetMenuChoice( db );
                break;
            case "view all stacks":
                var stacks = db.GetAllStacks();
                UserView.DisplayStacks( stacks );
                GetMenuChoice( db );
                break;
            case "delete a stack":
                var stack = UserInput.GetStackToDelete();
                Stack stackChoice = GetStackById( stack, db );
                UserView.DisplayStackToDelete( stackChoice );
                if (UserInput.ConfirmChoice())
                {
                    db.DeleteStack( stackChoice.Id );
                    var rule = new Rule($"[bold red] Record Deleted[/]");
                    AnsiConsole.Write( rule );
               
                    GetMenuChoice(db);
                }
                else
                {
                    var rule = new Rule("[bold red] Record NOT Deleted[/]");
                    AnsiConsole.Write( rule );
                    GetMenuChoice( db );
                }
               
                
               

                break;

        }

    }
    
    public static Stack GetStackById( int id, FlashCardContext db)
    {
        Stack stack = db.GetStackById( id );
        if ( stack != null )
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
