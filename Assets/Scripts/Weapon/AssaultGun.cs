using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultGun : BaseWeapon
{
    [SerializeField] private int assaultGunForce = 5;

    private void Start()
    {
        forceWeapon = assaultGunForce;
    }

    protected override int DamageToBullet(IBullet bullet)
    {
        bullet.AddBulletDamage(assaultGunForce);
        return bullet.GetBulletDamage();
    }

    private void AssaultGunDamageAddToBullet()
    {
         DamageToBullet(bullet);
    }

    public override void Shoot()
    {
        AssaultGunDamageAddToBullet();
        bullet.ActivatingBullet();
    }
    
}
