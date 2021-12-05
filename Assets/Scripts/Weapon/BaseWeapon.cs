using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public abstract class BaseWeapon : MonoBehaviour, IWeapon
{   
    public static bool ShootIsLocked { get; set;} 
    
    public int CountBulletInTheClip { get; set; }
    public bool WeaponActive { get; set; }
    public int MaxBulletInTheClip{ get; private set; }
    
    public RawImage Icon => icon;
    public Transform SpawnBullet => spawnBullet;
    
    public event Action IsChangedClip;
    public event Action IsEmptyClip;
    
    protected IBullet Bullet;
    
    [SerializeField] protected RawImage icon;
    [SerializeField] protected int clipCount;
    [SerializeField] protected int forceWeapon;
    
    [SerializeField] private Transform poolBullet; 
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private BaseBullet bulletPrefab;
    [FormerlySerializedAs("_rateScale")] [SerializeField] private float scaleValue;
    [FormerlySerializedAs("_spawnBullet")] [SerializeField] private Transform spawnBullet;
    [FormerlySerializedAs("_pointPositionWeapon")] [SerializeField] private Transform pointPositionWeapon;
    
    private int _indexBullet;
    private List<BaseBullet> _listBullets;
    
    private void Awake()
    {
        _listBullets = CreateClip(bulletPrefab);
        MaxBulletInTheClip = _listBullets.Count;
    }
    private void Start()
    {
        IsChangedClip?.Invoke();
        //where delete this observe
        IsChangedClip += OnEmptyClip;
    }
    public abstract void Shoot();
    
    public void UsageWeapon()
    {
        if (ShootIsLocked || CountBulletInTheClip <= 0 ) return;
        NextIndexBullet();
        LetItFly(_indexBullet);
    }
    
    public Vector3 GetPointLocalPositionWeapon()
    {
        return pointPositionWeapon.localPosition;
    }

    public Vector3 GetRateScale()
    {
        return new Vector3(scaleValue, scaleValue, scaleValue);
    }
    
    public void StartDoubleDamage(float second)
    { 
        StopCoroutine(DoubleDamage(0));
        StartCoroutine(DoubleDamage(second));
    }
    
    protected void DamageToBullet(IBullet bullet)
    {
        bullet.AddBulletDamage(forceWeapon);
    }

    private void LetItFly(int indexBullet)
    {
        if (!WeaponActive) return;
        //Add check on Null for List
        var bullet = _listBullets[indexBullet];

        if (bullet.IsFlying) return;
        AddBullet(bullet);
        ResetBulletToSpawn(bullet);
        Shoot();
        StartCoroutine(bullet.DeactivatingBullet(bullet.TimeLiveBullet));
    }
    
    private void NextIndexBullet()
    {
        void DownClip()
        {
            CountBulletInTheClip--;
            IsChangedClip?.Invoke();
        }
        if (_indexBullet < clipCount - 1)
        {
            DownClip();
            _indexBullet++;
        }
        else
        {
            DownClip();
            _indexBullet = 0;
        }
    }
    
    private void  ResetBulletToSpawn(BaseBullet bullet)
    {
        bullet.transform.position = GetSpawnBulletPosition();
        bullet.transform.localRotation = GetSpawnBulletLocalRotation();
    }
    
    private void OnEmptyClip()
    {
        if (CountBulletInTheClip != 0) return;
        IsEmptyClip?.Invoke();
        Debug.Log("The clip is empty");
    }

    public int Recharge(int bullets)
    {
        int remains;
        if (bullets != 0)
        {
            var countAddBullets = MaxBulletInTheClip - CountBulletInTheClip;
            CountBulletInTheClip += bullets <= countAddBullets ? bullets : countAddBullets;
            if (CountBulletInTheClip > MaxBulletInTheClip)
            {
                CountBulletInTheClip = MaxBulletInTheClip;
            }

            IsChangedClip?.Invoke();
            remains = bullets >= countAddBullets ? bullets - countAddBullets : 0;
        }
        else remains = 0;
        return remains;
    }

    public void ReturnAllBulletToSpawn()
    {
        foreach (var bullet in _listBullets)
        {
            ResetBulletToSpawn(bullet);
            bullet.DeactivatingBullet();
        }
    }
    
      
    private List<BaseBullet> CreateClip(BaseBullet bullet)
    {
        var listBullets = new List<BaseBullet>(clipCount);
        for (var i = 0; i < clipCount; i++)
        {
            listBullets.Add(Instantiate(bullet, poolBullet)); 
        }
        return listBullets;
    }

    private void AddBullet(IBullet bullet)
    {
        this.Bullet = bullet;
    }
    
    private IEnumerator DoubleDamage (float second)
    {
        var saveForce = forceWeapon;
        forceWeapon *= 2;
        yield return new WaitForSeconds(second);
        forceWeapon = saveForce;
    }

    private Vector3 GetSpawnBulletPosition()
    {
        return spawnBullet.position;
    }

    private Quaternion GetSpawnBulletLocalRotation()
    {
        return spawnBullet.localRotation;
    }

}

