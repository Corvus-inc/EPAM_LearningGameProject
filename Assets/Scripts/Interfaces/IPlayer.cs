using LoaderSystem;

public interface IPlayer
{
    StatLoader StatLoader { set; }
    int CountBullets { get; }
    GameState GameState { set; }
    IHealthSystem HealthSystem { set; }
    WeaponSystem WeaponSystem { set; }
    bool IsBoostedSpeed { set; }
    void LoadPlayer(PlayerData loadPlayerData);
}