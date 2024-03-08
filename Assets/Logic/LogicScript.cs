using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DBContext;
public class LogicScript : MonoBehaviour
{

    public Hero myHero;
    public GameObject? Circle;
    private GetAllButtons getAllButtons;
    //private Button[] ButtonsArray;
    void Start()// Get all the data from database and import it to Classes
    {
        myHero = new Hero();
        myHero.InitializeStats(); // Initialize Stats separately
        //this.ButtonsArray = getAllButtons.GetButtonsArray();

        
        SQLdb sqlDbInstance = new SQLdb();  // Create an instance of the SQLdb class
        sqlDbInstance.FillDB();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
