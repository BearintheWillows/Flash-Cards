using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flash_Cards.UI;
internal class DataInput
{
    internal static int IdInput()
    {
        var id = AnsiConsole.Ask<int>( "Please enter the Id of the stack you would like to view:" );
        return id;
    }
}
