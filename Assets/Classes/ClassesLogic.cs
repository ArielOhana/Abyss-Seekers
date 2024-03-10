using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
=======
using DBContext;
>>>>>>> 50cda48adafea976dd87f7ecdb35b4f881aba08e
using Assets;

namespace classesActions
{
    public class ClassesLogic : MonoBehaviour
    {
        //public static Hero LoadUser(string heroName)
        //{
        //    SQLdb dbContext = new SQLdb();
        //    //Hero currentHero = dbContext.GetUser(heroName);
        //    //return currentHero;
        //    return null;
        //}
        public void SaveUser(string heroName)
        {
            //Hero currentHero = SQLdb.GetUser(heroName);

        }
        public Object[] FullHero(Hero hero)
        {
            int inventoryID = hero.Inventory.Id;
            int statsID = hero.Stats.Id;
            Inventory bag = GetInventory(inventoryID);
            Stats HeroStats = GetStats(statsID);
            return null;
        }
        private Inventory GetInventory(int inventoryID)
        {
            // Add logic to retrieve and return the inventory based on the ID
            return null;
        }

        private Stats GetStats(int statsId)
        {
            // Add logic to retrieve and return the stats based on the ID
            return null;
        }

    }
}
