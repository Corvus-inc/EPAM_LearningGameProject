using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public bool GameIsPaused { get; set; }
    private bool GameHasEnded { get; set; }
    private const string NameStartGameScene = "MainScene";
    private const float RestartDelay = 1f;

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
