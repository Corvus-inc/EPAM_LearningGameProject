using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    private const string NameStartGameScene = "MainScene";
    private const float RestartDelay = 1f;
    
    public static bool GameIsLoaded { get; set; }
    public bool GameIsPaused { get; private set; }
    
    private bool GameHasEnded { get; set; }

    public void LoadGame()
    {
        GameIsLoaded = true;
        StartGame();
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene(NameStartGameScene);
        Resume();
    }

    public void EndGame()
    {
        if (GameHasEnded) return;
        GameHasEnded = true;
        Restart(); 
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Resume();
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

    }
    public void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
}
