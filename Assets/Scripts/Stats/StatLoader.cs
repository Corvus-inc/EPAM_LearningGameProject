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
        PlayerPrefs.SetString("PlayerData", JsonSerializationPlayerStats(SavablePlayerStats));
    }

    private PlayerStats LoadPlayerDataFromPlayerPrefs()
    {
        LoadablePlayerStats = JsonDeserializationPlayerStats(PlayerPrefs.GetString("PlayerData"));
        return LoadablePlayerStats;
    }
    
    
    private string JsonSerializationPlayerStats (PlayerStats playerStats)
    {
        var json = JsonUtility.ToJson(playerStats);
        return json;
    }

    private PlayerStats JsonDeserializationPlayerStats(string jsonPlayerStats)
    {
        var playerStats = JsonUtility.FromJson<PlayerStats>(jsonPlayerStats);
        return playerStats;
    }
}
