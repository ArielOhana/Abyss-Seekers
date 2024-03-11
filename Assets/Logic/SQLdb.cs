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
    public class SQLdb : MonoBehaviour
    {
        private string connectionString = "URI=file:Assets/Logic/DB/try4.db";
        private SqliteConnection DBConnection;
        string FilePath = "Assets/Logic/Default_JSON.json";
        public void Start()
        {
           
        }
        private void OpenConnection()
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
        private static void FillTable(SqliteCommand command, string tableName, List<Dictionary<string, object>> data)
        {
            foreach (var item in data)
            {
                string columns = string.Join(", ", item.Keys);
                string values = string.Join(", ", item.Keys.Select(key => "@" + key));
                string query = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";
                command.CommandText = query;
                command.Parameters.Clear();

                // Add parameters and their values
                foreach (var key in item.Keys)
                {
                    command.Parameters.AddWithValue("@" + key, item[key]);
                }
                command.ExecuteNonQuery();
            }
        }
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
                            command.CommandText = $"SELECT COUNT(*) FROM {table.Key}";
                            int rowCount = Convert.ToInt32(command.ExecuteScalar());
                            if (rowCount < table.Value.Count)
                            {
                                FillTable(command, table.Key, table.Value);
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
        private void UpdateHero(SqliteCommand command, Hero hero)
        {
            command.Parameters.Clear();
            command.CommandText = "UPDATE hero SET XP = @XP, Level = @Level  WHERE Id = @Id";
            command.Parameters.AddWithValue("@XP", hero.Xp);
            command.Parameters.AddWithValue("@Level", hero.Level);
            command.Parameters.AddWithValue("@Id", hero.Id);
            command.ExecuteNonQuery();
        }
        private void UpdateInventory(SqliteCommand command, Inventory inventory)
        {
            command.Parameters.Clear();
            command.CommandText = "UPDATE inventory SET WeaponIDs = @WeaponIDs, CurrentWeapon = @CurrentWeapon, " +
                                  "HelmetIDs = @HelmetIDs, CurrentHelmet = @CurrentHelmet, " +
                                  "ArmourIDs = @ArmourIDs, CurrentArmour = @CurrentArmour, " +
                                  "BootIDs = @BootIDs, CurrentBoot = @CurrentBoot, Coins = @Coins WHERE Id = @Id";

            string weaponIdsString = string.Join("+", inventory.Weapons.Select(w => w.Id));
            string helmetIdsString = string.Join("+", inventory.Helmets.Select(h => h.Id));
            string armourIdsString = string.Join("+", inventory.Bodyarmours.Select(b => b.Id));
            string bootIdsString = string.Join("+", inventory.Boots.Select(c => c.Id));

            command.Parameters.AddWithValue("@WeaponIDs", weaponIdsString);
            command.Parameters.AddWithValue("@CurrentWeapon", inventory.currentWeapon?.Id ?? 0);
            command.Parameters.AddWithValue("@HelmetIDs", helmetIdsString);
            command.Parameters.AddWithValue("@CurrentHelmet", inventory.currentHelmet?.Id ?? 0);
            command.Parameters.AddWithValue("@ArmourIDs", armourIdsString);
            command.Parameters.AddWithValue("@CurrentArmour", inventory.currentBodyarmour?.Id ?? 0);
            command.Parameters.AddWithValue("@BootIDs", bootIdsString);
            command.Parameters.AddWithValue("@CurrentBoot", inventory.currentBoot?.Id ?? 0);
            command.Parameters.AddWithValue("@Coins", inventory.Coins);
            command.Parameters.AddWithValue("@Id", inventory.Id);

            command.ExecuteNonQuery();
        }
        private static void UpdateStats(SqliteCommand command, Stats stats)
        {
            command.CommandText = @"UPDATE stats SET Damage = @Damage, Armour = @Armour, MaxHealth = @MaxHealth,HealthRegeneration = @HealthRegeneration, 
                                        MovementSpeed = @MovementSpeed, EvadeRate = @EvadeRate, HitRate = @HitRate, CriticalChance = @CriticalChance, 
                                        ArmourPenetration = @ArmourPenetration WHERE StatsID = @StatsID";

            command.Parameters.AddWithValue("@Damage", stats.Dmg);
            command.Parameters.AddWithValue("@Armour", stats.Armour);
            command.Parameters.AddWithValue("@MaxHealth", stats.MaxHealth);
            command.Parameters.AddWithValue("@HealthRegeneration", stats.HealthRegeneration);
            command.Parameters.AddWithValue("@MovementSpeed", stats.MovementSpeed);
            command.Parameters.AddWithValue("@EvadeRate", stats.EvadeRate);
            command.Parameters.AddWithValue("@HitRate", stats.HitRate);
            command.Parameters.AddWithValue("@CriticalChance", stats.CriticalChance);
            command.Parameters.AddWithValue("@ArmourPenetration", stats.ArmourPenetration);
            command.ExecuteNonQuery();
        }
        public Hero CreateHero(String name, String role)
        {
            Hero hero;
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

                    Stats newStats = GetStats(statsId);
                    Inventory newInventory = GetInventory(inventoryId);
                    hero = new Hero(name, 1, 1, newStats, newInventory, role);
                }
                Debug.Log("Hero created");
                DBConnection.Close();
                return hero;
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
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
        public Hero GetHero(string name)
        {
            Hero hero = null;
            try
            {
                OpenConnection();
                using (var command = new SqliteCommand())
                {
                    command.Connection = DBConnection;
                    command.CommandText = "SELECT * FROM hero WHERE name = @Name LIMIT 1";
                    command.Parameters.AddWithValue("@Name", name);
                    using SqliteDataReader data = command.ExecuteReader();
                    if (data.Read())
                    {
                        int heroId = Convert.ToInt32(data.GetValue(0));
                        string newName = (string)data.GetValue(1);
                        int newLevel = (int)data.GetValue(2);
                        int newXp = (int)data.GetValue(3);
                        int statsId = (int)data.GetValue(4);
                        Stats newStats = GetStats(statsId);
                        int inventoryId = (int)data.GetValue(5);
                        Inventory newInventory = GetInventory(inventoryId);
                        string role = (string)data.GetValue(6);
                        hero = new Hero(newName, newLevel, newXp, newStats, newInventory, role);
                        DBConnection.Close();
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
                    UpdateHero(command, hero);
                    UpdateInventory(command, hero.Inventory);
                    UpdateStats(command, hero.Stats);
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
        public Weapon GetWeapon(int objectId)
        {
            Weapon weapon = null;
            try
            {
                string query = $"SELECT * FROM weapon WHERE Id = {objectId} LIMIT 1";
                OpenConnection();
                using (var command = new SqliteCommand(query, DBConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {

                        int Id = reader.GetInt32(reader.GetOrdinal("ID"));
                        string Name = reader.GetString(reader.GetOrdinal("Name"));
                        int Damage = reader.GetInt32(reader.GetOrdinal("Damage"));
                        int CriticalDamage = reader.GetInt32(reader.GetOrdinal("CriticalDamage"));
                        int Range = reader.GetInt32(reader.GetOrdinal("Range"));
                        int Value = reader.GetInt32(reader.GetOrdinal("Value"));
                        int Rarity = reader.GetInt32(reader.GetOrdinal("Rarity"));
                        string Url = reader.GetString(reader.GetOrdinal("Url"));
                        weapon = new Weapon(Id, Name, Damage, CriticalDamage, Range, Value, Rarity, Url);
                        DBConnection.Close();
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
        private List<Weapon> MakeWeaponList(string weaponIdString)
        {
            List<int> Ids = ConvertIdsStringToList(weaponIdString);
            List<Weapon> weapons = new();
            for (int i = 0; i < Ids.Count; i++)
            {
                Weapon newWeapon = GetWeapon(Ids[i]);
                weapons.Add(newWeapon);
            }
            return weapons;
        }
        public Helmet GetHelmet(int objectId)
        {
            Helmet helmet = null;
            try
            {
                string query = $"SELECT * FROM helmet WHERE Id = {objectId} LIMIT 1";
                OpenConnection();
                using (var command = new SqliteCommand(query, DBConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {

                        int Id = reader.GetInt32(reader.GetOrdinal("ID"));
                        string Name = reader.GetString(reader.GetOrdinal("Name"));
                        int AdditionalArmour = reader.GetInt32(reader.GetOrdinal("AdditionalArmour"));
                        int Value = reader.GetInt32(reader.GetOrdinal("Value"));
                        int Rarity = reader.GetInt32(reader.GetOrdinal("Rarity"));
                        string Url = reader.GetString(reader.GetOrdinal("Url"));
                        helmet = new Helmet(Id, Name, AdditionalArmour, Value, Rarity, Url);
                        DBConnection.Close();
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
        private List<Helmet> MakeHelmetList(string helmetIdString)
        {
            List<int> Ids = ConvertIdsStringToList(helmetIdString);
            List<Helmet> helmets = new();
            for (int i = 0; i < Ids.Count; i++)
            {
                Helmet newHelmet = GetHelmet(Ids[i]);
                helmets.Add(newHelmet);
            }
            return helmets;
        }
        public Boots GetBoots(int objectId)
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

                        int Id = reader.GetInt32(reader.GetOrdinal("ID"));
                        string Name = reader.GetString(reader.GetOrdinal("Name"));
                        int AdditionalArmour = reader.GetInt32(reader.GetOrdinal("AdditionalArmour"));
                        int Value = reader.GetInt32(reader.GetOrdinal("Value"));
                        int Rarity = reader.GetInt32(reader.GetOrdinal("Rarity"));
                        string Url = reader.GetString(reader.GetOrdinal("Url"));
                        boots = new Boots(Id, Name, AdditionalArmour, Value, Rarity, Url);
                        DBConnection.Close();
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
        private List<Boots> MakeBootsList(string bootsIdString)
        {
            List<int> Ids = ConvertIdsStringToList(bootsIdString);
            List<Boots> boots = new();
            for (int i = 0; i < Ids.Count; i++)
            {
                Boots newBoots = GetBoots(Ids[i]);
                boots.Add(newBoots);
            }
            return boots;
        }
        public Bodyarmour GetBodyarmour(int objectId)
        {
            Bodyarmour bodyarmour = null;
            try
            {
                string query = $"SELECT * FROM bodyarmour WHERE Id = {objectId} LIMIT 1";
                OpenConnection();
                using (var command = new SqliteCommand(query, DBConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {

                        int Id = reader.GetInt32(reader.GetOrdinal("ID"));
                        string Name = reader.GetString(reader.GetOrdinal("Name"));
                        int AdditionalArmour = reader.GetInt32(reader.GetOrdinal("AdditionalArmour"));
                        int Value = reader.GetInt32(reader.GetOrdinal("Value"));
                        int Rarity = reader.GetInt32(reader.GetOrdinal("Rarity"));
                        string Url = reader.GetString(reader.GetOrdinal("Url"));
                        bodyarmour = new Bodyarmour(Id, Name, AdditionalArmour, Value, Rarity, Url);
                        DBConnection.Close();
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
        private List<Bodyarmour> MakeBodyarmourList(string bodyarmoursIdString)
        {
            List<int> Ids = ConvertIdsStringToList(bodyarmoursIdString);
            List<Bodyarmour> bodyarmours = new();
            for (int i = 0; i < Ids.Count; i++)
            {
                Bodyarmour newBodyarmour = GetBodyarmour(Ids[i]);
                bodyarmours.Add(newBodyarmour);
            }
            return bodyarmours;
        }
        public Inventory GetInventory(int objectId)
        {
            Inventory inventory = null;
            try
            {
                string query = $"SELECT * FROM inventory WHERE InventoryID = {objectId} LIMIT 1";
                OpenConnection();
                using (var command = new SqliteCommand(query, DBConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int Id = reader.GetInt32(reader.GetOrdinal("InventoryID"));
                            string WeaponIdString = reader.GetString(reader.GetOrdinal("WeaponIDs"));
                            string BootsIdString = reader.GetString(reader.GetOrdinal("WeaponIDs"));
                            string HelmetIdString = reader.GetString(reader.GetOrdinal("WeaponIDs"));
                            string BodyarmourIdString = reader.GetString(reader.GetOrdinal("WeaponIDs"));
                            int CurrentWeaponId = reader.GetInt32(reader.GetOrdinal("CurrentWeapon"));
                            int CurrentHelmetId = reader.GetInt32(reader.GetOrdinal("CurrentHelmet"));
                            int CurrentBootsId = reader.GetInt32(reader.GetOrdinal("CurrentBoot"));
                            int CurrentBodyarmourId = reader.GetInt32(reader.GetOrdinal("CurrentArmour"));
                            int coins = reader.GetInt32(reader.GetOrdinal("Coins"));

                            List<Weapon> weapons = MakeWeaponList(WeaponIdString);
                            List<Boots> boots = MakeBootsList(BootsIdString);
                            List<Helmet> helmets = MakeHelmetList(HelmetIdString);
                            List<Bodyarmour> bodyarmours = MakeBodyarmourList(BodyarmourIdString);

                            Weapon currentWeapon = GetWeapon(CurrentWeaponId);
                            Helmet currentHelmet = GetHelmet(CurrentHelmetId);
                            Boots currentBoot = GetBoots(CurrentBootsId);
                            Bodyarmour currentBodyarmour = GetBodyarmour(CurrentBodyarmourId);

                            inventory = new Inventory(weapons, bodyarmours, helmets, boots, currentWeapon,
                                                    currentBodyarmour, currentHelmet, currentBoot, coins, objectId);
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
        public List<int> ConvertIdsStringToList(string idsString)
        {
            List<string> listString = idsString.Split("+").ToList();
            List<int> listInt = new List<int>();
            for (int i = 0; i < listString.Count(); i++)
            {
                int parsedInt = int.Parse(listString[i]);
                listInt.Add(parsedInt);

            }
            return listInt;
        }
        public Stats GetStats(int id)
        {
            Stats stats;
            try
            {
                string query = $"SELECT * FROM stats WHERE StatsID = {id} LIMIT 1";
                OpenConnection();
                using (var command = new SqliteCommand(query, DBConnection))
                {
                    using (var reader = command.ExecuteReader())
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
        public AllItems GetAllItems()
        {
            List<Weapon> weapons = GetAllWeapons();
            List<Helmet> helmets = GetAllHelmets();
            List<Bodyarmour> bodyArmours = GetAllBodyArmours();
            List<Boots> boots = GetAllBoots();
            AllItems allitems = new AllItems(weapons, helmets,bodyArmours, boots);// helmets, 
            return allitems;
        }
        public List<Helmet> GetAllHelmets()
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

                            Helmet helmet = new Helmet(Id, Name, AdditionalArmour, Value, Rarity, Url);
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
        public List<Boots> GetAllBoots()
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

                            Boots boots = new Boots(id, name, value, additionalArmour, rarity, url);
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
        public List<Weapon> GetAllWeapons()
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

                            Weapon weapon = new Weapon(id, name, damage, criticalDamage, range, value, rarity, url);
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
        public List<Bodyarmour> GetAllBodyArmours()
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
                            Bodyarmour bodyArmour = new Bodyarmour(id, name, additionalArmour, value, rarity, url);
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
    }
}
