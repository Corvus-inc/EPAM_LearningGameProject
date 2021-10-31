using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PartBullet : ShotGunBullet
{
    public Vector3 direction;
    private void FixedUpdate()
    {
        transform.Translate(direction * (Time.deltaTime * _speedBullet));
    }
}
