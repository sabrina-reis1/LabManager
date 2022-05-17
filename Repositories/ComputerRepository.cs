using LabManager.Models;
using Microsoft.Data.Sqlite;

namespace LabManager.Repositories;

class ComputerRepository
{
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
                reader.GetInt32(0), reader.GetString(1), reader.GetString(2)
            );
            computers.Add(computer); //adicionando os atributos dos computadores no array list

        }
        connection.Close();

        return computers;
    }
}