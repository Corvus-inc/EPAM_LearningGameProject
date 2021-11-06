using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public static bool ShootIsLocked { get; set;} 
    public WeaponType WeaponType { get; private set; }
    public int CountBulletInTheClip { get; set; }
    public int MaxBulletInTheClip{ get; private set; }
    public event Action IsEmptyClip;
    public event Action IsChangedClip;
    
    [SerializeField] private BaseWeapon gunPrefab;
    [SerializeField] private Transform poolBullet;
    [SerializeField] private Transform spawnWeapon;
    [SerializeField] private BaseBullet bulletPrefab;

    private BaseWeapon _gunCurrent;
    private List<BaseBullet> _listBullets;
    private int _indexBullet = 0;

    private void Awake()
    {
        _gunCurrent = Instantiate(gunPrefab, spawnWeapon);

        spawnWeapon.localScale = _gunCurrent.GetRateScale();
        //Need correctly write this calculated
        var localPosition = _gunCurrent.transform.localPosition;
        localPosition = new Vector3(
            localPosition.x - _gunCurrent.GetPointLocalPositionWeapon().x,
            localPosition.y - _gunCurrent.GetPointLocalPositionWeapon().y,
            localPosition.z - _gunCurrent.GetPointLocalPositionWeapon().z);
        _gunCurrent.transform.localPosition = localPosition;

        _gunCurrent.WeaponActive = true;

        _listBullets = CreateClip(bulletPrefab);
        MaxBulletInTheClip = _listBullets.Count;
    }

    private void Start()
    {
        IsChangedClip?.Invoke();
        //when delete this observe
        IsChangedClip += OnEmptyClip;
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0) || CountBulletInTheClip <= 0 || GameState.GameIsPaused || ShootIsLocked) return;

        NextIndexBullet();
        LetItFly(_indexBullet);
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

    public void StartDoubleDamage(float second)
    { 
        StopCoroutine(_gunCurrent.DoubleDamage(0));
        StartCoroutine(_gunCurrent.DoubleDamage(second));
    }
    public void StopDoubleDamage()
    {
        StopCoroutine(_gunCurrent.DoubleDamage(0));
    }

    private void LetItFly(int indexBullet)
    {
        if (!_gunCurrent.WeaponActive) return;
        //Add check on Null for List
        var bullet = _listBullets[indexBullet];

        if (bullet.IsFlying) return;
        _gunCurrent.AddBullet(bullet);
        ResetBulletToSpawn(bullet);
        _gunCurrent.Shoot();
        StartCoroutine(bullet.DeactivatingBullet(bullet.TimeLiveBullet));
    }

    private void OnEmptyClip()
    {
        if (CountBulletInTheClip != 0) return;
        IsEmptyClip?.Invoke();
        Debug.Log("The clip is empty");
    }

    private void  ResetBulletToSpawn(BaseBullet bullet)
    {
        bullet.transform.position = _gunCurrent.GetSpawnBulletPosition();
        bullet.transform.localRotation = _gunCurrent.GetSpawnBulletLocalRotation();
    }

    private List<BaseBullet> CreateClip(BaseBullet bullet)
    {
        var listBullets = new List<BaseBullet>(_gunCurrent.ClipCount);
        for (var i = 0; i < _gunCurrent.ClipCount; i++)
        {
            listBullets.Add(Instantiate(bullet, poolBullet)); 
        }
        return listBullets;
    }

    public void ReturnAllBulletToSpawn()
    {
        foreach (var bullet in _listBullets)
        {
            ResetBulletToSpawn(bullet);
            bullet.DeactivatingBullet();
        }
    }
    
    private void NextIndexBullet()
    {
        void DownClip()
        {
            CountBulletInTheClip--;
            IsChangedClip?.Invoke();
        }
        if (_indexBullet < gunPrefab.ClipCount - 1)
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
}