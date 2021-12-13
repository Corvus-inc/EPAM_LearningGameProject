using LoaderSystem;

public interface IPlayer
{
    PlayerLevel MyLevel { get; set; }
    IStatLoader StatLoader { set; }
    int CountBullets { get; }
    IGameState GameState { set; }
    IHealthSystem HealthSystem { set; }
    WeaponSystem WeaponSystem { set; }
    bool IsBoostedSpeed { set; }
    void LoadPlayer(PlayerData loadPlayerData);
}