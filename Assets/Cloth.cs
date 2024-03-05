using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets
{
    public class Cloth
    {
        public Boots Boots;
        public Helmet Helmet;
        public Bodyarmor Bodyarmor;

        public Cloth(Boots boots, Helmet helmet, Bodyarmor bodyarmor)
        {
            Boots = boots;
            Helmet = helmet;
            Bodyarmor = bodyarmor;
        }
        public float SumAdditionalArmor()
        {
            return Boots.AdditionalArmor+Helmet.AdditionalArmor+Bodyarmor.AdditionalArmor;
        }
    }
}
