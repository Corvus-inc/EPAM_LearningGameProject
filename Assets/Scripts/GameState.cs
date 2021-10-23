using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public static bool gameIsPaused = false;

    private static bool gameHasEnded = false;

    private const string _nameStartGameScene = "MainScene";

    public float restartDelay = 1f;

    public void StartGame()
    {
        SceneManager.LoadScene(_nameStartGameScene);
        Resume();
    }

    public void EndGame()
    {
        if (gameHasEnded != false) return;
        gameHasEnded = true;
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
        gameIsPaused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        
    }
}
