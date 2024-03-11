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
        private int totalIds = 0;
        public int Id { get; set; }
        public string Name;
        public int Value;
        public float AdditionalArmour;

        public Helmet(float additionalArmour, string name, int value)
        {
            totalIds++;
            Id = totalIds;
            AdditionalArmour = additionalArmour;
            Name = name;
            Value = value;
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
