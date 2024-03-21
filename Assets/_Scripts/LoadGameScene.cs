using Assets;
using Context;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGameScene : MonoBehaviour
{
    public SQLdb DBManager;
    public GameObject gameContainer;
    public Transform gamesContainer;
    public Text statusText;
    
    private List<GameData> savedGames = new List<GameData>();

    [System.Serializable]
    public class GameData
    {
        public string playerName;
        public string gameName;
        public string role;
    }

    Vector3[] spawns = new[]
    {
        new Vector3(-2.16f, 320.7619f, 0),
        new Vector3(-0.67f, 150, 0),
        new Vector3(0.75f, -20, 0),
        new Vector3(2.22f, -190, 0)
    };

    private int currentSpawnItr = 0;
    private int maxSpawnItr = 0;

    private System.Random rndm;
    public Button createNewGameButton;

    void Start()
    {
        DBManager = new SQLdb();
        rndm = new System.Random();
        maxSpawnItr = spawns.Length;
        ShufflePositions();
        LoadSavedGames();
        UpdateUI();
}

    public class Hero
    {
        public string Name;
        public string Role;
    }

    void LoadSavedGames()
    {
        List<Assets.Hero> heroes = new();
        var AllHeroes = DBManager.AllHeros();
        if(AllHeroes.Count < 4)
        {

        }
        foreach (var hero in AllHeroes)
        {
            heroes.Add(hero);
        }


        savedGames.Clear();
        foreach (var hero in heroes)
        {
            GameData gameData = new GameData
            {
                playerName = hero.Name,
                gameName = hero.Name, 
                role = hero.Role
            };
            savedGames.Add(gameData);
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < savedGames.Count; i++)
        {
            GameObject gameContainerInstance = Instantiate(gameContainer, gamesContainer);

            Transform canvasTransform = gameContainerInstance.transform.Find("PreFabCanvas");

            if (i < spawns.Length)
            {
                Vector3 position = GetNextPosition();
                gameContainerInstance.transform.localPosition = position;
            }

            GameObject canvasObject = gameContainerInstance.transform.Find("PreFabCanvas").gameObject;
            canvasObject.SetActive(true);

            Text gameText = canvasTransform.Find("GameText")?.GetComponent<Text>();
            Text roleText = canvasTransform.Find("RoleText")?.GetComponent<Text>();
            Button startButton = canvasTransform.Find("StartButton")?.GetComponent<Button>();
            Button deleteButton = canvasTransform.Find("DeleteButton")?.GetComponent<Button>();

            if (gameText != null && roleText != null && startButton != null && deleteButton != null)
            {
                gameText.text = savedGames[i].gameName;
                roleText.text = savedGames[i].role;

                int index = i; 
                startButton.onClick.AddListener(() =>
                {
                    StartGame(savedGames[index].gameName);
                });

                deleteButton.onClick.AddListener(() => DeleteGame(index));
            }
            else
            {
                Debug.LogError("UI elements not found in Game Container prefab.");
            }
        }

        createNewGameButton.gameObject.SetActive(savedGames.Count < 5);
    }

    public void StartGame(string heroName)
    {
        try
        {
            Debug.Log("Starting game for player: " + heroName);
            MainMenu.currentHero = DBManager.GetHero(heroName);
            //Inv.RenderValues();
            print(MainMenu.currentHero.Role);
            SceneManager.LoadScene("TownHall");
        }
        catch (Exception ex)
        {
            Debug.Log("Error: " + ex.Message);
        }
    }


    public void DeleteGame(int index)
    {
        Debug.Log("Attempting to delete game at index: " + index);
        Debug.Log("Number of saved games before deletion: " + savedGames.Count);

        if (index >= 0 && index < savedGames.Count)
        {
            Debug.Log("Deleting game for player: " + savedGames[index].playerName);

            string playerName = savedGames[index].playerName;
            DBManager.DeleteHero(playerName);

            savedGames.RemoveAt(index);
            UpdateUI();
        }
        else
        {
            Debug.LogError("Invalid index: " + index);
        }
    }


    public void CreateNewGame()
    {
        SceneManager.LoadScene("playerCreator");
    }

    private Vector3 GetNextPosition()
    {
        if (currentSpawnItr == maxSpawnItr)
        {
            currentSpawnItr = 0;
            ShufflePositions();
        }

        return spawns[currentSpawnItr++];
    }

    private void ShufflePositions()
    {
        spawns = spawns.OrderBy(spawn => rndm.Next()).ToArray();
    }
}
