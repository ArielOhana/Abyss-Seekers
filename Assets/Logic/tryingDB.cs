using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using Mono.Data.Sqlite;

public class TryingDB : MonoBehaviour
{
    private string connectionString = "URI=file:Inventory.db";

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD
        Debug.Log("Broker");
=======
>>>>>>> 50cda48adafea976dd87f7ecdb35b4f881aba08e
        CreateDB();
    }

    public void CreateDB()
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
<<<<<<< HEAD
=======

>>>>>>> 50cda48adafea976dd87f7ecdb35b4f881aba08e
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS weapons (name VARCHAR(20), damage INT);";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
=======
        Debug.Log("hello");
>>>>>>> 50cda48adafea976dd87f7ecdb35b4f881aba08e
    }
}
