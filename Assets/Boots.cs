using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets
{
    public class Boots
    {
        public string Name;
        public int Value;
        public float AdditionalArmor;
        public float Penetration;
        public Boots(float additionalArmor, string name, int value)
        {
            AdditionalArmor = additionalArmor;
            Name = name;
            Value = value;
        }
    }
}
