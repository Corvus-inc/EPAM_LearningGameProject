using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitWeapon : BaseWeapon
{
    public override void Shoot()
    {
        Bullet.ActivatingBullet();
    }
}
