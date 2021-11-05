using System;
using Stats;
using UnityEngine;

public class StatLoader
{
    public PlayerData PlayerData { get; set; }
    public event Action OnSavePlayerData;
    public HealthPlayerData HealthPlayerData { get; set; }
    public event Action OnSaveHealthPlayerData;


    private readonly PlayerStats _started =  new PlayerStats()
    {
        health = 100,
        maxHealth = 100,
        speed = 20,
        boostSpeedRate =  2,
        countBullets = 60,
        playerPosition = new float[3]{0,0,0},
        countClip =new [] {0,0},
        startedWeapon = 0
    };
    public PlayerStats LoadablePlayerStats { get; private set; }
    public PlayerStats SavingPlayerStats { get; private set; }
    
    //How transfer only delegate - gameState.IsSaveProgress
    public StatLoader(bool isLoader, GameState gameState)
    {
        HealthPlayerData = new HealthPlayerData();
        var startedData = new PlayerStats(_started);
        LoadablePlayerStats = !isLoader ? startedData : SavingSystem.Load("PlayerData", startedData);
        gameState.IsSaveProgress += SavePlayerStats;
    }

    public void SavePlayerStats()
    {
        OnSavePlayerData?.Invoke();
        OnSaveHealthPlayerData?.Invoke();

        var savingPlayerData = new PlayerStats();
        savingPlayerData.health = HealthPlayerData.Health;
        savingPlayerData.maxHealth = 100;
        savingPlayerData.speed = PlayerData.Speed;
        savingPlayerData.boostSpeedRate = _started.boostSpeedRate;
        savingPlayerData.countBullets = PlayerData.CountBullet;
        savingPlayerData.playerPosition = PlayerData.Position;
        savingPlayerData.countClip = new[] {0, 0};
        savingPlayerData.startedWeapon = 0;
        
        SavingPlayerStats = savingPlayerData;
        SavingSystem.Save(SavingPlayerStats,"PlayerData");
    }

    public PlayerData LoadPlayerData()
    {
        PlayerData = new PlayerData();
        PlayerData.Speed = LoadablePlayerStats.speed;
        PlayerData.BoostSpeedRate = LoadablePlayerStats.boostSpeedRate;
        PlayerData.CountBullet = LoadablePlayerStats.countBullets;
        PlayerData.Position = LoadablePlayerStats.playerPosition;
        return PlayerData;
    }
}

public class PlayerData
{
    public float Speed { get; set; }
    public float BoostSpeedRate { get; set; }
    public int CountBullet { get; set; }
    public float[] Position { get; set; }
}
public class WeaponPlayerData
{
    public int index { get; set; }
    // public int countBullet { get; set; }
    // public float[] position { get; set; }
}
public class HealthPlayerData
{
    public int Health { get; set; }
}