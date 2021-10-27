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
    public Vector3 playerPosition;
}
