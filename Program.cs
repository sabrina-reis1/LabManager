﻿using Microsoft.Data.Sqlite;
using LabManager.Database;
using LabManager.Repositories;
using LabManager.Models;

//routing == roteamento

var modelName = args[0];
var modelAction = args[1]; //action

var databaseConfig = new DatabaseConfig();
new DatabaseSetup(databaseConfig);

//computers
if(modelName == "Computer")
{
    var computerRepository = new ComputerRepository(databaseConfig);
    if(modelAction == "List")
    {
        Console.WriteLine("Computer List");
        
        foreach (var computer in computerRepository.GetAll())
        {
            Console.WriteLine("{0}, {1}, {2}", computer.Id, computer.Ram, computer.Processor);
        }
    }
    if(modelAction == "New")
    {
        Console.WriteLine("Computer New");
        var id = Convert.ToInt32(args[2]); //converter para string pq args é tipo string
        var ram = args[3];
        var processor = args[4]; //pegamos o que o usuário colocou

        var computer = new Computer(id, ram, processor);
        computerRepository.Save(computer);
    }
    if(modelAction == "Show")
    {
        Console.WriteLine("Computer Show");
        var id = Convert.ToInt32(args[2]);

        if(computerRepository.existsById(id))
        {
            var computer = computerRepository.GetById(id); // compyterrepository é de onde está vindo o código
            Console.WriteLine("{0},{1},{2}", computer.Id, computer.Ram, computer.Processor);
        } else {
            Console.WriteLine($"O computador ${id} não existe");
        }
    }
    if(modelAction == "Update")
    {
        Console.WriteLine("Computer Update");
        var id = Convert.ToInt32(args[2]);
        var ram = args[3];
        var processor = args[4];

        var computer = new Computer(id, ram, processor);
        computerRepository.Update(computer);
        Console.WriteLine("{0},{1},{2}", computer.Id, computer.Ram, computer.Processor);
    }
    if(modelAction == "Delete")
    {
        Console.WriteLine("Computer Delete");
        var id = Convert.ToInt32(args[2]);
        
        computerRepository.Delete(id);
        Console.WriteLine("Computer {0}", id);
    }
}

//labs

if(modelName == "Lab")
{
        if(modelAction == "List")
        {
        Console.WriteLine("Lab List");
        var connection = new SqliteConnection("Data Source=database.db");
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM labs";

        var reader = command.ExecuteReader();

        while(reader.Read())
        {
            Console.WriteLine(
                "{0}, {1}, {2}, {3}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)
            );
        }

        reader.Close();
        connection.Close();
    }

    if(modelAction == "New")
    {
        var id = Convert.ToInt32(args[2]);
        var number = args[3];
        var name = args[4];
        var block = args[5];

        var connection = new SqliteConnection("Data Source=database.db"); //ctrl . criar banco de dados com SQL
        connection.Open(); //abrir uma conexão

        var command = connection.CreateCommand(); //criando comando
        command.CommandText = "INSERT INTO Labs VALUES($id, $number, $name, $block);"; //parametros a serem substituídos por valores
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$number", number);
        command.Parameters.AddWithValue("$name", name);
        command.Parameters.AddWithValue("$block", block);

        command.ExecuteNonQuery();
        connection.Close();
    }
}