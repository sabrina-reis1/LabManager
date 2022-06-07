using LabManager.Database;
using LabManager.Models;
using Microsoft.Data.Sqlite;

namespace LabManager.Repositories;

class ComputerRepository
{
    private DatabaseConfig databaseConfig;
    
    public ComputerRepository(DatabaseConfig databaseConfig) 
    {
        this.databaseConfig = databaseConfig;
    }

    public List<Computer> GetAll()
    {
        var computers = new List<Computer>(); //criando uma lista vazia

        var connection = new SqliteConnection("Data Source=database.db"); //ctrl . criar banco de dados com SQL
        connection.Open(); //abrindo uma conexão

        var command = connection.CreateCommand(); //criando comando
        command.CommandText = "SELECT * FROM Computers"; //parametros a serem substituídos por valores

        var reader = command.ExecuteReader(); //executando comando

        while(reader.Read()) //pegar todas as linhas enquanto for true
        {
            var computer = new Computer(
                reader.GetInt32(0), reader.GetString(1), reader.GetString(2));

                computers.Add(readerToComputer(reader));
            
         //adicionando os atributos dos computadores no array list
        }
        connection.Close();

        return computers;
    }

    public Computer Save(Computer computer)
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString); //ctrl . criar banco de dados com SQL
        connection.Open(); //abrir uma conexão

        var command = connection.CreateCommand(); //criando comando
        command.CommandText = "INSERT INTO Computers VALUES($id, $ram, $processor);"; //parametros a serem substituídos por valores
        command.Parameters.AddWithValue("$id", computer.Id);
        command.Parameters.AddWithValue("$ram", computer.Ram);
        command.Parameters.AddWithValue("$processor", computer.Processor);

        command.ExecuteNonQuery();
        connection.Close();
        return computer;
    }

   public Computer GetById(int id)
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Computers WHERE id = $id;";
        command.Parameters.AddWithValue("$id",id);
        
        var reader = command.ExecuteReader();
        reader.Read(); //para passar para outra linha
        var computer = new Computer(reader.GetInt32(0),reader.GetString(1),reader.GetString(2));

        connection.Close();

        return computer;
    }

    public Computer Update(Computer computer)
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "UPDATE Computers SET ram = $ram, processor = $processor WHERE id = $id;"; //where mostra qual linha vc quer q seja atualizada
        command.Parameters.AddWithValue("$id", computer.Id);
        command.Parameters.AddWithValue("$ram", computer.Ram);
        command.Parameters.AddWithValue("$processor", computer.Processor);

        command.ExecuteNonQuery();
        connection.Close();
        return computer;
    }

    public void Delete (int id)
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Computers WHERE id = $id;";
        command.Parameters.AddWithValue("$id", id);

        command.ExecuteNonQuery(); // pq nao pega uma resposta do banco de dados 
        connection.Close();
    }

    public bool existsById(int id)
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT count(id) FROM Computers WHERE id = $id;";
        command.Parameters.AddWithValue("$id", id);

        bool result = Convert.ToBoolean(command.ExecuteScalar());

        return result;
    }    

    private Computer readerToComputer(SqliteDataReader reader)
    {
        var Computer = new Computer(reader.GetInt32(0),reader.GetString(1),reader.GetString(2));

        return Computer;
    }
}

//SELECT count(id) FROM Computers WHERE id = $id;