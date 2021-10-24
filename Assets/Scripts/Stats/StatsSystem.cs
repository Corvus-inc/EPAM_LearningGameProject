using UnityEngine;

public class StatsSystem
{
    private int _health;
    private int _damage;
    private float _speed;
    private float _timeBoostSpeed;
    private float _boostSpeedRate;

    public int Health => _health;
    public int Damage => _damage;
    public float Speed => _speed;
    public float TimeBoostSpeedState => _damage;
    public float BoostSpeedRate => _boostSpeedRate; 
    
    private PlayerStats _playerStats;

    public StatsSystem(int health, int damage, float speed, float timeBoostSpeed)
    {
        _health = health;
        _damage = damage;
        _speed = speed;
        _timeBoostSpeed = timeBoostSpeed;
    }

    public StatsSystem(PlayerStats playerStats)
    {
        _health = playerStats.Heals;
        _speed = playerStats.Speed;
        _boostSpeedRate = playerStats.BoostSpeedRate;
        _timeBoostSpeed = playerStats.TimeBoostSpeed;
    }

    public void UpdatePlayerStats(PlayerStats stats)
    {
        _health = stats.Heals;
        _speed = stats.Speed;
        _timeBoostSpeed = stats.TimeBoostSpeed;
    }

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
