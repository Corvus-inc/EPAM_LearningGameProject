using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultGun : BaseWeapon, IWeapon
{
    public override void Shoot()
    {
        DamageToBullet(Bullet);
        Bullet.ActivatingBullet();
    }
}
