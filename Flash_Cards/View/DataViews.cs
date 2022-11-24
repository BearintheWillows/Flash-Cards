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
    public static void ViewAll<T>( IEnumerable<T> obj )
    {
        var table = new Table().Expand();
        var rowNum = 1;
        
        if ( obj is IEnumerable<StackDto> stack )
        {
            table.AddColumn( "Id" );
            table.AddColumn( "Name" );
            table.AddColumn( "Card Count" );
            

            foreach ( var item in stack )
            {
                if ( rowNum % 2 == 0 )
                {
                    table.AddRow( $"[black on grey82]{item.Id}[/]",
                                 $"[black on grey82]{item.Name}[/]",
                                 $"[black on grey82]{item.Count}[/]" );

                }
                else
                {
                    table.AddRow( item.Id.ToString(),
                    item.Name, item.Count.ToString() );
                }
                rowNum++;
            }

        } else if (obj is IEnumerable<CardDto> card)
        {
            table.AddColumn( "Id" );
            table.AddColumn( "Front" );
            table.AddColumn( "Back" );
            
            foreach ( var item in card )
            {
                if ( rowNum % 2 == 0 )
                {
                    table.AddRow( $"[black on grey82]{rowNum}[/]",
                                 $"[black on grey82]{item.Question}[/]",
                                 $"[black on grey82]{item.Answer}[/]" );

                }
                else
                {
                    table.AddRow( rowNum.ToString(), item.Question, item.Answer );
                }
                rowNum++;
            }
        }


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
    public static void ViewById<T>( T obj)
    {
        Console.Clear();
        if ( obj is StackDto stack )
        {
            var table = new Table().Expand();
            table.AddColumn( "Id" );
            table.AddColumn( "Name" );
            table.AddColumn( "Card Count" );
            table.AddRow( stack.Id.ToString(), stack.Name, stack.Count.ToString() );
            AnsiConsole.Write( table );
        }
        else if ( obj is CardDto card )
        {
            var table = new Table().Expand();
            table.AddColumn( "Id" );
            table.AddColumn( "Front" );
            table.AddColumn( "Back" );
            table.AddRow( card.StackPosition.ToString(), card.Question, card.Answer );
            AnsiConsole.Write( table );
        }
    }

    internal static void ViewAllCards( List<CardDto> cards )
    {
        foreach ( var item in cards )
        {
            Console.WriteLine( $"Id: {item.Id} | Question: {item.Question} | Answer: {item.Answer}" );
        }
        Console.ReadKey();
    }

    internal static void ViewCardById( Card cardChoice )
    {
        //TODO - Implement ViewCardById
    }
}
