using Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            var command = connection.CreateCommand();
            command.CommandText = @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Stacks' AND xtype='U')
                            CREATE TABLE Stacks(
                            Id INT IDENTITY PRIMARY KEY,
                            Name VARCHAR(255) NOT NULL,
                            CardId INT FOREIGN KEY REFERENCES FlashCards(Id)
                            )";
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    //Create table
    private void CreateFlashCardTable()
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = connection.CreateCommand();
        connection.Open();
        command.CommandText = @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE
           name='FlashCards' AND xtype='U')
                                CREATE TABLE FlashCards
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

    public void AddStack( Stack stack )
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = connection.CreateCommand();
        connection.Open();
        command.CommandText = @"INSERT INTO Stacks (Name)
                                    VALUES (@name)";
        command.Parameters.AddWithValue( "@name", stack.Name );
        command.ExecuteNonQuery();
    }

    //view stacks
    public List<Stack> GetAllStacks()
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = connection.CreateCommand();
        connection.Open();
        command.CommandText = @"SELECT * FROM Stacks";
        var reader = command.ExecuteReader();
        var stacks = new List<Stack>();
        while ( reader.Read() )
        {
            var stack = new Stack
            {
                Id = (int)reader["Id"],
                Name = (string)reader["Name"]
            };
            stacks.Add( stack );
        }
        return stacks;
    }

}
