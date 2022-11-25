using Flash_Cards.Data;
using Flash_Cards.Models;
using Flash_Cards.UI;
using Flash_Cards.View;
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

    internal static void ViewAllSessions(FlashCardContext db)
    {
        List<Session> sessions = db.GetAllSessions();
        DataViews.ViewAll( sessions );
    }

    //view sessions by stackid
    internal static void ViewSessionsByStackId( FlashCardContext db )
    {
        int stackId = DataInput.StackIdInput();
        List<Session> sessions = db.GetAllSessions();
        sessions = sessions.Where( s => s.StackId == stackId ).ToList();
        DataViews.ViewAll( sessions );
    }
}
