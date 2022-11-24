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
        List<CardDto> cards = db.GetAllCardsByStackId( stack.Id );
        cards = (List<CardDto>)CardController.SetStackPositions( cards );
        switch ( choice.ToLower() )
        {
            case "add a new card":
                var card = MenuInputs.GetNewCardInput();
                stackController.AddCardToStack( stack.Id, card );
                InspectStackMenu( stack );
                break;
            case "view all cards":
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
                CardDto? cardChoice = cards.AsQueryable().FirstOrDefault( c => c.StackPosition == cardId );
                if ( cardChoice != null )
                {
                    InspectCardMenu( cardChoice, stack );
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

    public void InspectCardMenu( CardDto card, StackDto stack )
    {
        Console.Clear();
        DataViews.ViewById( card );
        Rule rule = new Rule( $"[bold blue]Inspecting: Card #{card.StackPosition} from Stack: {stack.Name}[/]" );
        AnsiConsole.Write( rule );
        string input = MenuInputs.GetInspectCardMenuInput();
        switch ( input.ToLower( System.Globalization.CultureInfo.InvariantCulture ) )
        {
            case "update card":
                CardController.EditCard( card, db );
                //db.UpdateCard( id, card );
                break;
            case "delete card":
                if ( MenuInputs.ConfirmChoice() )
                {
                   // db.DeleteCard( id );
                    var deleteRule = new Rule( "[bold red] Record Deleted[/]" );
                    AnsiConsole.Write( deleteRule );
                    
                }
                else
                {
                    var notDeleteRule = new Rule( "[bold red] Record NOT Deleted[/]" );
                    AnsiConsole.Write( notDeleteRule );
                 
                }
                break;
            case "back to stack":
                InspectStackMenu( stack );
                break;
        }
        InspectStackMenu( stack );
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
