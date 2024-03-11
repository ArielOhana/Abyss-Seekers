using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    private SQLdb DBManager;

     void Start()
        {
            DBManager = new SQLdb();
        }

    public void LoadScene()
    {
        SceneManager.LoadScene("TownHall");
    }
    public void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
    public void CreatePlayer(string name,string role)
    {
        DBManager.CreateHero(name, role);
        SceneManager.LoadScene("TownHall");
    }
    
    public void EnterInventroy()
    {
        SceneManager.LoadScene("Inventory");
    }
}
