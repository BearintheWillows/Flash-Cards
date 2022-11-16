using Flash_Cards.Data;
using Flash_Cards.UI;
using Flash_Cards.View;
using Models;
using Spectre.Console;

namespace Flash_Cards.Controllers;
internal class MenuController
{


    public void ManageStackMenu( FlashCardContext db )
    {
        var choice = MenuInputs.GetStackMenuInput();
        switch ( choice.ToLower() )
        {
            case "create a new stack":
                var ReturnedStack = MenuInputs.GetNewStackInput();
                db.AddStack( ReturnedStack );
                ManageStackMenu( db );
                break;
            case "view all stacks":
                var stacks = db.GetAllStacks();
                DataViews.ViewAllStacks( stacks );
                ManageStackMenu( db );
                break;
            case "delete a stack":
                var stack = MenuInputs.GetStackToDelete();
                Stack stackChoice = StackController.GetStackById( stack, db );
                DataViews.ViewStackToDelete( stackChoice );
                if ( MenuInputs.ConfirmChoice() )
                {
                    db.DeleteStack( stackChoice.Id );
                    var rule = new Rule($"[bold red] Record Deleted[/]");
                    AnsiConsole.Write( rule );

                    ManageStackMenu( db );
                }
                else
                {
                    var rule = new Rule("[bold red] Record NOT Deleted[/]");
                    AnsiConsole.Write( rule );
                    ManageStackMenu( db );
                }
                break;
            case "Back to Main Menu":
                break;
                  
        }

    }
}
