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
    public class BackendFunctions
    {
        //if a table is without default info it fill it up
        public static void FillTable(SqliteCommand command, string tableName, List<Dictionary<string, object>> data)
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
        //updating a hero object into the DB
        public static void UpdateHero(SqliteCommand command, Hero hero)
        {
            command.Parameters.Clear();
            command.CommandText = "UPDATE hero SET XP = @XP, Level = @Level  WHERE HeroID = @Id";
            command.Parameters.AddWithValue("@XP", hero.Xp);
            command.Parameters.AddWithValue("@Level", hero.Level);
            command.Parameters.AddWithValue("@Id", hero.Id);
            command.ExecuteNonQuery();
        }
        //updating an inventory object into the DB
        public static void UpdateInventory(SqliteCommand command, Inventory inventory)
        {
            command.Parameters.Clear();
            command.CommandText = "UPDATE inventory SET WeaponIDs = @WeaponIDs, CurrentWeapon = @CurrentWeapon, " +
                                  "HelmetIDs = @HelmetIDs, CurrentHelmet = @CurrentHelmet, " +
                                  "ArmourIDs = @ArmourIDs, CurrentArmour = @CurrentArmour, " +
                                  "BootIDs = @BootIDs, CurrentBoot = @CurrentBoot, Coins = @Coins WHERE InventoryID = @Id";

            string weaponIdsString = inventory.ListWeapons();
            string helmetIdsString = inventory.ListBodyArmours();
            string armourIdsString = inventory.ListBoots();
            string bootIdsString = inventory.ListHelmets();

            command.Parameters.AddWithValue("@WeaponIDs", weaponIdsString);
            int currentWeaponId = inventory.CurrentWeapon.Id;
            command.Parameters.AddWithValue("@CurrentWeapon", currentWeaponId);

            command.Parameters.AddWithValue("@HelmetIDs", helmetIdsString);
            int currentHelmetId = inventory.CurrentHelmet.Id;
            command.Parameters.AddWithValue("@CurrentHelmet", currentHelmetId);

            command.Parameters.AddWithValue("@ArmourIDs", armourIdsString);
            int currentArmourId = inventory.CurrentBodyarmour.Id;
            command.Parameters.AddWithValue("@CurrentArmour", currentArmourId);

            command.Parameters.AddWithValue("@BootIDs", bootIdsString);
            int currentBootId = inventory.CurrentBoot.Id;
            command.Parameters.AddWithValue("@CurrentBoot", currentBootId);
            command.Parameters.AddWithValue("@Coins", inventory.Coins);
            command.Parameters.AddWithValue("@Id", inventory.Id);


            command.ExecuteNonQuery();
        }
        //updating a stats object into the DB
        public static void UpdateStats(SqliteCommand command, Stats stats)
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
            command.Parameters.AddWithValue("@StatsID", stats.Id);
            command.ExecuteNonQuery();
        }
        //convert from string ids to a list of ids
        public static List<int> ConvertIdsStringToList(string idsString)
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
        //convert from string ids to a list of weapons
        public static List<Weapon> MakeWeaponList(string weaponIdString)
        {
            List<int> Ids = ConvertIdsStringToList(weaponIdString);
            List<Weapon> weapons = new();
            for (int i = 0; i < Ids.Count; i++)
            {
                Weapon newWeapon = SQLdb.GetWeapon(Ids[i]);
                weapons.Add(newWeapon);

            }
            return weapons;
        }
        //convert from string ids to a list of helmets
        public static List<Helmet> MakeHelmetList(string helmetIdString)
        {
            List<int> Ids = ConvertIdsStringToList(helmetIdString);
            List<Helmet> helmets = new();
            for (int i = 0; i < Ids.Count; i++)
            {
                Helmet newHelmet = SQLdb.GetHelmet(Ids[i]);
                helmets.Add(newHelmet);
            }
            return helmets;
        }
        //convert from string ids to a list of boots
        public static List<Boots> MakeBootsList(string bootsIdString)
        {
            List<int> Ids = ConvertIdsStringToList(bootsIdString);
            List<Boots> boots = new();
            for (int i = 0; i < Ids.Count; i++)
            {
                Boots newBoots = SQLdb.GetBoots(Ids[i]);
                boots.Add(newBoots);
            }
            return boots;
        }
        //convert from string ids to a list of bodyarmours
        public static List<Bodyarmour> MakeBodyarmourList(string bodyarmoursIdString)
        {
            List<int> Ids = ConvertIdsStringToList(bodyarmoursIdString);
            List<Bodyarmour> bodyarmours = new();
            for (int i = 0; i < Ids.Count; i++)
            {
                Bodyarmour newBodyarmour = SQLdb.GetBodyarmour(Ids[i]);
                bodyarmours.Add(newBodyarmour);
            }
            return bodyarmours;
        }
        //get an allitems object, that holds all the store objects
        public static AllItems GetAllItems()
        {
            List<Weapon> weapons = SQLdb.GetAllWeapons();
            List<Helmet> helmets = SQLdb.GetAllHelmets();
            List<Bodyarmour> bodyArmours = SQLdb.GetAllBodyArmours();
            List<Boots> boots = SQLdb.GetAllBoots();
            AllItems allitems = new(weapons, helmets, bodyArmours, boots);
            return allitems;
        }
    }
}
