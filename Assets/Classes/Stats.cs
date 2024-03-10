using System;
using UnityEngine;

namespace Assets
{
    public class Stats
    {
        // Properties of the Stats class
        private int totalIds = 0;
        public int Id { get; set; }
        public float Dmg { get; private set; }
        public float ArmourPenetration { get; private set; }
        public float CriticalChance { get; private set; }
        public float HitRate { get; private set; }
        public float Health { get; private set; }
        public float MaxHealth { get; private set; }
        public float HealthRegeneration { get; private set; }
        public float Armour { get; private set; }
        public float EvadeRate { get; private set; }
        public float MovementSpeed { get; private set; }

        public Stats(Hero hero, Role role, Weapon weapon, Inventory inventory)
        {
            totalIds++;
            Id = totalIds;
            Dmg = role.Damage + weapon.Damage;
            ArmourPenetration = role.ArmourPenetration;
            CriticalChance = role.CriticalChance;
            HitRate = role.HitRate;
            MaxHealth = role.MaxHealth;
            Health = role.MaxHealth;
            HealthRegeneration = role.HealthRegeneration;
            Armour = role.Armour + inventory.SumAdditionalArmour(hero);
            EvadeRate = role.EvadeRate;
            MovementSpeed = role.MovementSpeed;
        }
        public void Heal()
        {
            if (Health + HealthRegeneration <= MaxHealth)
                Health += HealthRegeneration;
        }
        public override string ToString()
        {
            return $"Stats:\n" +
                   $"Damage (Dmg): {Dmg}\n" +
                   $"Armour Penetration: {ArmourPenetration}\n" +
                   $"Critical Chance: {CriticalChance}\n" +
                   $"Hit Rate: {HitRate}\n" +
                   $"Health: {Health}\n" +
                   $"Max Health: {MaxHealth}\n" +
                   $"Health Regeneration: {HealthRegeneration}\n" +
                   $"Armour: {Armour}\n" +
                   $"Evade Rate: {EvadeRate}\n" +
                   $"Movement Speed: {MovementSpeed}";
        }
    }
}


