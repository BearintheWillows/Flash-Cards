using Flash_Cards.UI;
using Models;
using Spectre.Console;

namespace Flash_Cards.View;
internal static class MenuViews
{

    public static void MainMenu()
    {
        //Displays initial UI to user for the Main Menu
        var rule = new Rule( "[bold green]Welcome to Flash Cards![/]" );
        AnsiConsole.Write( rule );
        var rule2 = new Rule( "[bold green]Please select an option from the menu below:[/]" );
        AnsiConsole.Write( rule2 );

    }

    
}
