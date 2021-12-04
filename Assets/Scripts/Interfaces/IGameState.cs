using System;

public interface IGameState
{
    event Action IsSaveProgress;
    bool GameIsPaused { get; }
    bool GameIsLoaded { get; set; }
    void Pause();
    void Resume();
    void Restart();
    void QuitGame();
    void SaveGame();
    void EndGame();
}