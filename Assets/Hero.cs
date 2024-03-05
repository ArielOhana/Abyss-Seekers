using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace Assets
{
    public class Hero
    {
        // Level property
        protected int Level { get; set; }

        // XP property
        protected int Xp { get; set; }

        // Weapons property
        protected Weapon Weapon { get; set; }

        // Inventory property
        protected Inventory Inventory { get; set; }

        // Clothes property
        protected Cloth Clothes { get; set; }

        // Role property
        protected Role Role { get; set; }
        public Stats Stats { get; set; }

        // Constructor
        public Hero()
        {
            Weapon = new Weapon(15,1,  30,  1, 100,"Katana");
            Inventory = new Inventory(new List<Weapon>{Weapon,Weapon},new List<Bodyarmor> {new Bodyarmor(15.2f,"Ariel's Body Armor", 100), new Bodyarmor(12.2f, "Ohana's Body Armor", 120),new Bodyarmor(22.2f, "Best Body Armor", 220) },new List<Helmet> { new Helmet(13f,"Ariel's Helmet", 80), new Helmet(16.3f,"Ohana's Helmet",110), new Helmet(18.3f, "Max Helmet", 140) }, new List<Boots> { new Boots(2f, "Ariel's Boots", 20), new Boots(4.3f, "Ohana's Boots",40), new Boots(6.3f, "Max Boots", 80) }, 0,0,0,0,100);
            Clothes = new Cloth(Inventory.currentBoot,Inventory.currentHelmet,Inventory.currentBodyarmor);
            Xp = 0;
            Level = 0;
            Role = new Role(new string[] { "Bomb", "Fly" }, "Witch", 100f, 1, 2, 3, 4, 5, 6, 7, 8);


        }
        public void InitializeStats()
        {
            Stats = new Stats();
            Debug.Log(Stats.ToString());
            Debug.Log(Inventory.ToString());
        }
    }
}