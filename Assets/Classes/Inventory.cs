using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Build;
using UnityEngine;

namespace Assets
{
    public class Inventory
    {
        private int totalIds = 0;
        public int Id { get; set; }
        public string Weapons { get; set; }
        public Weapon currentWeapon { get; set; }
        public string Bodyarmours { get; set; }
        public Bodyarmour currentBodyarmour { get; set; }
        public string Helmets { get; set; }
        public Helmet currentHelmet { get; set; }
        public string Boots { get; set; }
        public Boots currentBoot { get; set; }
        public int Coins { get; set; }

        public Inventory(string weapons, string bodyarmours, string helmets, string boots, int selectedWeapon, int selectedBodyarmour, int selectedHelmet, int selectedBoots,int coins)
        {
            totalIds++;
            Id = totalIds;
            Weapons = weapons;
            Bodyarmours = bodyarmours;
            Helmets = helmets;
            Boots = boots;
            currentWeapon = GetByIDWeapon(selectedWeapon);
            currentBodyarmour = GetByIDBodyarmour(selectedBodyarmour);
            currentHelmet = GetByIDHelmet(selectedHelmet);
            currentBoot = GetByIDBoot(selectedBoots);
            Coins = coins;
        }
        private Weapon GetByIDWeapon(int id)
        {

            return null;
        }
        private Bodyarmour GetByIDBodyarmour(int id)
        {

            return null;
        }
        private Helmet GetByIDHelmet(int id)
        {

            return null;
        }
        private Boots GetByIDBoot(int id)
        {

            return null;
        }

        public override string ToString()
        {
            return $"Inventory:\n" +
                   $"Weapons: {Weapons}\n" +
                   $"Current Weapon: {currentWeapon}\n" +
                   $"Bodyarmours: {Bodyarmours}\n" +
                   $"Current Bodyarmour: {currentBodyarmour}\n" +
                   $"Helmets: {Helmets}\n" +
                   $"Current Helmet: {currentHelmet}\n" +
                   $"Boots: {Boots}\n" +
                   $"Current Boot: {currentBoot}\n" +
                   $"Coins: {Coins}";
        }

        public float SumAdditionalArmour(Hero hero)
        {
            return (currentBodyarmour.AdditionalArmour + currentHelmet.AdditionalArmour + currentBoot.AdditionalArmour);
        }
        public void AddItem(string ObjectType, string itemIds, int coinsChange)
        {
            switch (ObjectType)
            {
                case "weapon":
                    this.Weapons += itemIds;
                    break;
                case "helmet":
                    this.Weapons += itemIds; 
                    break;
                case "boots":
                    this.Boots += itemIds;
                    break;
                case "bodyarmour":
                    this.Bodyarmours += itemIds;
                    break;
                default:
                    this.Coins += coinsChange;
                    break;
            }
        }
       
    }
}
