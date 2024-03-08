//using System.Data.SQLite;
//using Newtonsoft.Json;
//using System.IO;
//using System.Collections.Generic;
//using System.Data;
//using System.Collections;
//using Unity.VisualScripting.Dependencies.Sqlite;

//public class SQLdb
//{
//    private static readonly string DBFile = "./SQLdb.db";
//    private static readonly string JSONfile = "./Default_JSON.json";
//    private static SQLiteConnection? connection;

//    private static void OpenConnection()
//    {
//        try
//        {
//            string connectionString = $"Data Source={DBFile};";
//            connection = new SQLiteConnection(connectionString);
//            connection.Open();
//            Console.WriteLine("Connection opened");
//        }
//        catch (Exception e)
//        {
//            Console.WriteLine("Error: " + e.Message);
//        }
//    }

//    private static void FillTable(SQLiteCommand SQLCommends, string tableName, List<Dictionary<string, object>> data)
//    {
//        foreach (var item in data)
//        {
//            using (SQLiteCommand insertCommand = new SQLiteCommand(connection))
//            {
//                insertCommand.CommandText = $@"INSERT INTO {tableName} (";

//                foreach (var key in item.Keys)
//                {
//                    insertCommand.CommandText += $"{key}, ";
//                }
//                insertCommand.CommandText = insertCommand.CommandText.TrimEnd(',', ' ') + ") VALUES (";
//                foreach (var key in item.Keys)
//                {
//                    insertCommand.CommandText += $"@{key}, ";
//                    insertCommand.Parameters.AddWithValue($"@{key}", item[key]);
//                }
//                insertCommand.CommandText = insertCommand.CommandText.TrimEnd(',', ' ') + ")";
//                insertCommand.ExecuteNonQuery();

//            }
//            Console.WriteLine($"Finished adding data to {tableName}");
//        }

//    }
//    private static void UpdateHeroRow(SQLiteCommand command, Hero hero)
//    {

//        command.Parameters.Clear();
//    Example: command.CommandText = "UPDATE hero SET XP = @XP, Level = @Level  WHERE Id = @Id";
//        command.Parameters.AddWithValue("@XP", hero.XP);
//        command.Parameters.AddWithValue("@Level", hero.Level);
//        command.Parameters.AddWithValue("@Id", hero.ID);
//        command.ExecuteNonQuery();

//    }

//    private static void UpdateInventoryRow(SQLiteCommand command, Inventory inventory)
//    {
//        command.Parameters.Clear();
//    Example: command.CommandText = "UPDATE inventory SET WeponIDs = @WeponIDs, CurrentWeapon = @CurrentWeapon,  HelmetIDs = @HelmetIDs, " +
//        "                           CurrentHelmet = @CurrentHelmet, ArmourIDs = @ArmourIDs, CurrentArmour = @CurrentArmour, " +
//        "                           BootIDs = @BootIDs, CurrentBoot = @CurrentBoot,WHERE Id = @Id";

//        command.Parameters.AddWithValue("@WeaponIDs", inventory.WeaponIDs);
//        command.Parameters.AddWithValue("@CurrentWeapon", inventory.CurrentWeapon);
//        command.Parameters.AddWithValue("@HelmetIDs", inventory.HelmetIDs);
//        command.Parameters.AddWithValue("@CurrentHelmet", inventory.CurrentHelmet);
//        command.Parameters.AddWithValue("@ArmourIDs", inventory.ArmourIDs);
//        command.Parameters.AddWithValue("@CurrentArmour", inventory.CurrentArmour);
//        command.Parameters.AddWithValue("@BootIDs", inventory.BootIDs);
//        command.Parameters.AddWithValue("@CurrentBoot", inventory.CurrentBoot);
//        command.Parameters.AddWithValue("@Id", inventory.ID);
//        command.ExecuteNonQuery();
//    }

//    private static void UpdateStatsRow(SQLiteCommand command, Stats stats)
//    {
//        command.CommandText = @"UPDATE stats SET Damage = @Damage, Armour = @Armour, MaxHealth = @MaxHealth,HealthRegeneration = @HealthRegeneration, 
//                                            MovementSpeed = @MovementSpeed, EvadeRate = @EvadeRate, HitRate = @HitRate, CriticalChance = @CriticalChance, 
//                                            ArmorPenetration = @ArmorPenetration WHERE StatsID = @StatsID";

//        command.Parameters.AddWithValue("@Damage", stats.Damage);
//        command.Parameters.AddWithValue("@Armour", stats.Armour);
//        command.Parameters.AddWithValue("@MaxHealth", stats.MaxHealth);
//        command.Parameters.AddWithValue("@HealthRegeneration", stats.HealthRegeneration);
//        command.Parameters.AddWithValue("@MovementSpeed", stats.MovementSpeed);
//        command.Parameters.AddWithValue("@EvadeRate", stats.EvadeRate);
//        command.Parameters.AddWithValue("@HitRate", stats.HitRate);
//        command.Parameters.AddWithValue("@CriticalChance", stats.CriticalChance);
//        command.Parameters.AddWithValue("@ArmorPenetration", stats.ArmorPenetration);
//        command.Parameters.AddWithValue("@StatsID", stats.StatsID);

//        command.ExecuteNonQuery();
//    }

