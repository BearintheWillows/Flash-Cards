using Flash_Cards.Data;
using Flash_Cards.Models;
using Flash_Cards.UI;
using Flash_Cards.View;
using Models;
using Spectre.Console;
using System.Collections.Generic;
using System.Data;

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
                int id = DataInput.StackIdInput();
                StackDto? stack = stackController.GetStackById(id);

                if ( stack != null )
                {
                    InspectStackMenu( stack );
                }
                else
                {
                    Console.Clear();
                    var rule = new Spectre.Console.Rule( "[bold red]Stack not found.[/]" );
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
        stack = stackController.GetStackById( stack.Id )!;
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
                    db.DeleteSessions( stack.Id );
                    var rule = new Spectre.Console.Rule($"[bold red] Record Deleted[/]");
                    AnsiConsole.Write( rule );

                    ManageStackMenu();
                }
                else
                {
                    var rule = new Spectre.Console.Rule("[bold red] Record NOT Deleted[/]");
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
        var rule = new Spectre.Console.Rule( $"[bold blue]Inspecting: Card #{card.StackPosition} from Stack: {stack.Name}[/]" );
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
                    db.DeleteCard( card.Id );
                    var deleteRule = new Spectre.Console.Rule( "[bold red] Record Deleted[/]" );
                    AnsiConsole.Write( deleteRule );
                    MenuViews.ContinueConfirm();

                }
                else
                {
                    var notDeleteRule = new Spectre.Console.Rule( "[bold red] Record NOT Deleted[/]" );
                    AnsiConsole.Write( notDeleteRule );
                    MenuViews.ContinueConfirm();

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
                Console.Clear();
                PlayStackMenu();
                break;


            case "Manage Stacks!":
                Console.Clear();
                ManageStackMenu();
                break;
            case "View Sessions":
                Console.Clear();
                ManageViewSessionsMenu();
                SessionController.ViewAllSessions(db);
                break;

            case "Exit":
                exitProgram = true;
                break;
        }
        return exitProgram;
    }

    private void ManageViewSessionsMenu()
    {
        string choice = MenuInputs.GetManageSessionsMenuInput();
        switch ( choice )
        {
            case "View All Sessions":
                SessionController.ViewAllSessions( db );
                break;
            case "View Sessions by Stack":
                SessionController.ViewSessionsByStackId( db );
                MainMenu();
                break;
            case "Back to Main Menu":
                MainMenu();
                break;
        }
    }

    private void PlayStackMenu()
    {
        var stackId = DataInput.StackIdInput();
        var stack = stackController.GetStackById( stackId );
        var cards = db.GetAllCardsByStackId( stackId );
        List<Round> rounds = new();
        var session = new Session { StackId = stackId, SessionDate = DateTime.Now  };

        int  roundNum = 1;

        while ( roundNum < cards.Count )
        {
            foreach ( var card in cards )
            {
                var round = new Round { CardId = card.Id, RoundNumber = roundNum, SessionId = session.Id };
                Console.Clear();
                var rule = new Spectre.Console.Rule( $"[bold blue]Round {roundNum}[/]" );
                AnsiConsole.Write( rule );
                AnsiConsole.MarkupLine( $"[yellow] Question: {card.Question}[/]" );
                Console.WriteLine( "Press any key to reveal answer." );
                Console.ReadKey();
                AnsiConsole.MarkupLine( $"[green]{card.Answer}[/]" );
                if (AnsiConsole.Confirm( $"[yellow] Did you guess correctly?[/]" ))
                {
                    round.Correct = 1;
                }
                else
                {
                    round.Correct = 0;
                }
   
                Console.WriteLine( "Press any key to continue." );
                Console.ReadKey();
                roundNum++;

                rounds.Add( round );
            }
            IEnumerable < Round > TotalQuery = rounds.AsQueryable().Where( r => r.Correct == 1 );
            session.Total = TotalQuery.Count();
        }
        Console.Clear();
        var endRule = new Spectre.Console.Rule( "[bold blue]End of Stack[/]" );
        AnsiConsole.Write( endRule );
        AnsiConsole.MarkupLine( $"[green]You got {session.Total} correct out of {rounds.Count}[/]" );
        SessionController.AddSession( session, rounds, db );
        MenuViews.ContinueConfirm();
        MainMenu();
    }
       
}
