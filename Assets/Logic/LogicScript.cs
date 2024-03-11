using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{

    public Hero myHero;
    public GameObject Circle;
    private GetAllButtons getAllButtons;

    void Start()
    {
<<<<<<< HEAD
        SQLdb DBManager = new SQLdb();
        //DBManager.ReadJson();
        //DBManager.CreateHero("sasi", "orc");
        //DBManager.CreateHero("or", "elf");
        Hero hero = DBManager.GetHero("ori");
        DBManager.SaveHero(hero);
        for (int i = 0; i < hero.Inventory.Boots.Count; i++)
        {
            Debug.Log(hero.Inventory.Boots[i].Name);
        }
=======
       
        //SQLdb DBManager = new SQLdb();  // Create an instance of the SQLdb class
        //DBManager.NewHero("dor", "mage");

>>>>>>> 347f3af07e1e7a4eff961f3f0d40e176df7a01f7
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
