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
        public Weapon CurrentWeapon { get; set; }
        public List<Bodyarmour> Bodyarmours { get; set; }
        public Bodyarmour CurrentBodyarmour { get; set; }
        public List<Helmet> Helmets { get; set; }
        public Helmet CurrentHelmet { get; set; }
        public List<Boots> Boots { get; set; }
        public Boots CurrentBoot { get; set; }
        public int Coins { get; set; }

<<<<<<< HEAD
        public Inventory(int id, List<Weapon> weapons, Weapon currentWeapon, List<Bodyarmour> bodyarmours, Bodyarmour currentBodyarmour,
                List<Helmet> helmets, Helmet currentHelmet, List<Boots> boots, Boots currentBoot, int coins)
=======
        public Inventory(List<Weapon> weapons, List<Bodyarmour> bodyarmours, List<Helmet> helmets, List<Boots> boots, Weapon selectedWeapon, Bodyarmour selectedBodyarmour, Helmet selectedHelmet, Boots selectedBoots, int coins, int id)
>>>>>>> 347f3af07e1e7a4eff961f3f0d40e176df7a01f7
        {
            Id = id;
            Weapons = weapons;
            CurrentWeapon = currentWeapon;
            Bodyarmours = bodyarmours;
            CurrentBodyarmour = currentBodyarmour;
            Helmets = helmets;
            CurrentHelmet = currentHelmet;
            Boots = boots;
            CurrentBoot = currentBoot;
            Coins = coins;
<<<<<<< HEAD

        }
        public int num = 1;
        public void Print()
=======
        }
        public override string ToString()
>>>>>>> 347f3af07e1e7a4eff961f3f0d40e176df7a01f7
        {
            Debug.Log(num);
            Debug.Log(Weapons[0].Id);
            Debug.Log(CurrentBodyarmour);
            num++;
        }
        public string ListWeapons()
        {
            List<Weapon> list = Weapons;
            string str = "";
            foreach (Weapon weapon in list)
            {
                string str2 = weapon.GetID();
                Debug.Log(str2);
                str += str2;
                str += "+";
            }
            str.Remove(-1, 1);
            return str;
        }
        public string ListBoots()
        {
            List<Boots> list = Boots;

            string str = "";
            foreach (Boots boot in list)
            {
                str += boot.Id;
                str += "+";
            }
            str.Remove(-1, 1);
            return str;
        }
        public string ListBodyArmours()
        {
            List<Bodyarmour> list = Bodyarmours;
            string str = "";
            foreach (Bodyarmour bodyarmour in list)
            {
                str += bodyarmour.Id;
                str += "+";
            }
            str.Remove(-1, 1);
            return str;
        }
        public string ListHelmets()
        {
            List<Helmet> list = Helmets;
            string str = "";
            foreach (Helmet helmet in list)
            {
                str += helmet.Id;
                str += "+";
            }
            str.Remove(-1, 1);
            return str;
        }

        public int SumAdditionalArmour(Hero hero)
        {
            return (CurrentBodyarmour.AdditionalArmour + CurrentHelmet.AdditionalArmour + CurrentBoot.AdditionalArmour);
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
