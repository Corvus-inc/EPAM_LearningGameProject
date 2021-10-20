using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField][FormerlySerializedAs("UIMenu")] private GameObject uiMenu;
    [SerializeField][FormerlySerializedAs("UIPause")] private GameObject uiPause;

    private string _nameMainMenuScene = "MainMenuScene";
    private bool isActiveMenuPause = false;
    private bool isActivePause = false;
    private static bool GameIsPaused
    {
        get => GameManager.gameIsPaused;
        set => GameManager.gameIsPaused = value;
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
        GameManager.Pause();
    }
    private void PauseOff()
    {
        uiPause.SetActive(false);
        GameManager.Resume();
    }
    private void PauseInMenu()
    {
        uiMenu.SetActive(true);
        GameManager.Pause();
    }

    private void ResumeInMenu()
    {
        uiMenu.SetActive(false);
        GameManager.Resume();
    }

    public void RestartInMenu()
    {
        GameManager.Restart();
    }

    public void LoadMainMenu()
    {
        if (!string.IsNullOrEmpty(_nameMainMenuScene))
        {
            GameManager.Resume();
            SceneManager.LoadScene(_nameMainMenuScene);
        }
    }

    public void QuitGameMenu()
    {
        _gameManager.QuitGame();
    }
}
