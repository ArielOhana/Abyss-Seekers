using UnityEngine;
using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Newtonsoft.Json;


namespace Assets
{
    public class SQLdb 
    {
        private static string connectionString = "URI=file:Assets/Logic/DB/anotherDB.db";
        public static SqliteConnection DBConnection;
        private static string FilePath = "Assets/Logic/DB/Default_JSON.json";

        //connection to SQL tables
        private static void OpenConnection()
        {
            try
            {
                DBConnection = new SqliteConnection(connectionString);
                DBConnection.Open();
            }
            catch (Exception e)
            {
                Debug.Log("Error: " + e.Message);
            }
        }

        // check if the object lists ar full        
        public void ReadJson()
        {
            try
            {
                string jsonContent = File.ReadAllText(FilePath);
                var jsonData = JsonConvert.DeserializeObject<Dictionary<string, List<Dictionary<string, object>>>>(jsonContent);
                OpenConnection();
                using (DBConnection)
                {
                    foreach (var table in jsonData)
                    {
                        using (var command = new SqliteCommand())
                        {
                            command.Connection = DBConnection;
                            command.CommandText = $"SELECT COUNT(*) FROM {table.Key};";
                            int rowCount = Convert.ToInt32(command.ExecuteScalar());
                            if (rowCount < table.Value.Count)
                            {
                                BackendFunctions.FillTable(command, table.Key, table.Value);
                            }
                        }
                    }
                    Debug.Log($"finished updating data");
                }
                DBConnection.Close();
            }
            catch (Exception ex)
            {
                Debug.Log("Error: " + ex.Message);
            }
            finally
            {
                if (DBConnection.State == ConnectionState.Open)
                {
                    DBConnection.Close();
                }
            }
        }

