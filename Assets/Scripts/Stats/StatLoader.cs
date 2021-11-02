using Stats;
using UnityEngine;

public class StatLoader
{
    private readonly PlayerStats _started =  new PlayerStats()
    {
        health = 100,
        maxHealth = 100,
        speed = 20,
        boostSpeedRate =  2,
        countBullets = 60,
        playerPosition = new float[3]{0,0,0},
        countClip = 0
    };
    public PlayerStats LoadablePlayerStats { get;}
    
    public StatLoader(bool isLoader)
    {
        var startedData = new PlayerStats(_started);
        LoadablePlayerStats = !isLoader ? startedData : SavingSystem.Load("PlayerData", startedData);
    }
}
