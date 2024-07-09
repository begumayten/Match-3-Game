using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
}
