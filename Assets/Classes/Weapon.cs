using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{using UnityEngine;
    public class Weapon
    {
<<<<<<< HEAD
=======
        private int totalIds = 0;
>>>>>>> 50cda48adafea976dd87f7ecdb35b4f881aba08e
        public int Id { get; set; }
        public int Damage { get; set; }
        public int Range { get; set; }
        public int CriticalDamage { get; set; }
        public int Value { get; set; }
        public string Name { get; set; }
        public int Rarity { get; set; }
        public string Url { get; set; }


        // Constructor with parameters
<<<<<<< HEAD
        public Weapon(int id, string name, int damage, int criticalDamage, int range, int value,  int rarity, string url)
        {
            Id = id;
            Damage = damage;
            Range = range;
            CriticalDamage = criticalDamage;
            Name = name;
            Value = value;
            Rarity = rarity;
            Range = range;
            Url = url;
=======
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
>>>>>>> 50cda48adafea976dd87f7ecdb35b4f881aba08e
        }
        public override string ToString()
        {
            return $"Weapon Details:\n" +
                   $"Name: {this.Name}\n" +
                   $"Damage: {this.Damage}\n" +
                   $"Range: {this.Range}\n" +
                   $"Critical Damage: {this.CriticalDamage}\n" +
<<<<<<< HEAD
=======
                   $"Hands Required: {this.Hands}\n" +
>>>>>>> 50cda48adafea976dd87f7ecdb35b4f881aba08e
                   $"Value: {this.Value}";
        }
    }
}