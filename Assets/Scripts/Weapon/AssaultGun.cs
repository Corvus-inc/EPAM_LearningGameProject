using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultGun : BaseWeapon
{
    [SerializeField] public int assaultGunRate = 5;

    public AssaultGun(bool active, BaseBullet bullet) : base(active:true) 
    {
        forceRate = assaultGunRate;
        weaponActive = active;
        this.bullet = bullet;
       
    }

    private int AssaultGunDamage(int bulletDamage)
    {
        return Damage(forceRate, bulletDamage);
    }

    public override void Shoot()
    {
       var t = AssaultGunDamage(bullet.bulletDamage);
        Debug.Log(t);
    }
}
