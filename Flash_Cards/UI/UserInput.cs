using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flash_Cards.UI;
internal static class UserInput
{
    public static string ShowStackMenu()
    {
        var rule = new Rule("[bold blue]Main Menu[/]");
        AnsiConsole.Write( rule );

        var menu = AnsiConsole.Prompt(
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
        return menu;
    }

}
