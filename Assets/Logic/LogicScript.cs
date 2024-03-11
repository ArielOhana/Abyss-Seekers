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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
