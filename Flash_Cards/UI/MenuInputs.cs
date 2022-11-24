using Models;
using Spectre.Console;
using System.Runtime.CompilerServices;

namespace Flash_Cards.UI;
internal static class MenuInputs
{
    public static string GetStackMenuInput()
    {

        var rule = new Rule("[bold blue]Mange Stacks Menu[/]");
        AnsiConsole.Write( rule );

        var menuChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Please select an option:")
                .PageSize(5)
                .AddChoices(new[]
                {
                    "Create a new stack",
                    "View all stacks",
                    "Inspect a Stack",
                    "Back to Main Menu"
                }));
    
        return menuChoice;
    }

    public static string GetCardMenuInput()
    {
        {
            var rule = new Rule("[bold blue]Flash Card Menu[/]");
            AnsiConsole.Write( rule );

            var menuChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Please select an option:")
                .PageSize(10)
                .AddChoices(new[]
                {
                    "Create a new card",
                    "Select a card",
                    "Update a card",
                    "View all cards",
                    "Delete a card",
                    "Back to Main Menu",
                }));
            return menuChoice;
        }
    }

    public static string MainMenuInput()
    {

        var menuChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("   Main Menu")
                .PageSize(5)
                .AddChoices(new[]
                {
                    "Play a Stack!",
                    "Manage Stacks!",
                    "View Sessions",
                    "Exit"
                }));
        return menuChoice;
    }

    public static Stack GetNewStackInput()
    {
        {
            var rule = new Rule("[bold blue]Create a new stack[/]");
            AnsiConsole.Write( rule );

            string input = AnsiConsole.Ask<string>("Please enter a name for your new stack:");
            while ( !Validation.ValidStackName( input ) )
            {
                input = AnsiConsole.Ask<string>( "Please Try again: " );
            }

            var newStack = new Stack { Name = input };
            return newStack;
        }
    }

    internal static int GetStackToDelete()
    {
        var rule = new Rule("[bold Red]Delete a Stack");
        var input = AnsiConsole.Ask<int>("Please enter an Id of the stack you would like to delete: ");
        while ( !Validation.ValidStackId( input ) )
        {
            input = AnsiConsole.Ask<int>( "Please Try again: " );
        }

        return input;


    }

    internal static bool ConfirmChoice()
    {
        if ( AnsiConsole.Confirm( "Are you sure you want to delete?" ) )
        {
            AnsiConsole.MarkupLine( "[bold green]Record Deleted[/]" );
            return true;

        }
        else
        {
            return false;

        }
    }

    internal static string GetInspectStackMenuInput()
    {

        var menuChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Please select an option:")
                .PageSize(10)
                .AddChoices(new[]
                {
                    "Add a new card",
                    "Inspect a card",
                    "View all cards",
                    "Delete Stack",
                    "Back to Main Menu",
                }));

        return menuChoice;
    }

    internal static Card GetNewCardInput()
    {
        AnsiConsole.MarkupLine( "[bold blue]Create a new card[/]" );
        string question = AnsiConsole.Ask<string>( "Please enter a question for your new card:" );
        string answer = AnsiConsole.Ask<string>( "Please enter an answer for your new card:" );
        var newCard = new Card { Question = question, Answer = answer };
        return newCard;
    }

    internal static int GetCardIdInput()
    {
        int cardId = AnsiConsole.Ask<int>( "[bold blue]Enter card Id: [/]" );
        
        return cardId;
    }

    internal static string GetInspectCardMenuInput()
    {
        {
            var menuChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Please select an option:")
                .PageSize(10)
                .AddChoices(new[]
                {
                    "Update card",
                    "Delete card",
                    "Back to Stack",
                }));

            return menuChoice;
        }
    }
}
