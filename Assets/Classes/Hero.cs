using System;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets
{
    public class Hero
    {
        private static int totalIds = 0;

        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Xp { get; set; }
<<<<<<< HEAD
=======
        public Weapon Weapon { get; set; }  
        public Inventory Inventory { get; set; }
        public Role Role { get; set; } 
>>>>>>> 50cda48adafea976dd87f7ecdb35b4f881aba08e
        public Stats Stats { get; set; }
        public Inventory Inventory { get; set; }
        public string Role { get; set; }

<<<<<<< HEAD

        public Hero(string name, int level, int xp, Stats stats, Inventory inventory, string role)
=======
        public Hero(string name, int level, int xp, Weapon weapon, Inventory inventory, Role role, Stats stats)
>>>>>>> 50cda48adafea976dd87f7ecdb35b4f881aba08e
        {
            totalIds++;
            Id = totalIds;
            Name = name;
            Level = level;
            Xp = xp;
<<<<<<< HEAD
=======
            Weapon = weapon;
>>>>>>> 50cda48adafea976dd87f7ecdb35b4f881aba08e
            Inventory = inventory;
            Role = role;
            Stats = stats;
        }
<<<<<<< HEAD
        public int getId(Hero hero)
        {
            return this.Id;
        }
    }
}

//CREATE TABLE IF NOT EXISTS helmets(
//    ID INTEGER PRIMARY KEY,
//    Name TEXT,
//    Damage INTEGER,
//    AdditionalArmour INTEGER,
//    Rarity INTEGER,
//Url STRING
//);

//CREATE TABLE IF NOT EXISTS boots(
//    ID INTEGER PRIMARY KEY,
//    Name TEXT,
//    Value INTEGER,
//    AdditionalArmour INTEGER,
//    Rarity INTEGER,
//Url STRING
//);

//CREATE TABLE IF NOT EXISTS bodyarmour(
//    ID INTEGER PRIMARY KEY,
//    Name TEXT,
//    Value INTEGER,
//    AdditionalArmour INTEGER,
//    Rarity INTEGER,
//    Url STRING
//);
//CREATE TABLE IF NOT EXISTS roles(
//    ID INTEGER PRIMARY KEY,
//    Name TEXT,
//    Damage INTEGER,
//    Armour INTEGER,
//    MaxHealth INTEGER,
//    HealthRegeneration INTEGER,
//    MovementSpeed INTEGER,
//    EvadeRate INTEGER,
//    HitRate INTEGER,
//    CriticalChance INTEGER,
//    ArmourPenetration INTEGER,
//    SpecialAbility TEXT, WeaponID INTEGER
//);
//CREATE TABLE IF NOT EXISTS stats(
//    StatsID INTEGER PRIMARY KEY,
//    Damage INTEGER,
//    Armour INTEGER,
//    MaxHealth INTEGER,
//    HealthRegeneration INTEGER,
//    MovementSpeed INTEGER,
//    EvadeRate INTEGER,
//    HitRate INTEGER,
//    CriticalChance INTEGER,
//    ArmourPenetration INTEGER
//);
//CREATE TABLE IF NOT EXISTS hero(
//    HeroID INTEGER PRIMARY KEY,
//    Name TEXT,
//    Level INTEGER,
//    XP INTEGER,
//    StatsID INTEGER,
//    InventoryID INTEGER,
//    Role TEXT
//);

//CREATE TABLE IF NOT EXISTS inventory(
//    InventoryID INTEGER PRIMARY KEY,
//    WeaponIDs TEXT,
//    CurrentWeapon INTEGER,
//    HelmetIDs TEXT,
//    CurrentHelmet INTEGER,
//    ArmourIDs TEXT,
//    CurrentArmour INTEGER,
//    BootIDs TEXT,
//    CurrentBoot INTEGER,
//    Coins INTEGER
//);
//CREATE TABLE IF NOT EXISTS enemies(
//    ID INTEGER PRIMARY KEY,
//    Name TEXT,
//    MaxHealth INTEGER,
//    Damage INTEGER,
//    HealthRegeneration INTEGER,
//    HitRate INTEGER,
//    Armour INTEGER,
//    EvadeRate INTEGER,
//    MovementSpeed INTEGER,
//    CriticalChance INTEGER,
//    ArmourPenetration INTEGER,
//    SpecialAbility TEXT
//);
//CREATE TABLE IF NOT EXISTS weapons(
// ID INTEGER PRIMARY KEY,
//Name TEXT,
//Damage INTEGER,
//Hands INTEGER,
//CriticalDamage INTEGER,
//Range INTEGER,
//Value INTEGER,
//Rarity INTEGER,
//Url STRING
// );
=======
        public void Print()
        {
            Debug.Log($"Hero Details:\n" +
                       $"ID: {Id}\n" +
                       $"Name: {Name}\n" +
                       $"Level: {Level}\n" +
                       $"XP: {Xp}\n" +
                       $"Weapon: {Weapon?.Name}\n" +
                       $"Inventory: {Inventory?.ToString()}\n" +
                       $"Role: {Role?.Name}\n" +
                       $"Stats: {Stats?.ToString()}\n");
        }
    }
}
>>>>>>> 50cda48adafea976dd87f7ecdb35b4f881aba08e
