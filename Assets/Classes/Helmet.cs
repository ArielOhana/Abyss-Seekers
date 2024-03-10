using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class Helmet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public int AdditionalArmour { get; set; }
        public int Rarity { get; set; }
        public string Url { get; set; }

        public Helmet(int id, string name, int additionalArmour, int value, int rarity, string url)
        {
            Id = id;
            AdditionalArmour = additionalArmour;
            Name = name;
            Value = value;
            Rarity = rarity;
            Url = url;
        }
    }
}
