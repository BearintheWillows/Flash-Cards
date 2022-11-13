using Serilog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flash_Cards.Data;
internal class FlashCardContext
{
    private readonly string _connectionString;
    public FlashCardContext( string connectionString )
    {
        _connectionString = connectionString;
    }
    public void CreateTables()
    {
        CreateFlashCardTable();
        CreateStacksTable();
    }
    private void CreateStacksTable()
    {
        {
            using var connection = new SqlConnection(_connectionString);
            var cmd = connection.CreateCommand();
            cmd.CommandText = @"CREATE TABLE Stacks(
                            Id INT IDENTITY PRIMARY KEY,
                            Name VARCHAR(255) NOT NULL,
                            CardId INT FOREIGN KEY REFERENCES FlashCards(Id)
                            )";
            connection.Open();
            cmd.ExecuteNonQuery();
        }
    }

    //Create table
    private void CreateFlashCardTable()
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = connection.CreateCommand();
        connection.Open();
        command.CommandText = @"CREATE TABLE FlashCards
                                (Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
                                Question VARCHAR(255) NOT NULL,
                                Answer VARCHAR(255) NOT NULL,
                                )";

        try
        {
            command.ExecuteNonQuery();
            Log.Information( "Table Created Successfully" );
        }
        catch ( Exception e)
        {

            Log.Warning( "Table Not created successfully" );
            Log.Error( e.Message);
        }
        

    }

}
