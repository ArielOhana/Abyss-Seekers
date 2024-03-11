using System;
using System.Collections.Generic;

namespace Assets
{
	public class AllItems
	{
		public List<Weapon> AllWeapons { get; set; }
        public List<Helmet> AllHelmets { get; set; }
        public List<Bodyarmour> AllBodyArmours { get; set; }
        public List<Boots> AllBoots { get; set; }
        public AllItems(List<Weapon> allWeapons, List<Helmet> allHelmets, List<Bodyarmour> allBodyArmours, List<Boots> allBoots)
        {
            AllWeapons = allWeapons ?? throw new ArgumentNullException(nameof(allWeapons));
            AllHelmets = allHelmets ?? throw new ArgumentNullException(nameof(allHelmets));
            AllBodyArmours = allBodyArmours ?? throw new ArgumentNullException(nameof(allBodyArmours));
            AllBoots = allBoots ?? throw new ArgumentNullException(nameof(allBoots));
        }
    }

}

