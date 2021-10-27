using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatLoader
{
    private readonly PlayerStats _startedPlayerStats =  new PlayerStats()
    {
        health = 100,
        maxHealth = 100,
        speed = 20,
        boostSpeedRate =  2,
        countBullets = 30,
    };
    
    public PlayerStats LoadablePlayerStats { get; private set; }
    public PlayerStats SavablePlayerStats {private get; set; }
    
    public StatLoader(bool isLoader)
    {
        LoadablePlayerStats = !isLoader ? _startedPlayerStats : LoadPlayerDataFromPlayerPrefs();
    }

    public void SavePlayerDataToPlayerPrefs()
    {
        PlayerPrefs.SetString("PlayerData",LoaderSystem.JsonSerialization(SavablePlayerStats));
    }

    private PlayerStats LoadPlayerDataFromPlayerPrefs()
    {
        LoadablePlayerStats = LoaderSystem.JsonDeserialization<PlayerStats>(PlayerPrefs.GetString("PlayerData"));
        return LoadablePlayerStats;
    }
}
