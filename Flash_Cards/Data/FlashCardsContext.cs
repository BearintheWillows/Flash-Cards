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
        CreateStacksTable();
        CreateFlashCardTable();
        
    }
    private void CreateStacksTable()
    {
        {
            using var connection = new SqlConnection(_connectionString);
            var command = connection.CreateCommand();
            command.CommandText = @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='stacks' AND xtype='U')
                            CREATE TABLE stacks(
                            Id INT IDENTITY(1,1) PRIMARY KEY,
                            Name VARCHAR(255) NOT NULL,
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
           name='flashcards' AND xtype='U')
                                CREATE TABLE flashcards
                                (Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
                                Question VARCHAR(255) NOT NULL,
                                Answer VARCHAR(255) NOT NULL,
                                StackId int FOREIGN KEY REFERENCES stacks(Id)
                                )";
                               

        try
        {
            command.ExecuteNonQuery();
            Log.Information( "Flashcard table Created Successfully" );
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

    internal Stack? GetStackById( int input )
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = connection.CreateCommand();
        connection.Open();
        command.CommandText = @"SELECT * FROM Stacks WHERE Id = @id";
        command.Parameters.AddWithValue( "@id", input );
        var reader = command.ExecuteReader();
        reader.Read();

        //check if stack exists
        if ( reader.HasRows )
        {
            var stack = new Stack
            {
                Id = (int)reader["Id"],
                Name = (string)reader["Name"]
            };
            Log.Information( $"Stack {stack.Name} found" );
            return stack;
        }
        else
        {
            return null;
        }
    }

    //delete stack
    public void DeleteStack( int id )
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = connection.CreateCommand();
        connection.Open();
        command.CommandText = @"DELETE FROM Stacks WHERE Id = @id";
        command.Parameters.AddWithValue( "@id", id );
        command.ExecuteNonQuery();
    }

    //create a card and add to a stack
    public void AddCard( Card card)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = connection.CreateCommand();
        connection.Open();
        command.CommandText = @"INSERT INTO flashcards (Question, Answer, StackId)
                                    VALUES (@question, @answer, @stackId)";
        command.Parameters.AddWithValue( "@question", card.Question );
        command.Parameters.AddWithValue( "@answer", card.Answer );
        command.Parameters.AddWithValue( "@stackId", card.StackId );
        command.ExecuteNonQuery();
    }

    
}
