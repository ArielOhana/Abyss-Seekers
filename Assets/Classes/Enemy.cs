using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class Enemy 
    {
        public string Name { get; set; }
        public float MaxHealth { get; set; }
        public float Damage { get; set; }
        public float HealthRegeneration { get; set; }
        public float HitRate { get; set; }
        public float EvadeRate { get; set; }
        public float Armour { get; set; }
        public float MovementSpeed { get; set; }
        public float CriticalChance { get; set; }
        public float ArmourPenetration { get; set; }
        public string[] SpecialAbility { get; set; }

        public void Print()
        {
            Debug.Log($"Enemy Details:\n" +
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