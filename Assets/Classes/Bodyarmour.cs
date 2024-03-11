using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class Bodyarmour
    {
        private static int totalIds = 0;

        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public int AdditionalArmour { get; set; }
        public int Rarity { get; set; }
        public string Url { get; set; }

        public Bodyarmour(string name, int value, int additionalArmour, int rarity, string url)
        {
            totalIds++;
            Id = totalIds;
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
