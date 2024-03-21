using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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

        public static SQLdb DBManager;

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
        public Dictionary<string, object> WonFight()
        {
            Dictionary<string, object> loot = new();
            int randomCoins = new System.Random().Next(1, 4) * 10;
            int randomXp = new System.Random().Next(50, 100) * 15;
            int nextXp = this.Xp + randomXp;
            Boolean raisedLevelAmount = false;
            if (nextXp > 1000) 
            {
                raisedLevelAmount = true;
                nextXp -= 1000;
            }
            this.Level ++;
            this.Xp = nextXp;
            Debug.Log("1"+this.Inventory.Coins);
            this.Inventory.Coins += randomCoins;
            loot.Add("Coins", randomCoins);
            loot.Add("TotalXp", randomXp);
            loot.Add("RaisedLevel", raisedLevelAmount);
            return loot;
        }
        public List<Enemy> SpawnEnemies()
        {
            List<Enemy> enemyList = DBManager.GetAllEnemies();
            System.Random rand = new System.Random();
            try
            {
                int hardnes = rand.Next(1, 5) * Level;
                while (hardnes > 0)
                {
                    int randomEnemyIndex = rand.Next(1, 6);
                    Enemy selectedEnemy = enemyList.Find(enemy => enemy.Worth == randomEnemyIndex);
                    // Check if the budget is enough to add this enemy
                    if (selectedEnemy != null && selectedEnemy.Worth <= hardnes)
                    {
                        enemyList.Add(selectedEnemy);
                        hardnes -= selectedEnemy.Worth;
                    }
                }
                return enemyList;
            }
            catch (Exception e)
            {
                Debug.Log("Error: " + e.Message);
                return null;
            }
        }
    }
}
