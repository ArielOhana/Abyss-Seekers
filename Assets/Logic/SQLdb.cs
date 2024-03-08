using UnityEngine;
using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace DBContext
{
    public class SQLdb : MonoBehaviour
    {
        private string connectionString = "URI=file:" + Application.dataPath + "/emptyTables.db";
        private SqliteConnection DBConnection;
        string filePath = "Assets/Default_JSON.json";
        List<Dictionary<string, object>> jsonData;


        /*private void OpenConnection()
        {
            try
            {
                DBConnection = new SqliteConnection(dbFile);
                DBConnection.Open();
                Debug.Log("Connection opened");
            }
            catch (Exception e)
            {
                Debug.Log("Error: " + e.Message);
            }
            finally
            {
                DBConnection.Close();
            }
        }*/

        private void FillTable(SqliteCommand insertCommand, string tableName, Dictionary<string, object> data)
        {
            insertCommand.CommandText = $@"INSERT INTO {tableName} (";

            foreach (var key in data.Keys)
            {
                insertCommand.CommandText += $"{key}, ";
            }
            insertCommand.CommandText = insertCommand.CommandText.TrimEnd(',', ' ') + ") VALUES (";
            foreach (var key in data.Keys)
            {
                insertCommand.CommandText += $"@{key}, ";
                insertCommand.Parameters.AddWithValue($"@{key}", data[key]);
            }
            insertCommand.CommandText = insertCommand.CommandText.TrimEnd(',', ' ') + ")";
            insertCommand.ExecuteNonQuery();

            Debug.Log($"Finished adding data to {tableName}");
        }

        private void readJson()
        {
            string jsonContent;
            if (File.Exists(filePath))
            {
                jsonContent = File.ReadAllText(filePath);
                JsonDataWrapper wrapper = JsonUtility.FromJson<JsonDataWrapper>(jsonContent);
                if (wrapper != null && wrapper.Data != null)
                {
                    jsonData = wrapper.Data;

                    foreach (var item in jsonData)
                    {
                        foreach (var entry in item)
                        {
                            // Accessing Key and Value properties correctly
                            Debug.Log("working json");
                        }
                    }
                }
                else
                {
                    Debug.LogError("Failed to deserialize JSON data or the data structure is incorrect.");
                }
            }
            else
            {
                Debug.LogError($"File not found: {filePath}");
            }
        }
        private class JsonDataWrapper
        {
            public List<Dictionary<string, object>> Data;
        }

        public void FillDB()
        {
            try
            {
                readJson();
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = connectionString;
                        foreach (var table in jsonData)
                        {
                            command.CommandText = $"SELECT COUNT(*) FROM {table}";
                            int rowCount = Convert.ToInt32(command.ExecuteScalar());
                            if (rowCount < 1) // Check if the table has any rows
                            {
                                FillTable(command, "your_table_name_here", table);
                            }
                            Debug.Log("Finished updating data");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Log("Error: " + ex.Message);
            }
        }
    }
}

        /*
        private static void UpdateHeroRow(SQLiteCommand command, Hero hero)
        {

            command.Parameters.Clear();
            command.CommandText = "UPDATE hero SET XP = @XP, Level = @Level  WHERE Id = @Id";
            command.Parameters.AddWithValue("@XP", hero.XP);
            command.Parameters.AddWithValue("@Level", hero.Level);
            command.Parameters.AddWithValue("@Id", hero.ID);
            command.ExecuteNonQuery();

        }

        private static void UpdateInventoryRow(SQLiteCommand command, Inventory inventory)
        {
            command.Parameters.Clear();
            command.CommandText = "UPDATE inventory SET WeponIDs = @WeponIDs, CurrentWeapon = @CurrentWeapon,  HelmetIDs = @HelmetIDs, " +
                    "CurrentHelmet = @CurrentHelmet, ArmourIDs = @ArmourIDs, CurrentArmour = @CurrentArmour, " +
                    "BootIDs = @BootIDs, CurrentBoot = @CurrentBoot WHERE Id = @Id";

            command.Parameters.AddWithValue("@WeaponIDs", inventory.WeaponIDs);
            command.Parameters.AddWithValue("@CurrentWeapon", inventory.CurrentWeapon);
            command.Parameters.AddWithValue("@HelmetIDs", inventory.HelmetIDs);
            command.Parameters.AddWithValue("@CurrentHelmet", inventory.CurrentHelmet);
            command.Parameters.AddWithValue("@ArmourIDs", inventory.ArmourIDs);
            command.Parameters.AddWithValue("@CurrentArmour", inventory.CurrentArmour);
            command.Parameters.AddWithValue("@BootIDs", inventory.BootIDs);
            command.Parameters.AddWithValue("@CurrentBoot", inventory.CurrentBoot);
            command.Parameters.AddWithValue("@Id", inventory.ID);
            command.ExecuteNonQuery();
        }

        private static void UpdateStatsRow(SQLiteCommand command, Stats stats)
        {
            command.CommandText = @"UPDATE stats SET Damage = @Damage, Armour = @Armour, MaxHealth = @MaxHealth,HealthRegeneration = @HealthRegeneration, 
                                                MovementSpeed = @MovementSpeed, EvadeRate = @EvadeRate, HitRate = @HitRate, CriticalChance = @CriticalChance, 
                                                ArmorPenetration = @ArmorPenetration WHERE StatsID = @StatsID";

            command.Parameters.AddWithValue("@Damage", stats.Damage);
            command.Parameters.AddWithValue("@Armour", stats.Armour);
            command.Parameters.AddWithValue("@MaxHealth", stats.MaxHealth);
            command.Parameters.AddWithValue("@HealthRegeneration", stats.HealthRegeneration);
            command.Parameters.AddWithValue("@MovementSpeed", stats.MovementSpeed);
            command.Parameters.AddWithValue("@EvadeRate", stats.EvadeRate);
            command.Parameters.AddWithValue("@HitRate", stats.HitRate);
            command.Parameters.AddWithValue("@CriticalChance", stats.CriticalChance);
            command.Parameters.AddWithValue("@ArmorPenetration", stats.ArmorPenetration);
            command.Parameters.AddWithValue("@StatsID", stats.StatsID);
            command.ExecuteNonQuery();
        }

        

        public static void entityToUpdate(object entity)
        {
            try
            {
                OpenConnection();
                SQLiteCommand command = new SQLiteCommand(connection);
                switch (entity)
                {
                    case Hero hero:
                        UpdateHeroRow(command, hero);
                        break;
                    case Inventory inventory:
                        UpdateInventoryRow(command, inventory);
                        break;
                    case Stats stats:
                        UpdateStatsRow(command, stats);
                        break;
                    default:
                        Debug.Log("Unsupported entity type.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                Console.WriteLine("Connection closed");
            }
        }

        public static void NewHero(String name, String role)
        {
            try
            {
                OpenConnection();
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = "SELECT * FROM roles WHERE name = @role";
                    command.Parameters.AddWithValue("@role", role);
                    List<object> roleKeys = new List<object>();
                    List<object> roleValues = new List<object>();
                    int weaponId = 1;
                    int roleID = 1;
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Debug.Log($"Role information for '{role}':");
                            for (int i = 2; i < 11; i++)
                            {
                                roleKeys.Add(reader.GetName(i));
                                roleValues.Add(reader.GetValue(i));
                            }
                            roleID = (int)(long)reader["ID"];
                            weaponId = (int)(long)reader.GetValue(12);
                        }
                        else
                        {
                            Debug.Log($"No role found with name '{role}'.");
                        }
                    }
                    command.CommandText = $"INSERT INTO stats ({string.Join(", ", roleKeys)}) VALUES ({string.Join(", ", roleValues)})";
                    command.ExecuteNonQuery();
                    command.CommandText = "SELECT last_insert_rowid();";
                    int statsId = Convert.ToInt32(command.ExecuteScalar());
                    Debug.Log("Entered stats");

                    command.CommandText = "INSERT INTO inventory (WeaponIDs, CurrentWeapon, HelmetIDs, CurrentHelmet, ArmourIDs, CurrentArmour, BootIDs, CurrentBoot, Coins) " +
                                          "VALUES (@WeaponId, @CurrentWeapon, '1', '1', '1', '1', '1', '1', 100)";
                    command.Parameters.AddWithValue("@WeaponId", weaponId);
                    command.Parameters.AddWithValue("@CurrentWeapon", weaponId);
                    command.ExecuteNonQuery();
                    Debug.Log("Entered inventory");

                    command.CommandText = "SELECT last_insert_rowid();";
                    int inventoryId = Convert.ToInt32(command.ExecuteScalar());

                    command.Parameters.Clear();
                    command.CommandText = @"INSERT INTO hero (Name, Level, XP, StatsID, InventoryID, RoleID)" +
                                          "VALUES (@Name, 1, 0, @StatsId, @InventoryId, @RoleID);";
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@StatsId", statsId);
                    command.Parameters.AddWithValue("@InventoryId", inventoryId);
                    command.Parameters.AddWithValue("@RoleID", roleID);
                    command.ExecuteNonQuery();
                }
                Debug.Log("Hero created");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                Console.WriteLine("Connection closed");
            }
        }

        public static void GetUser(string name)
        {
            try
            {
                OpenConnection();
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = "SELECT * FROM hero WHERE name = @Name LIMIT 1";
                    command.Parameters.AddWithValue("@Name", name);
                    using SQLiteDataReader data = command.ExecuteReader();
                    if (data.Read())
                    {
                        Hero hero = new Hero
                        {
                            Id = (int)(long)data["HeroID"],
                            Name = (string)data["Name"],
                            Level = (int)(long)data["Level"],
                            xp = (int)(long)data["XP"],
                            InventoryId = (int)(long)data["InventoryID"],
                            RoleID = (int)(long)data["RoleID"]
                        };
                        Debug.Log(hero);
                    }
                    else
                    {
                        Debug.Log($"Hero with name '{name}' not found.");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error at GetUser: " + e.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                Console.WriteLine("Connection closed");
            }
        }

    }
}
*/
