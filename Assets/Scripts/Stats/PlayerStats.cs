using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class PlayerStats
{
    public int Heals { get; set; }
    public float Speed{ get; set; }
    public float TimeBoostSpeed { get; set; }
    public float BoostSpeedRate { get; set; }
    public int Experience { get; set; }
    public int Level { get; set; }
}
