using Flash_Cards.Data;
using Models;
using Spectre.Console;

namespace Flash_Cards.Controllers;
internal class StackController
{


    public static Stack GetStackById( int id, FlashCardContext db )
    {
        Stack stack = db.GetStackById( id );
        if ( stack != null )
        {
            return stack;
        }
        else
        {
            AnsiConsole.MarkupLine( "[red]Stack not found.[/]" );
            return null;
        }

    }
}
