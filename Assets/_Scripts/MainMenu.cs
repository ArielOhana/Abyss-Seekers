using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    [SerializeField] GameObject creditsMenu;

    void Start()
    {
        creditsMenu.SetActive(false);
    }

    IEnumerator LoadLevel(int levelIndex)  
    {
        transition.SetTrigger("start"); 
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

    public void PlayGame()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

<<<<<<< HEAD
=======
    public void GoToLoadGame()
    {
        SceneManager.LoadScene("LoadGame");
    }

>>>>>>> 347f3af07e1e7a4eff961f3f0d40e176df7a01f7
    public void QuitGame()
    {
        Application.Quit();
    }
}
