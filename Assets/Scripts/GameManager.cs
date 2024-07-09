using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class LevelData
{
    public int grid_width;
    public int grid_height;
    public int move_count;
    public List<string> grid;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int currentLevel = 1;
    private int totalLevels = 10;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadMainScene(); // Load the main scene when the game starts
        LoadLevelData(currentLevel); // Load level data when the game starts
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene"); // Load the correct main scene
    }

    public void LoadLevelScene()
    {
        SceneManager.LoadScene("LevelScene");
    }

    public void LoadNextLevel()
    {
        if (currentLevel < totalLevels)
        {
            currentLevel++;
            LoadLevelData(currentLevel); // Load next level data
            UpdateLevelButtonText(); // Update the button text
            LoadLevelScene();
        }
        else
        {
            // Display finished text on LevelButton
        }
    }

    public void ReloadLevel()
    {
        LoadLevelScene();
    }

    public void ShowFailPopup()
    {
        // Display fail popup with options
    }

    public void HandleWinLevel()
    {
        // Show celebration particles and animation
        LoadMainScene();
    }

    public void HandleLoseLevel()
    {
        ShowFailPopup();
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    private void UpdateLevelButtonText()
    {
        GameObject buttonObject = GameObject.Find("Button"); // Find the GameObject named "Button"
        if (buttonObject != null)
        {
            Text buttonText = buttonObject.GetComponentInChildren<Text>(); // Get the Text component of the Button
            if (buttonText != null)
            {
                buttonText.text = "Level " + currentLevel; // Update the button text with the current level
            }
            else
            {
                Debug.LogWarning("Text component not found on the Button GameObject.");
            }
        }
        else
        {
            Debug.LogWarning("Button GameObject not found.");
        }
    }

    private void LoadLevelData(int levelNumber)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "CaseStudyAssetsNoArea/Levels/level_" + levelNumber.ToString("00") + ".json");

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            LevelData levelData = JsonUtility.FromJson<LevelData>(json);

            // Now you can use levelData to set up your level
            SetupLevel(levelData);
        }
        else
        {
            Debug.LogError("Level file not found: " + filePath);
        }
    }

    private void SetupLevel(LevelData levelData)
    {
        // Clear existing level objects if any
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cube");
        foreach (GameObject cube in cubes)
        {
            Destroy(cube);
        }

        // Setup grid dimensions
        int width = levelData.grid_width;
        int height = levelData.grid_height;

        // Setup move count
        int moveCount = levelData.move_count;

        // Spawn grid items based on levelData.grid
        List<string> gridItems = levelData.grid;
        for (int i = 0; i < gridItems.Count; i++)
        {
            // Calculate grid position from index i
            int row = i / width;
            int col = i % width;
            Vector3 position = new Vector3(col, row, 0); // Assuming each grid cell is 1 unit in size

            // Spawn the appropriate object based on gridItems[i]
            GameObject gridItemPrefab = GetGridItemPrefab(gridItems[i]); // Implement GetGridItemPrefab method
            if (gridItemPrefab != null)
            {
                Instantiate(gridItemPrefab, position, Quaternion.identity);
            }
        }

        // Other setup logic like setting move count text, etc.
    }

    private GameObject GetGridItemPrefab(string itemType)
    {
        // Implement logic to return the prefab based on the item type (e.g., Cube, TNT, Box, Stone, Vase)
        // You can use a switch statement or a dictionary to map itemType to prefab
        // For simplicity, assume you have prefabs named "CubePrefab", "TNTPrefab", etc.

        switch (itemType)
        {
            case "r":
            case "g":
            case "b":
            case "y":
                return Resources.Load<GameObject>("CubePrefab");
            case "t":
                return Resources.Load<GameObject>("TNTPrefab");
            case "bo":
                return Resources.Load<GameObject>("BoxPrefab");
            case "s":
                return Resources.Load<GameObject>("StonePrefab");
            case "v":
                return Resources.Load<GameObject>("VasePrefab");
            default:
                return null;
        }
    }
}
