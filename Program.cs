using Microsoft.Data.Sqlite;

var connection = new SqliteConnection("Data Source=database.db"); //ctrl . criar banco de dados com SQL
connection.Open(); //abrir uma conexão

var command = connection.CreateCommand(); //criando comando
command.CommandText = @" 
  CREATE TABLE IF NOT EXISTS Computers(
    id int not null primary key,
    ram varchar(100) not null,
    processor varchar(100) not null
  );
";

command.ExecuteNonQuery();
connection.Close(); // quando abrimos uma conexão, precisamos fechá-la

//routing == roteamento

var modelName = args[0];
var modelAction = args[1]; //action

if(modelName == "Computer")
{
    if(modelAction == "List")
    {
        Console.WriteLine("Computer List");
        connection = new SqliteConnection("Data Source=database.db"); //ctrl . criar banco de dados com SQL
        connection.Open(); //abrindo uma conexão

        command = connection.CreateCommand(); //criando comando
        command.CommandText = "SELECT * FROM Computers"; //parametros a serem substituídos por valores

        var reader = command.ExecuteReader();
        //reader.Read(); //pegar primeira linha
        //reader.Read(); //pegar segunda linha

        while(reader.Read()) //pegar todas as linhas enquanto for true
        {
            Console.WriteLine(
                "{0}, {1}, {2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2)
            );

        }

        reader.Close();
        connection.Close(); // quando abrimos uma conexão, precisamos fechá-la
    }
    if(modelAction == "New")
    {
        var id = Convert.ToInt32(args[2]); //converter para string pq args é tipo string
        var ram = args[3];
        var processor = args[4]; //pegamos o que o usuário colocou

        connection = new SqliteConnection("Data Source=database.db"); //ctrl . criar banco de dados com SQL
        connection.Open(); //abrir uma conexão

        command = connection.CreateCommand(); //criando comando
        command.CommandText = "INSERT INTO Computers VALUES($id, $ram, $processor);"; //parametros a serem substituídos por valores
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$ram", ram);
        command.Parameters.AddWithValue("$processor", processor);

        command.ExecuteNonQuery();
        connection.Close(); // quando abrimos uma conexão, precisamos fechá-la
    }
}