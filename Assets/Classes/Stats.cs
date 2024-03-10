using System;
using UnityEngine;

namespace Assets
{
    public class Stats 
    {
        // Properties of the Stats class
        public int Id { get; private set; }
        public int Dmg { get; private set; }
        public int ArmourPenetration { get; private set; }
        public int CriticalChance { get; private set; }
        public int HitRate { get; private set; }
        public int MaxHealth { get; private set; }
        public int HealthRegeneration { get; private set; }
        public int Armour { get; private set; }
        public int EvadeRate { get; private set; }
        public int MovementSpeed { get; private set; }

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

        public Stats(Hero hero, Role role, Weapon weapon, Inventory inventory)
            : this(0,
                   role.Damage + weapon.Damage,
                   role.ArmourPenetration,
                   role.CriticalChance,
                   role.HitRate,
                   role.MaxHealth,
                   role.HealthRegeneration,
                   role.Armour + inventory.SumAdditionalArmour(hero),
                   role.EvadeRate,
                   role.MovementSpeed)
        {
        }

        public override string ToString()
        {
            return $"Stats:\n" +
                   $"Damage (Dmg): {Dmg}\n" +
                   $"Armour Penetration: {ArmourPenetration}\n" +
                   $"Critical Chance: {CriticalChance}\n" +
                   $"Hit Rate: {HitRate}\n" +
                   $"Max Health: {MaxHealth}\n" +
                   $"Health Regeneration: {HealthRegeneration}\n" +
                   $"Armour: {Armour}\n" +
                   $"Evade Rate: {EvadeRate}\n" +
                   $"Movement Speed: {MovementSpeed}";
        }
    }
}

