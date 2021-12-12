using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour, IGameState
{
    private const string NameStartGameScene = "MainScene";
    private const float RestartDelay = 1f;
    private bool GameHasEnded { get; set; }

    public event Action IsSaveProgress;

    public bool GameIsLoaded { get; set; }
    public bool GameIsPaused { get; private set; }

    private void Awake()
    {
        var doubleGS = FindObjectsOfType<GameState>();
        foreach (var el in doubleGS)
        {
            if (!ReferenceEquals( gameObject, el.gameObject))
            {
                Destroy(el.gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    public void LoadGame()
    {
        GameIsLoaded = true;
        StartGame();
    }

    public void SaveGame()
    {
        IsSaveProgress?.Invoke();
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
        GameHasEnded = false;
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