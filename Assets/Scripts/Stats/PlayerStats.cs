using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStats
{
    public int heath;
    public int maxHeath;
    public float speed;
    public float boostSpeedRate;
    public int countBullets;
    public Transform playerPosition;
}
