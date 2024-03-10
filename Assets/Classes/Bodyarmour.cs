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
<<<<<<< HEAD
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public int AdditionalArmour { get; set; }
        public int Rarity { get; set; }
        public string Url { get; set; }

        public Bodyarmour(int id, string name, int value, int additionalArmour, int rarity, string url)
        {
            Id = id;
            Name = name;
            Value = value;
            AdditionalArmour = additionalArmour;
            Rarity = rarity;
            Url = url;
=======
        private int totalIds = 0;
        public int Id { get; set; }
        public string Name;
        public int Value;
        public float AdditionalArmour;
        public float Penetration;

        public Bodyarmour(float additionalArmour, string name, int value)
        {
            totalIds++;
            Id = totalIds;
            AdditionalArmour = additionalArmour;
            Name = name;
            Value = value;
        }
        public override string ToString()
        {
            return $"Bodyarmour Details:\n" +
                   $"Name: {Name}\n" +
                   $"Additional Armour: {AdditionalArmour}\n" +
                   $"Value: {Value}\n" +
                   $"Penetration: {Penetration}";
>>>>>>> 50cda48adafea976dd87f7ecdb35b4f881aba08e
        }
    }
}
