using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour, IWeapon
{   
    [SerializeField] protected int clipCount;
    [SerializeField] protected int forceWeapon;

    [SerializeField] private float _rateScale;
    [SerializeField] private Transform _spawnBullet;
    [SerializeField] private Transform _pointPositionWeapon;
   
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


    public Vector3 GetSpawnBulletPosition()
    {
        return _spawnBullet.position;
    }

    public Quaternion GetSpawnBulletLocalRotation()
    {
        return _spawnBullet.localRotation;
    }
    
    public Vector3 GetPointLocalPositionWeapon()
    {
        return _pointPositionWeapon.localPosition;
    }

    public Vector3 GetRateScale()
    {
        return new Vector3(_rateScale, _rateScale, _rateScale);
    }
}

