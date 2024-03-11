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
        public Stats Stats { get; set; }
        public Inventory Inventory { get; set; }
        public string Role { get; set; }


        public Hero(string name, int level, int xp, Stats stats, Inventory inventory, string role)
        {
            totalIds++;
            Id = totalIds;
            Name = name;
            Level = level;
            Xp = xp;
            Inventory = inventory;
            Role = role;
            Stats = stats;
        }
        public int getId(Hero hero)
        {
            return this.Id;
        }
        public Dictionary<string, System.Object> WonFight(int levelDifficulty)
        {
            Dictionary<string, System.Object> loot = new();
            int randomCoins = new System.Random().Next(1, 4) * levelDifficulty;
            int randomXp = new System.Random().Next(50, 100) * levelDifficulty;
            int nextXp = this.Xp + randomXp;
            int raisedLevelAmount = 0;
            while (nextXp > 1000) 
            {
                raisedLevelAmount++;
                nextXp -= 1000;
            }
            this.Level += raisedLevelAmount;
            this.Xp = nextXp;
            this.Inventory.Coins = 1200;
            Debug.Log(randomXp);
            loot.Add("Coins", randomCoins);
            loot.Add("TotalXp", randomXp);
            loot.Add("RaisedLevels", raisedLevelAmount);
            loot.Add("nextXp", nextXp);
            return loot;
        }
    }
}

//CREATE TABLE IF NOT EXISTS helmets(
//    ID INTEGER PRIMARY KEY,
//    Name TEXT,
//    Value INTEGER,
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
//CriticalDamage INTEGER,
//Range INTEGER,
//Value INTEGER,
//Rarity INTEGER,
//Url STRING
// );
