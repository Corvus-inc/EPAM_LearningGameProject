using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameState _gameState;
    [SerializeField][FormerlySerializedAs("UIMenu")] private GameObject uiMenu;
    [SerializeField][FormerlySerializedAs("UIPause")] private GameObject uiPause;

    private string _nameMainMenuScene = "MainMenuScene";
    private bool isActiveMenuPause = false;
    private bool isActivePause = false;
    private static bool GameIsPaused
    {
        get => GameState.gameIsPaused;
        set => GameState.gameIsPaused = value;
    }

    void Update()
    {
        if(!isActivePause)
        IsActiveMenuPause();
        if(!isActiveMenuPause)
        IsActivePause();
    }

    
    private void IsActiveMenuPause()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        if (GameIsPaused)
        {
            ResumeInMenu();
            isActiveMenuPause = false;
        }
        else
        {
            PauseInMenu();
            isActiveMenuPause = true;
        }
    }
    private void IsActivePause()
    {
        if (!Input.GetKeyDown(KeyCode.P)) return;
        if (GameIsPaused)
        {
            PauseOff();
            isActivePause = false;
        }
        else
        {
            PauseOn();
            isActivePause = true;
        }
    }
    private void PauseOn()
    {
        uiPause.SetActive(true);
        _gameState.Pause();
    }
    private void PauseOff()
    {
        uiPause.SetActive(false);
        _gameState.Resume();
    }
    private void PauseInMenu()
    {
        uiMenu.SetActive(true);
        _gameState.Pause();
    }

    private void ResumeInMenu()
    {
        uiMenu.SetActive(false);
        _gameState.Resume();
    }

    public void RestartInMenu()
    {
        _gameState.Restart();
    }

    public void LoadMainMenu()
    {
        if (!string.IsNullOrEmpty(_nameMainMenuScene))
        {
            _gameState.Resume();
            SceneManager.LoadScene(_nameMainMenuScene);
        }
    }

    public void QuitGameMenu()
    {
        _gameState.QuitGame();
    }
}
