using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class Role
    {
        public string Name { get; set; }
        public int MaxHealth { get; set; }
        public int Damage { get; set; }
        public int HealthRegeneration { get; set; }
        public int HitRate { get; set; }
        public int EvadeRate { get; set; }
        public int Armour { get; set; }
        public int MovementSpeed { get; set; }
        public int CriticalChance { get; set; }
        public int ArmourPenetration { get; set; }
        public string[] SpecialAbility { get; set; }

        public Role(string[] specialAbility, string name, int maxHealth, int damage, int healthRegeneration, int hitRate,
                            int evadeRate, int movementSpeed, int armour, int criticalChance, int armourPenetration)
        {
            this.Name = name;
            this.MaxHealth = maxHealth;
            this.Damage = damage;
            this.HealthRegeneration = healthRegeneration;
            this.HitRate = hitRate;
            this.EvadeRate = evadeRate;
            this.MovementSpeed = movementSpeed;
            this.Armour = armour;
            this.CriticalChance = criticalChance;
            this.ArmourPenetration = armourPenetration;
            this.SpecialAbility = specialAbility;
        }
        public void Print()
        {
            Debug.Log($"Role Details:\n" +
                      $"Name: {Name}\n" +
                      $"Max Health: {MaxHealth}\n" +
                      $"Damage: {Damage}\n" +
                      $"Health Regeneration: {HealthRegeneration}\n" +
                      $"Hit Rate: {HitRate}\n" +
                      $"Evade Rate: {EvadeRate}\n" +
                      $"Armour: {Armour}\n" +
                      $"Movement Speed: {MovementSpeed}\n" +
                      $"Critical Chance: {CriticalChance}\n" +
                      $"Armour Penetration: {ArmourPenetration}\n" +
                      $"Special Ability: {string.Join(", ", SpecialAbility)}");
        }
    }
}