//    public static void FillDB()
//    {
//        try
//        {
//            using (connection)
//            {
//                OpenConnection();
//                SQLiteCommand command = new SQLiteCommand(connection);
//                string jsonContent = DBFile.ReadAllText(JSONfile);
//                var jsonData = JsonConvert.DeserializeObject<Dictionary<string, List<Dictionary<string, object>>>>(jsonContent);
//                foreach (var table in jsonData)
//                {
//                    command.CommandText = $"SELECT COUNT(*) FROM {table.Key}";
//                    int rowCount = Convert.ToInt32(command.ExecuteScalar());
//                    if (rowCount < table.Value.Count)
//                    {
//                        FillTable(command, table.Key, table.Value);
//                    }
//                }
//                Console.WriteLine($"finished updating data");
//            }
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine("Error: " + ex.Message);
//        }
//        finally
//        {
//            if (connection.State == ConnectionState.Open)
//            {
//                connection.Close();
//            }
//            Console.WriteLine("Connection closed");
//        }
//    }

//    public static void entityToUpdate(object entity)
//    {
//        try
//        {
//            using (connection)
//            {
//                OpenConnection();
//                SQLiteCommand command = new SQLiteCommand(connection);
//                switch (entity)
//                {
//                    case Hero hero:
//                        UpdateHeroRow(command, hero);
//                        break;
//                    case Inventory inventory:
//                        UpdateInventoryRow(command, inventory);
//                        break;
//                    case Stats stats:
//                        UpdateStatsRow(command, stats);
//                        break;
//                    default:
//                        Console.WriteLine("Unsupported entity type.");
//                        break;
//                }
//            }
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine("Error: " + ex.Message);
//        }
//        finally
//        {
//            if (connection.State == ConnectionState.Open)
//            {
//                connection.Close();
//            }
//            Console.WriteLine("Connection closed");
//        }
//    }

//    public static void NewHero(String name, String role)
//    {
//        try
//        {
//            using (connection)
//            {
//                OpenConnection();
//                using (SQLiteCommand command = new SQLiteCommand(connection))
//                {
//                    command.CommandText = "SELECT * FROM roles WHERE name = @role";
//                    command.Parameters.AddWithValue("@role", role);
//                    List<object> roleKeys = new List<object>();
//                    List<object> roleValues = new List<object>();
//                    int weaponId = 1;
//                    int roleID = 1;
//                    using (SQLiteDataReader reader = command.ExecuteReader())
//                    {
//                        if (reader.Read())
//                        {
//                            Console.WriteLine($"Role information for '{role}':");
//                            for (int i = 2; i < 11; i++)
//                            {
//                                roleKeys.Add(reader.GetName(i));
//                                roleValues.Add(reader.GetValue(i));
//                            }
//                            roleID = (int)(long)reader["ID"];
//                            weaponId = (int)(long)reader.GetValue(12);
//                        }
//                        else
//                        {
//                            Console.WriteLine($"No role found with name '{role}'.");
//                        }
//                    }
//                    command.CommandText = $"INSERT INTO stats ({string.Join(", ", roleKeys)}) VALUES ({string.Join(", ", roleValues)})";
//                    command.ExecuteNonQuery();
//                    command.CommandText = "SELECT last_insert_rowid();";
//                    int statsId = Convert.ToInt32(command.ExecuteScalar());
//                    Console.WriteLine("entered stats");

//                    command.CommandText = "INSERT INTO inventory (WeaponIDs, CurrentWeapon, HelmetIDs, CurrentHelmet, ArmourIDs, CurrentArmour, BootIDs, CurrentBoot, Coins) " +
//                                          "VALUES (@WeaponId, @CurrentWeapon, '1', '1', '1', '1', '1', '1', 100)";
//                    command.Parameters.AddWithValue("@WeaponId", weaponId);
//                    command.Parameters.AddWithValue("@CurrentWeapon", weaponId);
//                    command.ExecuteNonQuery();
//                    Console.WriteLine("entered inventory");

//                    command.CommandText = "SELECT last_insert_rowid();";
//                    int inventoryId = Convert.ToInt32(command.ExecuteScalar());

//                    command.Parameters.Clear();
//                    command.CommandText = @"INSERT INTO hero (Name, Level, XP, StatsID, InventoryID, RoleID)" +
//                                          "VALUES (@Name, 1, 0, @StatsId, @InventoryId, @RoleID);";
//                    command.Parameters.AddWithValue("@Name", name);
//                    command.Parameters.AddWithValue("@StatsId", statsId);
//                    command.Parameters.AddWithValue("@InventoryId", inventoryId);
//                    command.Parameters.AddWithValue("@RoleID", roleID);
//                    command.ExecuteNonQuery();

//                }
//                connection.Close();
//            }
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine("Error: " + ex.Message);
//        }
//        finally
//        {
//            if (connection.State == ConnectionState.Open)
//            {
//                connection.Close();
//            }
//            Console.WriteLine("Connection closed");
//        }
//    }

//    public static void GetUser(string name)
//    {
//        try
//        {
//            using (connection)
//            {
//                OpenConnection();
//                using (SQLiteCommand command = new SQLiteCommand(connection))
//                {
//                    command.CommandText = "SELECT * FROM hero WHERE name = @Name LIMIT 1";
//                    command.Parameters.AddWithValue("@Name", name);
//                    using SQLiteDataReader data = command.ExecuteReader();
//                    if (data.Read())
//                    {
//                        Hero hero = new Hero
//                        {
//                            Id = data["HeroID"],
//                            Name = data["Name"],
//                            Level = data["Level"],
//                            xp = data["XP"],
//                            InventoryId = data["InventoryID"].
//                            RoleID = data["RoleID"]
//                        };
//                        Console.WriteLine(hero);
//                    }
//                    else
//                    {
//                        Console.WriteLine($"Hero with name '{nameToSearch}' not found.");
//                    }
//                }
//            }
//        }
//        catch (Exception e)
//        {
//            Console.WriteLine("Error at GetUser: " + e.Message);
//        }
//        finally
//        {
//            if (connection.State == ConnectionState.Open)
//            {
//                connection.Close();
//            }
//            Console.WriteLine("Connection closed");
//        }
//    }

//}
