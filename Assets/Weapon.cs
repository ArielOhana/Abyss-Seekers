using System;
using System.Collections.Generic;
namespace Assets
{
    public class Weapon
    {
        // Properties of the Weapon class
        public int Damage { get; set; }
        public float Range { get; set; }
        public int CriticalDamage { get; set; }
        public int Hands { get; set; }
        public int Value { get; set; }

        public string Name { get; set; }

        // Constructor with parameters
        public Weapon(int damage, float range, int criticalDamage, int hands, int value, string Name)
        {
            Damage = damage;
            Range = range;
            CriticalDamage = criticalDamage;
            Hands = hands;
            Value = value;
        }
        public override string ToString()
        {
            return $"Weapon Details:\n" +
                   $"Name: {Name}\n" +
                   $"Damage: {Damage}\n" +
                   $"Range: {Range}\n" +
                   $"Critical Damage: {CriticalDamage}\n" +
                   $"Hands Required: {Hands}\n" +
                   $"Value: {Value}";
        }
    }
}