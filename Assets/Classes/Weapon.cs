using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{using UnityEngine;
    public class Weapon
    {
        private int totalIds = 0;
        public int Id { get; set; }
        public int Damage { get; set; }
        public float Range { get; set; }
        public int CriticalDamage { get; set; }
        public int Hands { get; set; }
        public int Value { get; set; }
        public string Name { get; set; }

        // Constructor with parameters
        public Weapon(int damage, float range, int criticalDamage, int hands, int value, string name)
        {
            totalIds++;
            Id = totalIds;
            this.Damage = damage;
            this.Range = range;
            this.CriticalDamage = criticalDamage;
            this.Hands = hands;
            this.Name = name;
            this.Value = value;
        }
        public override string ToString()
        {
            return $"Weapon Details:\n" +
                   $"Name: {this.Name}\n" +
                   $"Damage: {this.Damage}\n" +
                   $"Range: {this.Range}\n" +
                   $"Critical Damage: {this.CriticalDamage}\n" +
                   $"Hands Required: {this.Hands}\n" +
                   $"Value: {this.Value}";
        }
    }
}