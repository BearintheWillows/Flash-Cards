using Flash_Cards.Data;
using Flash_Cards.Models;
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
                List<StackDto> stacks = db.GetAllStacks();
                DataViews.ViewAll( stacks );
                ManageStackMenu();
                break;
            case "inspect a stack":
                int id = DataInput.IdInput();
                StackDto? stack = stackController.GetStackById(id);

                if ( stack != null )
                {
                    InspectStackMenu( stack );
                }
                else
                {
                    Console.Clear();
                    Rule rule = new Rule( "[bold red]Stack not found.[/]" );
                    AnsiConsole.Write( rule );
                    ManageStackMenu();
                }
              break;
            case "Back to Main Menu":
                MainMenu();
                break;
                  
        }

    }

    private void InspectStackMenu(StackDto stack)
    {
        DataViews.ViewById( stack );
        int cardId = 0;
        string choice = MenuInputs.GetInspectStackMenuInput();
        Console.Clear();
        switch ( choice.ToLower() )
        {
            case "add a new card":
                var card = MenuInputs.GetNewCardInput();
                stackController.AddCardToStack( stack.Id, card );
                InspectStackMenu( stack );
                break;
            case "view all cards":
                List<CardDto> cards = db.GetAllCardsByStackId( stack.Id );
                if ( cards != null )
                {
                    DataViews.ViewAll( cards );
                    InspectStackMenu( stack );
                }
                else
                {
                    AnsiConsole.MarkupLine( "[red]No Cardsfound.[/]" );
                }
                break;
            case "inspect a card":
                 cardId = MenuInputs.GetCardIdInput();
                CardDto? cardChoice = db.GetCardById( cardId );
                if ( cardChoice != null )
                {
                    DataViews.ViewById( cardChoice );
                }
                else
                {
                    AnsiConsole.MarkupLine( "[red]Card not found.[/]" );
                    AnsiConsole.MarkupLine( "[red]Press any key to return to menu[/]" );
                    Console.ReadKey();
                    InspectStackMenu( stack );

                }
               
                break;
            case "delete stack":
                if ( MenuInputs.ConfirmChoice() )
                {
                    db.DeleteStackAndCards( stack.Id );
                    var rule = new Rule($"[bold red] Record Deleted[/]");
                    AnsiConsole.Write( rule );

                    ManageStackMenu();
                }
                else
                {
                    var rule = new Rule("[bold red] Record NOT Deleted[/]");
                    AnsiConsole.Write( rule );
                    InspectStackMenu( stack );
                }
                break;

        }
    }

    /*public void InspectACardMenu( int id )
    {
        string choice = MenuInputs.GetInspectACardMenuInput();
        Console.Clear();
        switch ( choice.ToLower() )
        {
            case "edit card":
                var card = MenuInputs.GetNewCardInput();
                db.UpdateCard( id, card );
                break;
            case "delete card":
                if ( MenuInputs.ConfirmChoice() )
                {
                    db.DeleteCard( id );
                    var rule = new Rule( "[bold red] Record Deleted[/]" );
                    AnsiConsole.Write( rule );
                    ManageStackMenu();
                }
                else
                {
                    var rule = new Rule( "[bold red] Record NOT Deleted[/]" );
                    AnsiConsole.Write( rule );
                    InspectStackMenu( id );
                }
                break;
            case "back to stack menu":
                InspectStackMenu( id );
                break;
        }
    }*/

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
