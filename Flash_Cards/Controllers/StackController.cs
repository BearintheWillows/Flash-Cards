using Flash_Cards.Data;
using Models;
using Spectre.Console;

namespace Flash_Cards.Controllers;
internal class StackController
{
    public StackController( FlashCardContext db )
    {
        this.db = db;
    }

    private readonly FlashCardContext db;


    public Stack GetStackById( int id)
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
