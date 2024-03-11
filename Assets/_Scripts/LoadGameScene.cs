using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

public class LoadGameScene : MonoBehaviour
{
    public GameObject gameContainerPrefab;
    public Transform gamesContainer;
    public Button createNewGameButton;
    public TextMeshProUGUI statusText;

    private List<GameData> savedGames = new List<GameData>(4);

    [System.Serializable]
    public class GameData
    {
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

    void Start()
    {
        rndm = new System.Random();
        maxSpawnItr = spawns.Length;
        ShufflePositions();

        LoadSavedGames();
        UpdateUI();
    }

    void LoadSavedGames()
    {
        savedGames.Add(new GameData { gameName = "Game 1 - Warrior", role = "Warrior" });
        savedGames.Add(new GameData { gameName = "Game 2 - Rogue", role = "Rogue" });
    }

    void UpdateUI()
    {
        for (int i = 0; i < savedGames.Count; i++)
        {
            GameObject gameContainer = Instantiate(gameContainerPrefab, gamesContainer);
            int index = i;

            Transform canvasTransform = gameContainer.transform.Find("Canvas");

            if (i < spawns.Length)
            {
                Vector3 position = GetNextPosition();
                gameContainer.transform.localPosition = position;
            }

            TextMeshProUGUI gameText = canvasTransform.Find("GameText")?.GetComponent<TextMeshProUGUI>();
            if (gameText != null)
            {
                gameText.text = savedGames[i].gameName;
            }
            else
            {
                Debug.LogError("GameText not found in Game Container prefab.");
            }

            TextMeshProUGUI roleText = canvasTransform.Find("RoleText")?.GetComponent<TextMeshProUGUI>();
            if (roleText != null)
            {
                roleText.text = savedGames[i].role;
            }
            else
            {
                Debug.LogError("RoleText not found in Game Container prefab.");
            }

            Button startButton = canvasTransform.Find("StartButton")?.GetComponent<Button>();
            if (startButton != null)
            {
                startButton.onClick.AddListener(() => StartGame(index));
            }
            else
            {
                Debug.LogError("StartButton not found in Game Container prefab.");
            }

            Button deleteButton = canvasTransform.Find("DeleteButton")?.GetComponent<Button>();
            if (deleteButton != null)
            {
                deleteButton.onClick.AddListener(() => DeleteGame(index));
            }
            else
            {
                Debug.LogError("DeleteButton not found in Game Container prefab.");
            }

            Button createNewGameButton = canvasTransform.Find("CreateNewGameButton")?.GetComponent<Button>();
            if (createNewGameButton != null)
            {
                createNewGameButton.onClick.AddListener(() => CreateNewGame());
            }
            else
            {
                Debug.LogError("CreateNewGameButton not found in Game Container prefab.");
            }

            TextMeshProUGUI statusText = canvasTransform.Find("StatusText")?.GetComponent<TextMeshProUGUI>();
            if (statusText != null)
            {
            }
            else
            {
                Debug.LogError("StatusText not found in Game Container prefab.");
            }
        }

        if (savedGames.Count < 4)
        {
            createNewGameButton.gameObject.SetActive(true);
        }
        else
        {
            createNewGameButton.gameObject.SetActive(false);
        }
    }

    public void StartGame(int index)
    {
        string selectedGame = savedGames[index].gameName;
        Debug.Log("Starting game: " + selectedGame);
    }

    public void DeleteGame(int index)
    {
        string deletedGame = savedGames[index].gameName;
        savedGames.RemoveAt(index);
        Debug.Log("Deleted game: " + deletedGame);

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
