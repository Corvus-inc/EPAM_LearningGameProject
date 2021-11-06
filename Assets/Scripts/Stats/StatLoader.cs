using System;
using Stats;
using UnityEngine;

public class StatLoader
{
    public PlayerData PlayerData { get; private set; }
    public HealthPlayerData HealthPlayerData { get; private set; }
    public WeaponPlayerData WeaponPlayerData{ get; private set; }
    
    public event Action OnSavePlayerData;


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
    private PlayerStats SavingPlayerStats { get; set; }
    
    //How transfer only delegate - gameState.IsSaveProgress
    public StatLoader(bool isLoader, GameState gameState)
    {
        var startedData = new PlayerStats(_started);
        LoadablePlayerStats = !isLoader ? startedData : SavingSystem.Load("PlayerData", startedData);
        gameState.IsSaveProgress += SavePlayerStats;
        
        HealthPlayerData = LoadHealthPlayerData();
        WeaponPlayerData = LoadWeaponPlayerData();
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

    private void SavePlayerStats()
    {
        OnSavePlayerData?.Invoke();

        var savingPlayerData = new PlayerStats();
        savingPlayerData.health = HealthPlayerData.Health;
        savingPlayerData.maxHealth = _started.health;
        savingPlayerData.speed = PlayerData.Speed;
        savingPlayerData.boostSpeedRate = _started.boostSpeedRate;
        savingPlayerData.countBullets = PlayerData.CountBullet;
        savingPlayerData.playerPosition = PlayerData.Position;
        savingPlayerData.countClip = new[] {0, 0};
        savingPlayerData.startedWeapon = WeaponPlayerData.index;
        
        SavingPlayerStats = savingPlayerData;
        SavingSystem.Save(SavingPlayerStats,"PlayerData");
    }


    private HealthPlayerData LoadHealthPlayerData()
    {
        HealthPlayerData = new HealthPlayerData();
        HealthPlayerData.MaxHealth = LoadablePlayerStats.maxHealth;
        HealthPlayerData.Health = LoadablePlayerStats.health;
        return HealthPlayerData;
    }

    private WeaponPlayerData LoadWeaponPlayerData()
    {
        WeaponPlayerData = new WeaponPlayerData();
        WeaponPlayerData.index = LoadablePlayerStats.startedWeapon;
        return WeaponPlayerData;
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
    public int MaxHealth { get; set; }
    public int Health { get; set; }
}