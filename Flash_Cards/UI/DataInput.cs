using Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flash_Cards.UI;
internal class DataInput
{
    internal static CardDto GetEditCardInput( CardDto card )
    {
        var questionRule = new Rule( $"[bold blue]Updating Question: [white]{card.Question}[/][/]" );
        AnsiConsole.Write( questionRule );
        AnsiConsole.MarkupLine( "[bold]Enter a new question: [/]" );
        var question = AnsiConsole.Ask<string>( $"Question: " );
        if ( question != "" )
        {
            card.Question = question;
        }
        var answerRule = new Rule( $"[bold blue]Updating Answer:  [white]{card.Answer}[/][/]" );
        AnsiConsole.Write( answerRule );
        AnsiConsole.MarkupLine( "[bold]Enter a new answer: [/]" );
        var answer = AnsiConsole.Ask<string>( $"Answer: " );
        if ( answer != "" )
        {
            card.Answer = answer;
        }
        return card;
    }

    internal static int StackIdInput()
    {
        var id = AnsiConsole.Ask<int>( "Please enter the Id of the stack you would like to view:" );
        return id;
    }
}
