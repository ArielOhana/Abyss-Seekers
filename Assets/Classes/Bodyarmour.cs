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
        }
    }
}
