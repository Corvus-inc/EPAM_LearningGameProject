using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseWeapon : MonoBehaviour, IWeapon
{   
    public WeaponType WeaponType { get; private set; }
    public int CountBulletInTheClip { get; set; }
    public static bool ShootIsLocked { get; set;} 
    public bool WeaponActive { get; set; }
    
    public int MaxBulletInTheClip{ get; private set; }
    
    public int ClipCount => clipCount;
    public int ForceWeapon => forceWeapon;
    public RawImage Icon => icon;
    public Transform SpawnBullet => _spawnBullet;
    
    public event Action IsChangedClip;
    public event Action IsEmptyClip;
    
    
    [SerializeField] protected RawImage icon;
    [SerializeField] protected int clipCount;
    [SerializeField] private float _rateScale;
    [SerializeField] protected int forceWeapon;
    [SerializeField] private Transform poolBullet;
    [SerializeField] private WeaponType _weaponType;
    [SerializeField] private Transform _spawnBullet;
    [SerializeField] private BaseBullet bulletPrefab;
    [SerializeField] private Transform _pointPositionWeapon;
    
    protected IBullet bullet;
    
    
    private int _indexBullet = 0;
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


    public void AddBullet(IBullet bullet)
    {
        this.bullet = bullet;
    }

    public abstract void Shoot();
    
    public void UsageWeapon()
    {
        if (ShootIsLocked || CountBulletInTheClip <= 0 ) return;
        NextIndexBullet();
        LetItFly(_indexBullet);
    }

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
    
    public void StartDoubleDamage(float second)
    { 
        StopCoroutine(DoubleDamage(0));
        StartCoroutine(DoubleDamage(second));
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
        if (_indexBullet < ClipCount - 1)
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
        var listBullets = new List<BaseBullet>(ClipCount);
        for (var i = 0; i < ClipCount; i++)
        {
            listBullets.Add(Instantiate(bullet, poolBullet)); 
        }
        return listBullets;
    }
}

