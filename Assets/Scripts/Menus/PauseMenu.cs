using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PauseMenu : MonoBehaviour
{
    public GameState GameState { private get; set; } 
    public StatLoader Loader { private get; set; } 
    
    [SerializeField][FormerlySerializedAs("UIMenu")] private GameObject uiMenu;
    [SerializeField][FormerlySerializedAs("UIPause")] private GameObject uiPause;

    private const string NameMainMenuScene = "MainMenuScene";
    
    private bool _isActiveMenuPause = false;
    private bool _isActivePause = false;
    private bool GameIsPaused => GameState.GameIsPaused;

    void Update()
    {
        if(!_isActivePause)
            IsActiveMenuPause();
        if(!_isActiveMenuPause)
            IsActivePause();
    }

    
    private void IsActiveMenuPause()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        if (GameIsPaused)
        {
            ResumeInMenu();
            _isActiveMenuPause = false;
        }
        else
        {
            PauseInMenu();
            _isActiveMenuPause = true;
        }
    }
    private void IsActivePause()
    {
        if (!Input.GetKeyDown(KeyCode.P)) return;
        if (GameIsPaused)
        {
            PauseOff();
            _isActivePause = false;
        }
        else
        {
            PauseOn();
            _isActivePause = true;
        }
    }
    private void PauseOn()
    {
        uiPause.SetActive(true);
        GameState.Pause();
    }
    private void PauseOff()
    {
        uiPause.SetActive(false);
        GameState.Resume();
    }
    private void PauseInMenu()
    {
        uiMenu.SetActive(true);
        GameState.Pause();
    }

    private void ResumeInMenu()
    {
        uiMenu.SetActive(false);
        GameState.Resume();
    }

    public void RestartInMenu()
    {
        GameState.Restart();
    }

    public void LoadMainMenu()
    {
        if (!string.IsNullOrEmpty(NameMainMenuScene))
        {
            GameState.Resume();
            SceneManager.LoadScene(NameMainMenuScene);
        }
    }

    public void QuitGameMenu()
    {
        GameState.QuitGame();
    }

    public void SavePlayerProgress()
    {
        Loader.SavePlayerDataToPlayerPrefs();
    }
}
