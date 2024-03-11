using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{using UnityEngine;
    public class Weapon
    {
        public int Id { get; set; }
        public int Damage { get; set; }
        public int Range { get; set; }
        public int CriticalDamage { get; set; }
        public int Value { get; set; }
        public string Name { get; set; }
        public int Rarity { get; set; }
        public string Url { get; set; }


        public Weapon(int id, string name, int damage, int criticalDamage, int range, int value,  int rarity, string url)
        {
            Damage = damage;
            Range = range;
            CriticalDamage = criticalDamage;
            Name = name;
            Value = value;
            Rarity = rarity;
            Range = range;
            Url = url;
        }
        public override string ToString()
        {
            return $"Weapon Details:\n" +
                   $"Name: {this.Name}\n" +
                   $"Damage: {this.Damage}\n" +
                   $"Range: {this.Range}\n" +
                   $"Critical Damage: {this.CriticalDamage}\n" +
                   $"Value: {this.Value}";
        }
    }
}