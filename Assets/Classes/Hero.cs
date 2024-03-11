using System;
using System.Collections.Generic;
using System.Data;
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
        public Weapon Weapon { get; set; }  
        public Inventory Inventory { get; set; }
        public Role Role { get; set; } 
        public Stats Stats { get; set; }

        public Hero(string name, int level, int xp, Weapon weapon, Inventory inventory, Role role, Stats stats)
        {
            totalIds++;
            Id = totalIds;
            Name = name;
            Level = level;
            Xp = xp;
            Weapon = weapon;
            Inventory = inventory;
            Role = role;
            Stats = stats;
        }
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
