using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultGun : BaseWeapon, IWeapon
{
    protected override int DamageToBullet(IBullet bullet)
    {
        bullet.AddBulletDamage(forceWeapon);
        return bullet.GetBulletDamage();
    }

    private void DamageAddToBullet()
    {
       var t = DamageToBullet(bullet);
    }

    public override void Shoot()
    {
        DamageAddToBullet();
        bullet.ActivatingBullet();
    }
}
