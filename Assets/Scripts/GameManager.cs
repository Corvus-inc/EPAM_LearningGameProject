using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool gameIsPaused = false;

    private static bool gameHasEnded = false;

    private const string _nameStartGameScene = "MainScene";

    public float restartDelay = 1f;

    public void StartGame()
    {
        SceneManager.LoadScene(_nameStartGameScene);
    }

    public void EndGame()
    {
        if(gameHasEnded == false)
        {
            gameHasEnded = true;
            Invoke("Restart", restartDelay);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

    }
    public static void Pause()
    {
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public static void Resume()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        
    }
}
