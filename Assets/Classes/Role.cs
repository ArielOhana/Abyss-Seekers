using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets
{
    public class Role
    {
        public string Name { get; set; }
        public float MaxHealth { get; set; }
        public float Damage { get; set; }
        public float HealthRegeneration { get; set; }
        public float HitRate { get; set; }
        public float EvadeRate { get; set; }
        public float Armor { get; set; }
        public float MovementSpeed { get; set; }
        public float CriticalChance { get; set; }
        public float ArmorPenetration { get; set; }
        public string[] SpecialAbility { get; set; }
        public Role(string[] specialAbility,string name, float maxHealth, float damage, float healthRegeneration, float hitRate, float evadeRate, float movementSpeed, float armor, float criticalChance, float armorPenetration)
        {
            ArmorPenetration = armorPenetration;
            Name = name;
            MaxHealth = maxHealth;
            Damage = damage;
            HealthRegeneration = healthRegeneration;
            HitRate = hitRate;
            EvadeRate = evadeRate;
            MovementSpeed = movementSpeed;
            Armor = armor;
            CriticalChance = criticalChance;
            SpecialAbility = specialAbility;
        }
    }
}
