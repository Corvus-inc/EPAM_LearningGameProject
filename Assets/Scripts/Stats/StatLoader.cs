using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatLoader
{
    public void SavePlayerDataToPlayerPrefs(PlayerStats stats)
    {
        PlayerPrefs.SetString("PlayerData", JsonSerializationPlayerStats(stats));
    }
    
    public PlayerStats LoadPlayerDataFromPlayerPrefs()
    {
        return JsonDeserializationPlayerStats(PlayerPrefs.GetString("PlayerData"));
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
