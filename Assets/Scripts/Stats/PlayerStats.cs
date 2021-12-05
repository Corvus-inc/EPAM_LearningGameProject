using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStats : IPlayerStats, IStats
{
    public int Health{ get; set; }
    public float Speed{ get; set; }
    public int MAXHealth{ get; set; }
    public int CountBullets{ get; set; }
    public int StartedWeapon{ get; set; }
    public float BoostSpeedRate{ get; set; }
    public float[] PlayerPosition{ get; set; }
    public WeaponSavingStats[] WeaponSavingStatsArray { get; set; }

    public PlayerStats()
    {
    }

    public PlayerStats(PlayerStats dataPlayerStats)
    {
        Speed = dataPlayerStats.Speed;
        Health = dataPlayerStats.Health;
        MAXHealth = dataPlayerStats.MAXHealth;
        CountBullets = dataPlayerStats.CountBullets;
        StartedWeapon = dataPlayerStats.StartedWeapon;
        PlayerPosition = dataPlayerStats.PlayerPosition;
        BoostSpeedRate = dataPlayerStats.BoostSpeedRate;
        WeaponSavingStatsArray = dataPlayerStats.WeaponSavingStatsArray;
    }
}