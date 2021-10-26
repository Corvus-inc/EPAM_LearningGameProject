using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatLoader
{
    private PlayerStats startedPlayerStats =  new PlayerStats()
    {
        heath = 100,
        maxHeath = 100,
        speed = 20,
        boostSpeedRate =  2,
        countBullets = 30,
    };
    
    public PlayerStats LoadablePlayerStats { get; private set; }
    public StatLoader(bool isLoader)
    {
        if (!isLoader)
        {
            LoadablePlayerStats = startedPlayerStats;
        }
        else LoadablePlayerStats = LoadPlayerDataFromPlayerPrefs();
    }

    public void SavePlayerDataToPlayerPrefs()
    {
        PlayerPrefs.SetString("PlayerData", JsonSerializationPlayerStats(LoadablePlayerStats));
    }
    
    public PlayerStats LoadPlayerDataFromPlayerPrefs()
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
