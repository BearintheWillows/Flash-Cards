using Flash_Cards.Data;
using Flash_Cards.UI;
using Models;

namespace Flash_Cards.Controllers;
internal static class CardController
{
    public static IEnumerable<CardDto> SetStackPositions( IEnumerable<CardDto> cards )
    {
        int stackPosition = 1;
        foreach ( var card in cards )
        {
            card.StackPosition = stackPosition;
            stackPosition++;
        }
        return cards;
    }

    internal static void EditCard( CardDto card, FlashCardContext db )
    {
        DataInput.GetEditCardInput( card );
        db.UpdateCard( card );
        
    }
}
