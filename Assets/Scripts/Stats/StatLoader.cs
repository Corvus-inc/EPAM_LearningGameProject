using System;
using Stats;
using UnityEngine;

public class StatLoader
{
    public PlayerData PlayerData { get; set; }
    public event Action OnSavePlayer;

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
        var startedData = new PlayerStats(_started);
        LoadablePlayerStats = !isLoader ? startedData : SavingSystem.Load("PlayerData", startedData);
        gameState.IsSaveProgress += SavePlayerStats;
    }

    public void SavePlayerStats()
    {
        OnSavePlayer?.Invoke();

        var savingPlayerData = new PlayerStats();
        savingPlayerData.health = 100;
        savingPlayerData.maxHealth = 100;
        savingPlayerData.speed = PlayerData.speed;
        savingPlayerData.boostSpeedRate = _started.boostSpeedRate;
        savingPlayerData.countBullets = PlayerData.countBullet;
        savingPlayerData.playerPosition = PlayerData.position;
        savingPlayerData.countClip = new[] {0, 0};
        savingPlayerData.startedWeapon = 0;
        
        SavingPlayerStats = savingPlayerData;
        SavingSystem.Save(SavingPlayerStats,"PlayerData");
    }

    public PlayerData LoadPlayerData()
    {
        PlayerData = new PlayerData();
        PlayerData.speed = LoadablePlayerStats.speed;
        PlayerData.boostSpeedRate = LoadablePlayerStats.boostSpeedRate;
        PlayerData.countBullet = LoadablePlayerStats.countBullets;
        PlayerData.position = LoadablePlayerStats.playerPosition;
        return PlayerData;
    }
}

public class PlayerData
{
    public float speed { get; set; }
    public float boostSpeedRate { get; set; }
    public int countBullet { get; set; }
    public float[] position { get; set; }
}
public class WeaponPlayerData
{
    public int index { get; set; }
    // public int countBullet { get; set; }
    // public float[] position { get; set; }
}