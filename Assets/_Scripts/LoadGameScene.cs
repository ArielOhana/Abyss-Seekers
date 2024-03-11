using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Assets;
using System.Linq;

public class LoadGameScene : MonoBehaviour
{
    public SQLdb DBManager;
    public GameObject gameContainer; // Assign the game container prefab in the inspector
    public Transform gamesContainer;
    public TextMeshProUGUI statusText;

    private List<GameData> savedGames = new List<GameData>(4);

    [System.Serializable]
    public class GameData
    {
        public string playerName;
        public string gameName;
        public string role;
    }

    Vector3[] spawns = new[]
    {
        new Vector3(-2.16f, 7, 0),
        new Vector3(-0.67f, 7, 0),
        new Vector3(0.75f, 7, 0),
        new Vector3(2.22f, 7, 0)
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

    void LoadSavedGames()
    {
        List<Hero> heroes = DBManager.AllHeros();

        foreach (Hero hero in heroes)
        {
            GameData gameData = new GameData();
            gameData.playerName = hero.Name;
            gameData.gameName = $"{hero.Name}'s Game";
            gameData.role = hero.Role;

            savedGames.Add(gameData);
        }
    }

    void UpdateUI()
    {
        // Destroy all previous game containers
        foreach (Transform child in gamesContainer)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < savedGames.Count; i++)
        {
            // Instantiate the game container prefab
            GameObject gameContainerInstance = Instantiate(gameContainer, gamesContainer);

            // Get the canvas transform from the game container instance
            Transform canvasTransform = gameContainerInstance.transform.Find("Canvas");

            // Set position if within bounds
            if (i < spawns.Length)
            {
                Vector3 position = GetNextPosition();
                gameContainerInstance.transform.localPosition = position;
            }

            // Access UI elements from the canvas
            TextMeshProUGUI gameText = canvasTransform.Find("GameText")?.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI roleText = canvasTransform.Find("RoleText")?.GetComponent<TextMeshProUGUI>();
            Button startButton = canvasTransform.Find("StartButton")?.GetComponent<Button>();
            Button deleteButton = canvasTransform.Find("DeleteButton")?.GetComponent<Button>();

            // Check if UI elements are not null before updating
            if (gameText != null && roleText != null && startButton != null && deleteButton != null)
            {
                // Update UI elements with data
                gameText.text = savedGames[i].gameName;
                roleText.text = savedGames[i].role;

                // Add listeners to buttons
                startButton.onClick.AddListener(() => StartGame(i));
                deleteButton.onClick.AddListener(() => DeleteGame(i));
            }
            else
            {
                Debug.LogError("UI elements not found in Game Container prefab.");
            }
        }

        // Set the visibility of createNewGameButton
        createNewGameButton.gameObject.SetActive(savedGames.Count < 5);
    }

    public void StartGame(int index)
    {
        string playerName = savedGames[index].playerName;
        Debug.Log("Starting game for player: " + playerName);
    }

    public void DeleteGame(int index)
    {
        string playerName = savedGames[index].playerName;
        Debug.Log("Deleting game for player: " + playerName);

        DBManager.DeleteHero(playerName);

        savedGames.RemoveAt(index);
        UpdateUI();
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
