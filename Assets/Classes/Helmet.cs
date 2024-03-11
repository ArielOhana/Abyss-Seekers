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
        private static int totalIds = 0;
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public int AdditionalArmour { get; set; }
        public int Rarity { get; set; }
        public string Url { get; set; }

        public Helmet(string name, int additionalArmour, int value, int rarity, string url)
        {
            totalIds++;
            Id = totalIds;
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
<<<<<<< HEAD
        public string GetID()
        {
            int num = this.Id;
            return num.ToString();
        }
=======
>>>>>>> 347f3af07e1e7a4eff961f3f0d40e176df7a01f7
    }
}
