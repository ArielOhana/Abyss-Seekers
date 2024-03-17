using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public Inventory(int id, List<Weapon> weapons, Weapon currentWeapon, List<Bodyarmour> bodyarmours, Bodyarmour currentBodyarmour,
                List<Helmet> helmets, Helmet currentHelmet, List<Boots> boots, Boots currentBoot, int coins)
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
        }
        public void Print(string info)
        {
            switch (info)
            {
                case "amounts":
                    Debug.Log("Total weapons " + Weapons.Count()+
                               "bodyarmours: " + Bodyarmours.Count()+
                               "helmets " + Helmets.Count()+
                               "boots " + Boots.Count() +
                               "coins " + Coins);
                    break;
                case "currents":
                    Debug.Log("current weapon: " + CurrentWeapon.Name+
                               "bodyarmour: " + CurrentBodyarmour.Name +
                               "helmet: " + CurrentHelmet.Name +
                               "boot: " + CurrentBoot.Name +
                               "coins " + Coins);
                    break;
                case "all":
                    Debug.Log("current weapon: " + CurrentWeapon.Name +
                               "bodyarmours: " + CurrentBodyarmour.Name +
                               "helmets: " + CurrentHelmet.Name +
                               "boots: " + CurrentBoot.Name);
                    Debug.Log("Total weapons: " + Weapons.Count() +
                               "bodyarmours: " + Bodyarmours.Count() +
                               "helmets: " + Helmets.Count() +
                               "boots: " + Boots.Count() +
                               "coins " + Coins);
                    break;
                default:
                    break;
            }
        }

        public string ListWeapons()
        {
            List<Weapon> list = Weapons;
            string str = "";
            foreach (Weapon weapon in list)
            {
                string str2 = weapon.GetID();
                str += str2;
                str += "+";
            }
            str = str.Remove(str.Length - 1, 1);
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
            str = str.Remove(str.Length - 1, 1);
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
            str = str.Remove(str.Length - 1, 1);
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
            str = str.Remove(str.Length - 1, 1);
            return str;
        }
        public int SumAdditionalArmour()
        {
            return (CurrentBodyarmour.AdditionalArmour + CurrentHelmet.AdditionalArmour + CurrentBoot.AdditionalArmour);
        }
        public Boolean AddItem<T>(T item)
        {
            switch (item)
            {
                case Weapon weapon:
                    if (Paying(weapon.Value))
                        Weapons.Add(weapon);
                    else return false;
                    break;
                case Helmet helmet:
                    if (Paying(helmet.Value))
                        Helmets.Add(helmet);
                    else return false;
                    break;
                case Bodyarmour bodyarmour:
                    if (Paying(bodyarmour.Value))
                        Bodyarmours.Add(bodyarmour);
                    else return false;
                    break;
                case Boots boot:
                    if (Paying(boot.Value))
                        Boots.Add(boot);
                    else return false;
                    break;
            }
            return true;
        }

        private bool Paying(int value)
        {
            if (Coins >= value)
            {
                Coins -= value;
                return true;
            }
            else
            {
                Debug.Log("Not enough coins to purchase the item.");
                return false;
            }
        }
    }
}
