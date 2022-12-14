using Flash_Cards.Data;
using Flash_Cards.Models;
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


    public StackDto? GetStackById( int id)
    {
        Stack? stack = db.GetStackById( id );
       
        if ( stack != null )
        {
            StackDto newStack = new StackDto
            {
                Id = stack.Id,
                Name = stack.Name,
                Count = db.GetCardCountByStackId( id )
            };
            return newStack;
        }
        else
        {
            AnsiConsole.MarkupLine( "[red]Stack not found.[/]" );
            return null;
        }

    }

    //add card  
    public void AddCardToStack( int id, Card card)
    {
        Stack? stack = db.GetStackById( id );
        
        if ( stack != null )
        {
            card.StackId = id;
            db.AddCard( card );
          
        }
        else
        {
            AnsiConsole.MarkupLine( "[red]Stack not found.[/]" );
        }
    }
}
