using Flash_Cards.Data;
using Flash_Cards.UI;
using Flash_Cards.View;
using Models;
using Spectre.Console;

namespace Flash_Cards.Controllers;
internal class MenuController
{   
    private readonly FlashCardContext db;
    private readonly StackController stackController;
   

    public MenuController( FlashCardContext db, StackController stackController )
    {
        this.stackController = stackController;
        this.db = db;
     
    }

    
    public void ManageStackMenu()
    {
        var choice = MenuInputs.GetStackMenuInput();
        switch ( choice.ToLower() )
        {
            case "create a new stack":
                var ReturnedStack = MenuInputs.GetNewStackInput();
                db.AddStack( ReturnedStack );
                ManageStackMenu();
                break;
            case "view all stacks":
                var stacks = db.GetAllStacks();
                DataViews.ViewAllStacks( stacks );
                ManageStackMenu();
                break;
            case "delete a stack":
                var stack = MenuInputs.GetStackToDelete();
                Stack stackChoice = stackController.GetStackById( stack);
                DataViews.ViewStackToDelete( stackChoice );
                if ( MenuInputs.ConfirmChoice() )
                {
                    db.DeleteStack( stackChoice.Id );
                    var rule = new Rule($"[bold red] Record Deleted[/]");
                    AnsiConsole.Write( rule );

                    ManageStackMenu();
                }
                else
                {
                    var rule = new Rule("[bold red] Record NOT Deleted[/]");
                    AnsiConsole.Write( rule );
                    ManageStackMenu();
                }
                break;
            case "Back to Main Menu":
                MainMenu();
                break;
                  
        }

    }

    public bool MainMenu()
    {
        Console.Clear();
        bool exitProgram = false;
        MenuViews.MainMenu();
        string choice = MenuInputs.MainMenuInput();

        

        switch ( choice )
        {
            case "Play a Stack!":
                //TODO

                
            case "Manage Stacks!":
                Console.Clear();
                ManageStackMenu();
                break;
            case "View Sessions":
                //TODO
              
            case "Exit":
                exitProgram = true;
                break;
        }
        return exitProgram;
    }
}
