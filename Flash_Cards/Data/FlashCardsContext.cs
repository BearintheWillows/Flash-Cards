using Serilog;
using System;
using System.Collections.Generic;
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

    //Create table
    public void CreateFlashCardTable()
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = connection.CreateCommand();
        command.CommandText = @"CREATE TABLE FlashCards (
                                Id INT NOT NULL, PRIMARY KEY, AUTO INCREMENT
                                Question TEXT,
                                Answer TEXT,
                                StackId int FOREIGN KEY REFERENCES Stack(Id)
                                )";
        connection.Open();

        try
        {
        command.ExecuteNonQuery();
        }
        catch ( Exception e)
        {

            Log.Warning( "Table Not created successfully" );
            Log.Error( e.Message);
        }
        
    }

    //Create Stack table
    public void CreateStackTable()
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = connection.CreateCommand();
        command.CommandText = @"CREATE TABLE Stack (
                                Id INT IDENTITY(1,1) PRIMARY KEY,
                                Name VARCHAR(255))";
        connection.Open();

        try
        {
            command.ExecuteNonQuery();
        }
        catch ( Exception e )
        {

            Log.Warning( "Table Not created successfully" );
            Log.Error( e.Message );
        }

    }

}
