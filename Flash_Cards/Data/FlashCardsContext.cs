using Flash_Cards.Models;
using Models;
using Serilog;
using Spectre.Console;
using System.Data.SqlClient;
using System.Reflection.Metadata;

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
        CreateRoundsTable();
        CreateSessionsTable();
        

    }

    private void CreateSessionsTable()
    {
        using SqlConnection connection = new SqlConnection( _connectionString );
        connection.Open();
        SqlCommand cmd = connection.CreateCommand();
        cmd.CommandText = @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='sessions' AND xtype='U')
                            CREATE TABLE sessions (
                            Id INT IDENTITY(1,1) PRIMARY KEY,
                            Total INT,
                            SessionDate DATETIME,
                            RoundId int FOREIGN KEY REFERENCES rounds(id),
                            StackId int FOREIGN KEY REFERENCES stacks(Id)
                            )";
        cmd.ExecuteNonQuery();
    }
    
    private void CreateRoundsTable()
    {
            using SqlConnection connection = new SqlConnection( _connectionString );
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='rounds' AND xtype='U')
                            CREATE TABLE rounds(
                            Id INT IDENTITY(1,1) PRIMARY KEY,
                            Correct BIT,
                            CardId int FOREIGN KEY REFERENCES flashcards(Id)
                            )";
            cmd.ExecuteNonQuery();
    }

    private void CreateStacksTable()
    {
        {
            using var connection = new SqlConnection(_connectionString);
            var command = connection.CreateCommand();
            command.CommandText = @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='stacks' AND xtype='U')
                            CREATE TABLE stacks(
                            Id INT IDENTITY(1,1) PRIMARY KEY,
                            Name VARCHAR(255) NOT NULL UNIQUE,
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
        catch ( Exception e )
        {

            Log.Warning( "Table Not created successfully" );
            Log.Error( e.Message );
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

        try
        {
            command.ExecuteNonQuery();
            Log.Information( "Stack Added Successfully" );
        }
        catch ( Exception e )
        {
            if ( e.Message.Contains( "Violation of UNIQUE KEY constraint" ) )
            {
                AnsiConsole.MarkupLine( $"[bold red] Stack already exists. NOT SAVED.[/]" );
                Log.Warning( "Stack already exists" );
            }
            else
            {
                Log.Error( e.Message );
            }
        }


    }

    //view stacks
    public List<StackDto> GetAllStacks()
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = connection.CreateCommand();
        connection.Open();
        command.CommandText = @"SELECT * FROM Stacks";
        var reader = command.ExecuteReader();
        var stacks = new List<StackDto>();
        while ( reader.Read() )
        {
            StackDto stack = new StackDto
            {
                Id = (int)reader["Id"],
                Name = (string)reader["Name"],
                Count = GetCardCountByStackId( (int)reader["Id"] )
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
    public void DeleteStackAndCards( int id )
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = connection.CreateCommand();
        connection.Open();
        command.CommandText = @"DELETE FROM Flashcards WHERE StackId = @id ; DELETE FROM Stacks WHERE Id = @id";
        command.Parameters.AddWithValue( "@id", id );
        command.ExecuteNonQuery();
    }

    //create a card and add to a stack
    public void AddCard( Card card )
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

    public int GetCardCountByStackId( int id )
    {
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            connection.Open();
            command.CommandText = @"SELECT COUNT(*) FROM flashcards WHERE StackId = @stackId";
            command.Parameters.AddWithValue( "@stackId", id );
            var reader = command.ExecuteReader();
            reader.Read();
            var count = (int)reader[0];
            Log.Information( $"There are {count} cards in the database" );
            return count;
            
        }
    }

    internal CardDto? GetCardById( int cardId )
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = connection.CreateCommand();
        connection.Open();
        command.CommandText = @"SELECT * FROM flashcards WHERE Id = @id";
        command.Parameters.AddWithValue( "@id", cardId );
        var reader = command.ExecuteReader();
        reader.Read();
        if ( reader.HasRows )
        {
            var card = new CardDto
            {
                Id = (int)reader["Id"],
                Question = (string)reader["Question"],
                Answer = (string)reader["Answer"],
            };
            Log.Information( $"Card {card.Question} found" );
            return card;
        }
        else
        {
            return null;
        }
    }

    internal List<CardDto> GetAllCardsByStackId( int id )
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = connection.CreateCommand();
        connection.Open();
        command.CommandText = @"SELECT * FROM flashcards WHERE StackId = @id";
        command.Parameters.AddWithValue( "@id", id );
        var reader = command.ExecuteReader();
        var cards = new List<CardDto>();
        while ( reader.Read() )
        {
            CardDto card = new CardDto
            {
                Id = (int)reader["Id"],
                Question = (string)reader["Question"],
                Answer = (string)reader["Answer"],
            };
            cards.Add( card );
        }
        return cards;
    }

    internal void UpdateCard( CardDto card )
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = connection.CreateCommand();
        connection.Open();
        command.CommandText = @"UPDATE flashcards SET Question = @question, Answer = @answer WHERE Id = @id";
        command.Parameters.AddWithValue( "@question", card.Question );
        command.Parameters.AddWithValue( "@answer", card.Answer );
        command.Parameters.AddWithValue( "@id", card.Id );
        command.ExecuteNonQuery();
    }

    internal void DeleteCard( int id )
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = connection.CreateCommand();
        connection.Open();
        command.CommandText = @"DELETE FROM flashcards WHERE Id = @id";
        command.Parameters.AddWithValue( "@id", id );
        command.ExecuteNonQuery();
    }
}
