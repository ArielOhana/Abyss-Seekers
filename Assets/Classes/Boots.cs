using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class Boots
    {
        private static int totalIds = 0;
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public int AdditionalArmour { get; set; }
        public int Rarity { get; set; }
        public string Url { get; set; }

        public Boots(string name, int value, int additionalArmour, int rarity, string url)
        {
<<<<<<< HEAD
            totalIds++;
            Id = totalIds;
=======
            Id = id;

>>>>>>> 347f3af07e1e7a4eff961f3f0d40e176df7a01f7
            Name = name;
            Value = value;
            AdditionalArmour = additionalArmour;
            Rarity = rarity;
            Url = url;
        }
        public string GetID()
        {
            int num = this.Id;
            return num.ToString();
        }
    }
}
