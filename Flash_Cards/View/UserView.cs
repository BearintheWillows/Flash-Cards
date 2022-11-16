using Models;
using Spectre.Console;

namespace Flash_Cards.View;
internal static class UserView
{
    public static void DisplayStacks( List<Stack> stack )
    {
        var table = new Table().Expand();

        var rowNum = 1;
        table.AddColumn( "Id" );
        table.AddColumn( "Name" );
        table.AddColumn( "Card Count" );


        foreach ( var item in stack )
        {
            if ( rowNum % 2 == 0 )
            {
                table.AddRow( $"[black on grey82]{item.Id}[/]",
                             $"[black on grey82]{item.Name}[/]" );

            }
            else
            {
                table.AddRow( item.Id.ToString(),
                item.Name );
            }


        }
        rowNum++;


        AnsiConsole.Write( table );
        var rule = new Rule("[bold green]Press any key to return to the menu[/]");
        AnsiConsole.Write( rule );
        Console.ReadKey();
        Console.Clear();
    }

    public static void DisplayStackToDelete( Stack stack )
    {
        var table = new Table().Expand();
        table.AddColumn( "Id" );
        table.AddColumn( "Name" );
        table.AddColumn( "Card Count" );
        table.AddRow( stack.Id.ToString(),
                      stack.Name );
        AnsiConsole.Write( table );
        var rule = new Rule("[bold red]DELETE RECORD?[/]");
        AnsiConsole.Write( rule );

    }


}
