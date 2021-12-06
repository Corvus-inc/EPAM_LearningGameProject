using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : BaseWeapon
{
    public override void Shoot()
    {
        DamageToBullet(Bullet);
        Bullet.ActivatingBullet();
    }
}
