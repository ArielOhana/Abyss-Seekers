using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Context;

public class LogicScript : MonoBehaviour
{
    private readonly object currentHero;
    public Hero myHero;
    public GameObject Circle;

    void Start()
    {
        SQLdb DBManager = new SQLdb();
        DBManager.ReadJson();
        DBManager.CreateHero("dor", "warrior");
        MainMenu.currentHero = DBManager.GetHero("dor");
        List<Enemy> enemies = MainMenu.currentHero.SpawnEnemies();
        Debug.Log(enemies.Count);
        enemies = MainMenu.currentHero.SpawnEnemies();
        Debug.Log(enemies.Count);
        // Update is called once per frame
    }
       void Update()
        {

        }
}