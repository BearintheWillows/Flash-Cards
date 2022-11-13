﻿using Spectre.Console;
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

}