        //general hero functions
        public void CreateHero(String name, String role)
        {
            try
            {
                OpenConnection();
                using (var command = new SqliteCommand())
                {
                    command.Connection = DBConnection;
                    command.CommandText = "SELECT * FROM roles WHERE name = @role";
                    command.Parameters.AddWithValue("@role", role);
                    List<object> roleKeys = new List<object>();
                    List<object> roleValues = new List<object>();
                    int weaponId = 1;
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            for (int i = 2; i < 11; i++)
                            {
                                roleKeys.Add(reader.GetName(i));
                                roleValues.Add(reader.GetValue(i));
                            }
                            weaponId = (int)(long)reader.GetValue(12);
                        }
                        else
                        {
                            Debug.Log($"No role found with name '{role}'.");
                        }
                    }
                    command.CommandText = $"INSERT INTO stats ({string.Join(", ", roleKeys)}) VALUES ({string.Join(", ", roleValues)});";
                    command.ExecuteNonQuery();
                    command.CommandText = "SELECT last_insert_rowid();";
                    int statsId = Convert.ToInt32(command.ExecuteScalar());



                    command.CommandText = "INSERT INTO inventory (WeaponIDs, CurrentWeapon, HelmetIDs, CurrentHelmet, ArmourIDs, CurrentArmour, BootIDs, CurrentBoot, Coins) " +
                                          "VALUES (@WeaponId, @CurrentWeapon, '1', '1', '1', '1', '1', '1', 1000);";
                    command.Parameters.AddWithValue("@WeaponId", weaponId);
                    command.Parameters.AddWithValue("@CurrentWeapon", weaponId);
                    command.ExecuteNonQuery();
                    command.CommandText = "SELECT last_insert_rowid();";
                    int inventoryId = Convert.ToInt32(command.ExecuteScalar());

                    command.Parameters.Clear();
                    command.CommandText = @"INSERT INTO hero (Name, Level, XP, StatsID, InventoryID, Role)" +
                                          "VALUES (@Name, 1, 0, @StatsId, @InventoryId, @Role);";
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@StatsId", statsId);
                    command.Parameters.AddWithValue("@InventoryId", inventoryId);
                    command.Parameters.AddWithValue("@Role", role);
                    command.ExecuteNonQuery();

                }
                DBConnection.Close();
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                if (DBConnection.State == ConnectionState.Open)
                {
                    DBConnection.Close();
                }
            }
        }
        public Hero GetHero(string name)
        {
            Hero hero = null;
            try
            {
                OpenConnection();
                using (var command = new SqliteCommand())
                {
                    command.Connection = DBConnection;
                    command.CommandText = "SELECT * FROM hero WHERE name = @Name LIMIT 1;";
                    command.Parameters.AddWithValue("@Name", name);
                    using SqliteDataReader data = command.ExecuteReader();
                    if (data.Read())
                    {
                        int heroId = data.IsDBNull(0) ? 0 : data.GetInt32(0);
                        string newName = data.IsDBNull(1) ? string.Empty : (string)data.GetValue(1);
                        int newLevel = data.IsDBNull(2) ? 0 : data.GetInt32(2);
                        int newXp = data.IsDBNull(3) ? 0 : data.GetInt32(3);
                        int statsId = data.IsDBNull(4) ? 0 : data.GetInt32(4);
                        Stats newStats = GetStats(statsId);
                        int inventoryId = data.IsDBNull(5) ? 0 : data.GetInt32(5);
                        Inventory newInventory = GetInventory(inventoryId);
                        string role = data.IsDBNull(6) ? string.Empty : (string)data.GetValue(6);
                        hero = new Hero(newName, newLevel, newXp, newStats, newInventory, role);
                        return hero;
                    }
                    else
                    {
                        DBConnection.Close();
                        Debug.Log($"Hero with name '{name}' not found.");
                        return null;
                    }
                }

            }
            catch (Exception e)
            {
                Debug.Log("Error at GetUser: " + e.Message);
                return hero;
            }
            finally
            {
                if (DBConnection.State == ConnectionState.Open)
                {
                    DBConnection.Close();
                }

            }
        }
        public void SaveHero(Hero hero)
        {
            try
            {
                OpenConnection();
                using (var command = new SqliteCommand())
                {
                    command.Connection = DBConnection;
                    BackendFunctions.UpdateHero(command, hero);
                    BackendFunctions.UpdateInventory(command, hero.Inventory);
                    BackendFunctions.UpdateStats(command, hero.Stats);
                }
                DBConnection.Close();
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                if (DBConnection.State == ConnectionState.Open)
                {
                    DBConnection.Close();
                }
            }
        }

        //item getters (specific or whole list)
        public static Weapon GetWeapon(int objectId)
        {
            Weapon weapon = null;
            try
            {
                string query = $"SELECT * FROM weapons WHERE Id = {objectId} LIMIT 1";
                OpenConnection();
                using (var command = new SqliteCommand(query, DBConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            string Name = reader.GetString(reader.GetOrdinal("Name"));
                            int Damage = reader.GetInt32(reader.GetOrdinal("Damage"));
                            int CriticalDamage = reader.GetInt32(reader.GetOrdinal("CriticalDamage"));
                            int Range = reader.GetInt32(reader.GetOrdinal("Range"));
                            int Value = reader.GetInt32(reader.GetOrdinal("Value"));
                            int Rarity = reader.GetInt32(reader.GetOrdinal("Rarity"));
                            string Url = reader.GetString(reader.GetOrdinal("Url"));
                            weapon = new Weapon(Name, Damage, CriticalDamage, Range, Value, Rarity, Url);
                            DBConnection.Close();
                        }
                        return weapon;
                    }

                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return weapon;
            }
            finally
            {
                if (DBConnection.State == ConnectionState.Open)
                {
                    DBConnection.Close();
                }
            }
        }
        public static Helmet GetHelmet(int objectId)
        {
            Helmet helmet = null;
            try
            {
                string query = $"SELECT * FROM helmets WHERE Id = {objectId} LIMIT 1";
                OpenConnection();
                using (var command = new SqliteCommand(query, DBConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            string Name = reader.GetString(reader.GetOrdinal("Name"));
                            int AdditionalArmour = reader.GetInt32(reader.GetOrdinal("AdditionalArmour"));
                            int Value = reader.GetInt32(reader.GetOrdinal("Value"));
                            int Rarity = reader.GetInt32(reader.GetOrdinal("Rarity"));
                            string Url = reader.GetString(reader.GetOrdinal("Url"));
                            helmet = new Helmet(Name, AdditionalArmour, Value, Rarity, Url);
                            DBConnection.Close();
                        }
                        return helmet;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return helmet;
            }
            finally
            {
                if (DBConnection.State == ConnectionState.Open)
                {
                    DBConnection.Close();
                }
            }
        }
        public static Boots GetBoots(int objectId)
        {
            Boots boots = null;
            try
            {
                string query = $"SELECT * FROM boots WHERE Id = {objectId} LIMIT 1";
                OpenConnection();
                using (var command = new SqliteCommand(query, DBConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            string Name = reader.GetString(reader.GetOrdinal("Name"));
                            int AdditionalArmour = reader.GetInt32(reader.GetOrdinal("AdditionalArmour"));
                            int Value = reader.GetInt32(reader.GetOrdinal("Value"));
                            int Rarity = reader.GetInt32(reader.GetOrdinal("Rarity"));
                            string Url = reader.GetString(reader.GetOrdinal("Url"));
                            boots = new Boots(Name, AdditionalArmour, Value, Rarity, Url);
                            DBConnection.Close();
                        }
                        return boots;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return boots;
            }
            finally
            {
                if (DBConnection.State == ConnectionState.Open)
                {
                    DBConnection.Close();
                }
            }
        }
        public static Bodyarmour GetBodyarmour(int objectId)
        {
            Bodyarmour bodyarmour = null;
            try
            {
                string query = $"SELECT * FROM bodyarmour WHERE Id = {objectId} LIMIT 1";
                OpenConnection();
                using (var command = new SqliteCommand(query, DBConnection))
                {
                    using var reader = command.ExecuteReader();
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            string Name = reader.GetString(reader.GetOrdinal("Name"));
                            int AdditionalArmour = reader.GetInt32(reader.GetOrdinal("AdditionalArmour"));
                            int Value = reader.GetInt32(reader.GetOrdinal("Value"));
                            int Rarity = reader.GetInt32(reader.GetOrdinal("Rarity"));
                            string Url = reader.GetString(reader.GetOrdinal("Url"));
                            bodyarmour = new Bodyarmour(Name, AdditionalArmour, Value, Rarity, Url);
                            DBConnection.Close();
                        }
                        return bodyarmour;
                    }
                        
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return bodyarmour;
            }
            finally
            {
                if (DBConnection.State == ConnectionState.Open)
                {
                    DBConnection.Close();
                }
            }
        }
        public static List<Helmet> GetAllHelmets()
        {
            List<Helmet> helmets = new List<Helmet>();
            try
            {
                string query = "SELECT * FROM helmets";
                OpenConnection();
                using (var command = new SqliteCommand(query, DBConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int Id = reader.GetInt32(reader.GetOrdinal("ID"));
                            string Name = reader.GetString(reader.GetOrdinal("Name"));
                            int AdditionalArmour = reader.GetInt32(reader.GetOrdinal("AdditionalArmour"));
                            int Value = reader.GetInt32(reader.GetOrdinal("Value"));
                            int Rarity = reader.GetInt32(reader.GetOrdinal("Rarity"));
                            string Url = reader.GetString(reader.GetOrdinal("Url"));

                            Helmet helmet = new(Name, AdditionalArmour, Value, Rarity, Url);
                            helmets.Add(helmet);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                if (DBConnection.State == ConnectionState.Open)
                {
                    DBConnection.Close();
                }
            }
            return helmets;
        }
        public static List<Boots> GetAllBoots()
        {
            List<Boots> bootsList = new List<Boots>();
            try
            {
                string query = "SELECT * FROM boots";
                OpenConnection();
                using (var command = new SqliteCommand(query, DBConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("ID"));
                            string name = reader.GetString(reader.GetOrdinal("Name"));
                            int value = reader.GetInt32(reader.GetOrdinal("Value"));
                            int additionalArmour = reader.GetInt32(reader.GetOrdinal("AdditionalArmour"));
                            int rarity = reader.GetInt32(reader.GetOrdinal("Rarity"));
                            string url = reader.GetString(reader.GetOrdinal("Url"));

                            Boots boots = new(name, value, additionalArmour, rarity, url);
                            bootsList.Add(boots);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                if (DBConnection.State == ConnectionState.Open)
                {
                    DBConnection.Close();
                }
            }
            return bootsList;
        }
        public static List<Weapon> GetAllWeapons()
        {
            List<Weapon> weapons = new List<Weapon>();
            try
            {
                string query = "SELECT * FROM weapons";
                OpenConnection();
                using (var command = new SqliteCommand(query, DBConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("ID"));
                            string name = reader.GetString(reader.GetOrdinal("Name"));
                            int damage = reader.GetInt32(reader.GetOrdinal("Damage"));
                            int criticalDamage = reader.GetInt32(reader.GetOrdinal("CriticalDamage"));
                            int range = reader.GetInt32(reader.GetOrdinal("Range"));
                            int value = reader.GetInt32(reader.GetOrdinal("Value"));
                            int rarity = reader.GetInt32(reader.GetOrdinal("Rarity"));
                            string url = reader.GetString(reader.GetOrdinal("Url"));

                            Weapon weapon = new(name, damage, criticalDamage, range, value, rarity, url);
                            weapons.Add(weapon);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                if (DBConnection.State == ConnectionState.Open)
                {
                    DBConnection.Close();
                }
            }
            return weapons;
        }
        public List<Enemy> GetAllEnemies()
        {
            List<Enemy> enemies = new();
            try
            {
                string query = "SELECT * FROM Enemies";
                OpenConnection();
                using (var command = new SqliteCommand(query, DBConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("ID"));
                            string name = reader.GetString(reader.GetOrdinal("Name"));
                            int worth = reader.GetInt32(reader.GetOrdinal("Worth"));
                            int maxHealth = reader.GetInt32(reader.GetOrdinal("MaxHealth"));
                            int damage = reader.GetInt32(reader.GetOrdinal("Damage"));
                            int healthRegeneration = reader.GetInt32(reader.GetOrdinal("HealthRegeneration"));
                            int hitRate = reader.GetInt32(reader.GetOrdinal("HitRate"));
                            int evadeRate = reader.GetInt32(reader.GetOrdinal("EvadeRate"));
                            int armour = reader.GetInt32(reader.GetOrdinal("Armour"));
                            int movementSpeed = reader.GetInt32(reader.GetOrdinal("MovementSpeed"));
                            int criticalChance = reader.GetInt32(reader.GetOrdinal("CriticalChance"));
                            int armourPenetration = reader.GetInt32(reader.GetOrdinal("ArmourPenetration"));

                            // Create a new Enemy object with the retrieved data
                            Enemy enemy = new(name, worth, maxHealth, damage, healthRegeneration, hitRate, evadeRate,
                                             armour, movementSpeed, criticalChance, armourPenetration);
                            enemies.Add(enemy);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                if (DBConnection.State == ConnectionState.Open)
                {
                    DBConnection.Close();
                }
            }
            return enemies;
        }
        public static List<Bodyarmour> GetAllBodyArmours()
        {
            List<Bodyarmour> bodyArmours = new List<Bodyarmour>();
            try
            {
                string query = "SELECT * FROM bodyarmour";
                OpenConnection();
                using (var command = new SqliteCommand(query, DBConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("ID"));
                            string name = reader.GetString(reader.GetOrdinal("Name"));
                            int additionalArmour = reader.GetInt32(reader.GetOrdinal("AdditionalArmour"));
                            int value = reader.GetInt32(reader.GetOrdinal("Value"));
                            int rarity = reader.GetInt32(reader.GetOrdinal("Rarity"));
                            string url = reader.GetString(reader.GetOrdinal("Url"));

                            Bodyarmour bodyArmour = new Bodyarmour(name, additionalArmour, value, rarity, url);
                            bodyArmours.Add(bodyArmour);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                if (DBConnection.State == ConnectionState.Open)
                {
                    DBConnection.Close();
                }
            }
            return bodyArmours;
        }

        //create a inventory object from Id
        public Inventory GetInventory(int objectId)
        {
            Inventory inventory = null;
            try
            {
                string query = $"SELECT * FROM inventory WHERE InventoryID = {objectId}";
                OpenConnection();
                using (var command = new SqliteCommand(query, DBConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int Id = reader.GetInt32(reader.GetOrdinal("InventoryID"));
                            string WeaponIdString = reader.GetString(reader.GetOrdinal("WeaponIDs"));
                            string BootsIdString = reader.GetString(reader.GetOrdinal("BootIDs"));
                            string HelmetIdString = reader.GetString(reader.GetOrdinal("HelmetIDs"));
                            string BodyarmourIdString = reader.GetString(reader.GetOrdinal("ArmourIDs"));

                            int CurrentWeaponId = reader.GetInt32(reader.GetOrdinal("CurrentWeapon"));
                            int CurrentHelmetId = reader.GetInt32(reader.GetOrdinal("CurrentHelmet"));
                            int CurrentBootsId = reader.GetInt32(reader.GetOrdinal("CurrentBoot"));
                            int CurrentBodyarmourId = reader.GetInt32(reader.GetOrdinal("CurrentArmour"));
                            int coins = reader.GetInt32(reader.GetOrdinal("Coins"));

                            List<Weapon> weapons = BackendFunctions.MakeWeaponList(WeaponIdString);
                            List<Boots> boots = BackendFunctions.MakeBootsList(BootsIdString);
                            List<Helmet> helmets = BackendFunctions.MakeHelmetList(HelmetIdString);
                            List<Bodyarmour> bodyarmours = BackendFunctions.MakeBodyarmourList(BodyarmourIdString);

                            Weapon currentWeapon = GetWeapon(CurrentWeaponId);
                            Helmet currentHelmet = GetHelmet(CurrentHelmetId);
                            Boots currentBoot = GetBoots(CurrentBootsId);
                            Bodyarmour currentBodyarmour = GetBodyarmour(CurrentBodyarmourId);
                            inventory = new Inventory(objectId, weapons, currentWeapon, bodyarmours, currentBodyarmour,
                                                      helmets,currentHelmet, boots, currentBoot, coins);
                        }
                    }
                    DBConnection.Close();
                    return inventory;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return inventory;
            }
            finally
            {
                if (DBConnection.State == ConnectionState.Open)
                {
                    DBConnection.Close();
                }
            }
        }

        //create a stats object from Id
        public Stats GetStats(int id)
        {
            Stats stats = null;
            try
            {
                string query = $"SELECT * FROM stats WHERE StatsID = {id} LIMIT 1";
                OpenConnection();
                using (var command = new SqliteCommand(query, DBConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int statId = reader.GetInt32(reader.GetOrdinal("StatsID"));
                            int Dmg = reader.GetInt32(reader.GetOrdinal("Damage"));
                            int ArmourPenetration = reader.GetInt32(reader.GetOrdinal("ArmourPenetration"));
                            int CriticalChance = reader.GetInt32(reader.GetOrdinal("CriticalChance"));
                            int HitRate = reader.GetInt32(reader.GetOrdinal("HitRate"));
                            int MaxHealth = reader.GetInt32(reader.GetOrdinal("MaxHealth"));
                            int HealthRegeneration = reader.GetInt32(reader.GetOrdinal("HealthRegeneration"));
                            int Armour = reader.GetInt32(reader.GetOrdinal("Armour"));
                            int EvadeRate = reader.GetInt32(reader.GetOrdinal("EvadeRate"));
                            int MovementSpeed = reader.GetInt32(reader.GetOrdinal("MovementSpeed"));

                            stats = new Stats(statId, Dmg, ArmourPenetration, CriticalChance, HitRate, MaxHealth, HealthRegeneration, Armour, EvadeRate, MovementSpeed);
                        }
                    }
                }
                DBConnection.Close();
                return stats;

            }
            catch (Exception e)
            {
                Debug.Log("Error: " + e.Message);
                return null;
            }
            finally
            {
                if (DBConnection.State == ConnectionState.Open)
                {
                    DBConnection.Close();
                }
            }
        }

        //retrun a list of all heros
        public List<Hero> AllHeros()
        {
            List<Hero> heros = new List<Hero>();
            try
            {
                string query = "SELECT * FROM hero";
                OpenConnection();
                using (var command = new SqliteCommand(query, DBConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        foreach (Hero item in reader)
                        {
                            heros.Add(item);
                        }
                    }
                }
                DBConnection.Close();
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                if (DBConnection.State == ConnectionState.Open)
                {
                    DBConnection.Close();
                }
            }
            return heros;
        }

        //deliting a hero
        public void DeleteHero(string name)
        {
            try
            {
                OpenConnection();
                using (var command = new SqliteCommand())
                {
                    command.Connection = DBConnection;
                    command.CommandText = "DELETE FROM hero WHERE name = @Name";
                    command.Parameters.AddWithValue("@Name", name);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Debug.Log($"Hero with name '{name}' deleted successfully.");
                    }
                    else
                    {
                        Debug.Log($"Hero with name '{name}' not found.");
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Error at DeleteHero: " + e.Message);
            }
            finally
            {
                if (DBConnection.State == ConnectionState.Open)
                {
                    DBConnection.Close();
                }
            }
        }
    }
}
