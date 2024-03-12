using System;
using UnityEngine;

namespace Assets
{
    public class Stats 
    {
        // Properties of the Stats class
        public int Id { get; set; }
        public int Dmg { get; set; }
        public int ArmourPenetration { get; set; }
        public int CriticalChance { get; set; }
        public int HitRate { get; set; }
        public int MaxHealth { get; set; }
        public int HealthRegeneration { get; set; }
        public int Armour { get; set; }
        public int EvadeRate { get; set; }
        public int MovementSpeed { get; set; }

        public Stats (int id, int dmg, int armourPenetration, int criticalChance,
                     int hitRate, int maxHealth, int healthRegeneration,
                     int armour, int evadeRate, int movementSpeed)
        {
            Id = id;
            Dmg = dmg;
            ArmourPenetration = armourPenetration;
            CriticalChance = criticalChance;
            HitRate = hitRate;
            MaxHealth = maxHealth;
            HealthRegeneration = healthRegeneration;
            Armour = armour;
            EvadeRate = evadeRate;
            MovementSpeed = movementSpeed;
        }

        public void Print()
        {
            Debug.Log($"Stats - ID: {Id}, Damage: {Dmg}, Armour Penetration: {ArmourPenetration}, " +
                      $"Critical Chance: {CriticalChance}, Hit Rate: {HitRate}, Max Health: {MaxHealth}, " +
                      $"Health Regeneration: {HealthRegeneration}, Armour: {Armour}, " +
                      $"Evade Rate: {EvadeRate}, Movement Speed: {MovementSpeed}");
        }
    }
}

