using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour, IWeapon
{
    public int clipCount;

    protected int forceRate;
    public bool weaponActive;
    protected BaseBullet bullet;

    public int ForceRate => forceRate;
    public bool WeaponActive => weaponActive; 

    //protected BaseWeapon(bool active)
    //{
    //    weaponActive = active;
    //}


    protected virtual int Damage(int forceRate, int forceBullet)
    {
        return forceRate + forceBullet;
    }

    public abstract void Shoot();

}

