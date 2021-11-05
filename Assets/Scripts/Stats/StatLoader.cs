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
        countClip =new [] {0,0}
    };
    public PlayerStats LoadablePlayerStats { get;}
    
    public StatLoader(bool isLoader)
    {
        var startedData = new PlayerStats(_started);
        LoadablePlayerStats = !isLoader ? startedData : SavingSystem.Load("PlayerData", startedData);
    }

    public void SetDataForPlayer(PlayerCharacter pc)
    {
        // _speed = LoadablePlayerStats.speed;
        // _boostSpeedRate = LoadablePlayerStats.boostSpeedRate;
        // CountBullets = LoadablePlayerStats.countBullets;
        // var position = LoadablePlayerStats.playerPosition;
        // transform.position = new Vector3(position[0], position[1], position[2]);
    }
    
    public void SetDataForWeapons()
    {
        
    }
    
    public void SetDataForSkills()
    {
        
    }
    
    public void SetDataForEnemy()
    {
        
    }
}
