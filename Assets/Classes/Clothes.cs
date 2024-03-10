using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class Clothes
    {
        private int totalIds = 0;
        public int Id { get; set; }
        public Boots Boots;
        public Helmet Helmet;
        public Bodyarmour Bodyarmour;

        public Clothes(Boots boots, Helmet helmet, Bodyarmour bodyarmour)
        {
            totalIds++;
            Id = totalIds;
            Boots = boots;
            Helmet = helmet;
            Bodyarmour = bodyarmour;
        }
        public float SumAdditionalArmour()
        {
            float num =
                Boots.AdditionalArmour+Helmet.AdditionalArmour+Bodyarmour.AdditionalArmour;

            return num;
        }
    }
}
