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
        public int Id { get; set; }
        public List<Weapon> Weapons { get; set; }
        public Weapon currentWeapon { get; set; }
        public List<Bodyarmour> Bodyarmours { get; set; }
        public Bodyarmour currentBodyarmour { get; set; }
        public List<Helmet> Helmets { get; set; }
        public Helmet currentHelmet { get; set; }
        public List<Boots> Boots { get; set; }
        public Boots currentBoot { get; set; }
        public int Coins { get; set; }

        public Inventory(List<Weapon> weapons, List<Bodyarmour> bodyarmours, List<Helmet> helmets, List<Boots> boots, Weapon selectedWeapon, Bodyarmour selectedBodyarmour, Helmet selectedHelmet, Boots selectedBoots, int coins, int id)
        {
            Id = id;
            Weapons = weapons;
            Bodyarmours = bodyarmours;
            Helmets = helmets;
            Boots = boots;
            currentWeapon = selectedWeapon;
            currentBodyarmour = selectedBodyarmour;
            currentHelmet = selectedHelmet;
            currentBoot = selectedBoots;
            Coins = coins;
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

        public int SumAdditionalArmour(Hero hero)
        {
            return (currentBodyarmour.AdditionalArmour + currentHelmet.AdditionalArmour + currentBoot.AdditionalArmour);
        }
        //public void AddItem(string ObjectType, string itemIds, int coinsChange)
        //{
        //    switch (ObjectType)
        //    {
        //        case "weapon":
                    
        //            break;
        //        case "helmet":
        //            this.Weapons += itemIds; 
        //            break;
        //        case "boots":
        //            this.Boots += itemIds;
        //            break;
        //        case "bodyarmour":
        //            this.Bodyarmours += itemIds;
        //            break;
        //        default:
        //            this.Coins += coinsChange;
        //            break;
        //    }
        //}
    }
}
