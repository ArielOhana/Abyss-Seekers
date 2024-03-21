using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets;
 
namespace Context
{
    public class MainMenu : MonoBehaviour
    {
        public static Hero currentHero;
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

        public void GoToLoadGame()
        {
            SceneManager.LoadScene("Load");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}

