using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStats
{
    public int health;
    public int maxHealth;
    public float speed;
    public float boostSpeedRate;
    public int countBullets;
    public float[] playerPosition;
    public int[] countClip;

    public PlayerStats()
    {
    }

    public PlayerStats(PlayerStats dataPlayerStats)
    {
        health = dataPlayerStats.health;
        maxHealth = dataPlayerStats.maxHealth;
        speed = dataPlayerStats.speed;
        boostSpeedRate = dataPlayerStats.boostSpeedRate;
        countBullets = dataPlayerStats.countBullets;
        playerPosition = dataPlayerStats.playerPosition;
        countClip = dataPlayerStats.countClip;
    }
}
