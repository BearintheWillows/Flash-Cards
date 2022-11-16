using Flash_Cards.Data;
using Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flash_Cards.UI;
internal static class UserInput
{
    public static string GetStackMenuInput()
    {
        var rule = new Rule("[bold blue]Main Menu[/]");
        AnsiConsole.Write( rule );

        var menuChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Please select an option:")
                .PageSize(5)
                .AddChoices(new[]
                {
                    "Create a new stack",
                    "View all stacks",
                    "Delete a stack",
                    "Exit"
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
                    "Go to Stack Menu",
                }));
            return menuChoice;
        }
    }

    public static Stack GetNewStackInput()
    {
        {
        var rule = new Rule("[bold blue]Create a new stack[/]");
        AnsiConsole.Write( rule );

            string input = AnsiConsole.Ask<string>("Please enter a name for your new stack:");
            while (!Validation.ValidStackName( input ) )
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
        while ( !Validation.ValidStackId( input) )
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
}
