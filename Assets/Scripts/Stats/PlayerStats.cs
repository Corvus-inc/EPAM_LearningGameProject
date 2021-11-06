using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStats
{
    public int Health{ get; set; }
    public int MAXHealth{ get; set; }
    public float Speed{ get; set; }
    public float BoostSpeedRate{ get; set; }
    public int CountBullets{ get; set; }
    public float[] PlayerPosition{ get; set; }
    public int StartedWeapon{ get; set; }
    public WeaponSavingStats[] WeaponSavingStatsArray { get; set; }

    public PlayerStats()
    {
    }

    public PlayerStats(PlayerStats dataPlayerStats)
    {
        Health = dataPlayerStats.Health;
        MAXHealth = dataPlayerStats.MAXHealth;
        Speed = dataPlayerStats.Speed;
        BoostSpeedRate = dataPlayerStats.BoostSpeedRate;
        CountBullets = dataPlayerStats.CountBullets;
        PlayerPosition = dataPlayerStats.PlayerPosition;
        StartedWeapon = dataPlayerStats.StartedWeapon;
        WeaponSavingStatsArray = dataPlayerStats.WeaponSavingStatsArray;
    }
}
