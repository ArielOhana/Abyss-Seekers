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
        public List<Weapon> Weapons { get; set; }
        public Weapon currentWeapon { get; set; }
        public List<Bodyarmor> Bodyarmors { get; set; }
        public Bodyarmor currentBodyarmor { get; set; }
        public List<Helmet> Helmets { get; set; }
        public Helmet currentHelmet { get; set; }
        public List<Boots> Boots { get; set; }
        public Boots currentBoot { get; set; }

        public int Coins { get; set; }
        public Inventory(List<Weapon> weapons, List<Bodyarmor> bodyarmors, List<Helmet> helmets, List<Boots> boots, int selectedWeapon,int selectedBodyarmor,int selectedHelmet,int selectedBoots,int coins)
        {
            Weapons = weapons;
            Bodyarmors = bodyarmors;
            Helmets = helmets;
            Boots = boots;
            currentWeapon = Weapons[selectedWeapon];
            currentBodyarmor = Bodyarmors[selectedBodyarmor];
            currentHelmet = Helmets[selectedHelmet];
            currentBoot = Boots[selectedBoots];
            Coins = coins;
        }
        public override string ToString()
        {
            return $"Inventory:\n" +
                   $"Weapons: {string.Join(", ", Weapons)}\n" +
                   $"Current Weapon: {currentWeapon}\n" +
                   $"Bodyarmors: {string.Join(", ", Bodyarmors)}\n" +
                   $"Current Bodyarmor: {currentBodyarmor}\n" +
                   $"Helmets: {string.Join(", ", Helmets)}\n" +
                   $"Current Helmet: {currentHelmet}\n" +
                   $"Boots: {string.Join(", ", Boots)}\n" +
                   $"Current Boot: {currentBoot}\n" +
                   $"Coins: {Coins}";
        }

    }
}
