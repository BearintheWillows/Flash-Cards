using Flash_Cards.Models;
using Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flash_Cards.View;
internal class DataViews
{
    /// <summary>
    /// Displays a table of all stacks within a collection
    /// </summary>
    /// <param name="stack"></param>
    public static void ViewAllStacks( List<StackDto> stack )
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
                             $"[black on grey82]{item.Name}[/]" ,
                             $"[black on grey82]{item.Count}[/]");

            }
            else
            {
                table.AddRow( item.Id.ToString(),
                item.Name, item.Count.ToString() );
            }


        }
        rowNum++;


        AnsiConsole.Write( table );
        var rule = new Rule("[bold green]Press any key to return to the menu[/]");
        AnsiConsole.Write( rule );
        Console.ReadKey();
        Console.Clear();
    }
    
    /// <summary>
    /// Displays a singular stack and shows confirmation whether to delete stack
    /// </summary>
    /// <param name="stack"></param>
    public static void ViewStackById( StackDto stack )
    {
        Console.Clear();
        Rule rule = new Rule( $"[bold green]Stack Name: {stack.Name}[/]" );
        AnsiConsole.Write( rule );
        var table = new Table().Expand();
        table.AddColumn( "Id" );
        table.AddColumn( "Name" );
        table.AddColumn( "Card Count" );
        table.AddRow( stack.Id.ToString(),
                      stack.Name, stack.Count.ToString() );
        AnsiConsole.Write( table );

     
        
    }
}
