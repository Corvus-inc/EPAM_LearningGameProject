using System.Collections;
using System.Collections.Generic;
using Sounds;
using UnityEngine;

public class ShotGun : BaseWeapon
{
    public override void Shoot()
    {
        SoundManager.PlaySound(Sound.ShotgunShoot);
        DamageToBullet(Bullet);
        Bullet.ActivatingBullet();
    }
}
