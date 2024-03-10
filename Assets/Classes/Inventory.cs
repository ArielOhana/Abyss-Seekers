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
<<<<<<< HEAD
        public int Id { get; set; }
        public List<Weapon> Weapons { get; set; }
        public Weapon currentWeapon { get; set; }
        public List<Bodyarmour> Bodyarmours { get; set; }
        public Bodyarmour currentBodyarmour { get; set; }
        public List<Helmet> Helmets { get; set; }
=======
        private int totalIds = 0;
        public int Id { get; set; }
        public string Weapons { get; set; }
        public Weapon currentWeapon { get; set; }
        public string Bodyarmours { get; set; }
        public Bodyarmour currentBodyarmour { get; set; }
        public string Helmets { get; set; }
>>>>>>> 50cda48adafea976dd87f7ecdb35b4f881aba08e
        public Helmet currentHelmet { get; set; }
        public string Boots { get; set; }
        public Boots currentBoot { get; set; }
        public int Coins { get; set; }

<<<<<<< HEAD
        public Inventory(List<Weapon> weapons, List<Bodyarmour> bodyarmours, List<Helmet> helmets, List<Boots> boots, Weapon selectedWeapon, Bodyarmour selectedBodyarmour, Helmet selectedHelmet, Boots selectedBoots, int coins)
=======
        public Inventory(string weapons, string bodyarmours, string helmets, string boots, int selectedWeapon, int selectedBodyarmour, int selectedHelmet, int selectedBoots,int coins)
>>>>>>> 50cda48adafea976dd87f7ecdb35b4f881aba08e
        {
            totalIds++;
            Id = totalIds;
            Weapons = weapons;
            Bodyarmours = bodyarmours;
            Helmets = helmets;
            Boots = boots;
<<<<<<< HEAD
            currentWeapon = selectedWeapon;
            currentBodyarmour = selectedBodyarmour;
            currentHelmet = selectedHelmet;
            currentBoot = selectedBoots;
            Coins = coins;
        }
=======
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
>>>>>>> 50cda48adafea976dd87f7ecdb35b4f881aba08e

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

<<<<<<< HEAD
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
=======
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
>>>>>>> 50cda48adafea976dd87f7ecdb35b4f881aba08e
       
    }
}
