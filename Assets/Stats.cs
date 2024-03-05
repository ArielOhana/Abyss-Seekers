using System;

namespace Assets
{
    public class Stats : Hero
    {
        // Properties of the Stats class
        public float Dmg { get; set; }
        public float ArmorPenetration { get; set; }
        public float CriticalChance { get; set; }
        public float HitRate { get; set; }
        public float Health { get; set; }
        public float MaxHealth { get; set; }
        public float HealthRegeneration { get; set; }
        public float Armor { get; set; }
        public float EvadeRate { get; set; }
        public float MovementSpeed { get; set; }

        // Constructor with parameters
        public Stats()
        {
            Dmg = Role.Damage+Weapon.Damage;
            ArmorPenetration = Role.ArmorPenetration;
            CriticalChance = Role.CriticalChance;
            HitRate = Role.HitRate;
             MaxHealth = Role.MaxHealth;
            Health = Role.MaxHealth;
            HealthRegeneration = Role.HealthRegeneration;
            Armor = Role.Armor + Clothes.SumAdditionalArmor();
            EvadeRate = Role.EvadeRate;
            MovementSpeed = Role.MovementSpeed;
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
                   $"Armor Penetration: {ArmorPenetration}\n" +
                   $"Critical Chance: {CriticalChance}\n" +
                   $"Hit Rate: {HitRate}\n" +
                   $"Health: {Health}\n" +
                   $"Max Health: {MaxHealth}\n" +
                   $"Health Regeneration: {HealthRegeneration}\n" +
                   $"Armor: {Armor}\n" +
                   $"Evade Rate: {EvadeRate}\n" +
                   $"Movement Speed: {MovementSpeed}";
        }
    }
}