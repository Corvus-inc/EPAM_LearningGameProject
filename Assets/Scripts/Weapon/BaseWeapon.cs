using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour, IWeapon
{   
    public int ClipCount => clipCount;
    public int ForceWeapon => forceWeapon;
    public bool WeaponActive { get; set; }
    
    [SerializeField] protected int clipCount;
    [SerializeField] protected int forceWeapon;

    [SerializeField] private float _rateScale;
    [SerializeField] private WeaponType _weaponType;
    [SerializeField] private Transform _spawnBullet;
    [SerializeField] private Transform _pointPositionWeapon;

    protected IBullet bullet;

    public void AddBullet(IBullet bullet)
    {
        this.bullet = bullet;
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

    protected void DamageToBullet(IBullet bullet)
    {
        bullet.AddBulletDamage(forceWeapon);
    }

    public IEnumerator DoubleDamage (float second)
    {
        var saveForce = forceWeapon;
        forceWeapon *= 2;
        yield return new WaitForSeconds(second);
        forceWeapon = saveForce;

    }
}

