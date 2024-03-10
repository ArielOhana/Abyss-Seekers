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
<<<<<<< HEAD
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public int AdditionalArmour { get; set; }
        public int Rarity { get; set; }
        public string Url { get; set; }

        public Helmet(int id, string name, int additionalArmour, int value, int rarity, string url)
        {
            Id = id;
=======
        private int totalIds = 0;
        public int Id { get; set; }
        public string Name;
        public int Value;
        public float AdditionalArmour;

        public Helmet(float additionalArmour, string name, int value)
        {
            totalIds++;
            Id = totalIds;
>>>>>>> 50cda48adafea976dd87f7ecdb35b4f881aba08e
            AdditionalArmour = additionalArmour;
            Name = name;
            Value = value;
            Rarity = rarity;
            Url = url;
        }
        public void Print()
        {
            Debug.Log($"Helmet Details:\n" +
                      $"Name: {Name}\n" +
                      $"Value: {Value}\n" +
                      $"Additional Armour: {AdditionalArmour}");
        }
    }
}
