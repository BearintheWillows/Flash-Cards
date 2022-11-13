﻿using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Flash_Cards;
internal class Validation
{
    
    public static bool ValidStackName( string name )
    {
        //only letters regex
        var regex = new Regex(@"^[a-zA-Z]+$");

        if ( name.Length < 1)
        {
            AnsiConsole.MarkupLine( "[red] Please enter a name: [/]" );
            return false;
        }
        if ( name.Length > 10 )
        {
            AnsiConsole.MarkupLine( "[red]Please enter a name less than 10 characters: [/]" );
            return false;
        }
        if ( regex.IsMatch( name ) == false )
        {
            AnsiConsole.MarkupLine( "[red]Only letters allowed.[/]" );
            return false;
        }

        return true;


    }
}