using System.Collections;
using System.Collections.Generic;
using Sounds;
using UnityEngine;

public class AssaultGun : BaseWeapon, IWeapon
{
    public override void Shoot()
    {
        SoundManager.PlaySound(Sound.AssaultShoot);
        DamageToBullet(Bullet);
        Bullet.ActivatingBullet();
    }
}
