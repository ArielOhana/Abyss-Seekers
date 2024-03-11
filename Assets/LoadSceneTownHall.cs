using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("TownHall");
    }
    public void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
    public void CreatePlayer()
    {
        SceneManager.LoadScene("TownHall");
    }
    
    public void EnterInventroy()
    {
        SceneManager.LoadScene("Inventory");
    }
}
