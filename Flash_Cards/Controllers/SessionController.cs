using Flash_Cards.Data;
using Flash_Cards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flash_Cards.Controllers;
internal static class SessionController
{
    internal static void AddSession( Session session, List<Round> rounds, FlashCardContext db  )
    {
        decimal Id = db.AddSession( session );
        foreach ( var item in rounds )
        {
            item.SessionId = (int)Id;
        } 
        db.AddRounds( rounds );
    }
}
