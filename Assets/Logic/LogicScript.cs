using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DBContext;

public class LogicScript : MonoBehaviour
{
    public Hero myHero;
    public GameObject Circle;
    void Start()// Get all the data from database and import it to Classes
    {
        myHero = new Hero();
        myHero.InitializeStats(); // Initialize Stats separately
        Circle.transform.position += new Vector3(0f, -3f, 0f);

        
        SQLdb sqlDbInstance = new SQLdb();  // Create an instance of the SQLdb class
        sqlDbInstance.FillDB();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
