using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] private int clipCount;
    [SerializeField] protected int forceWeapon;
    private bool _weaponActive;

    
    protected IBullet bullet;

    public int ClipCount => clipCount;
    public int ForceWeapon => forceWeapon;

    public bool WeaponActive 
    {
        get { return _weaponActive;}
        set {_weaponActive = value;}
    } 

    public void AddBullet(IBullet bullet)
    {
        this.bullet = bullet;
    }
    protected virtual int DamageToBullet(IBullet bullet)
    {
        bullet.AddBulletDamage(forceWeapon);
        return bullet.GetBulletDamage();
    }

    public abstract void Shoot();

}

