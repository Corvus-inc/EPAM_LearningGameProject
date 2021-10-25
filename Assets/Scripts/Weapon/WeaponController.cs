using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameState GameState { private get; set; } 
    
    [SerializeField] private BaseWeapon gunPrefab;
    [SerializeField] private BaseBullet bulletPrefab;
    [SerializeField] private Transform poolBullet;
    [SerializeField] private Transform spawnWeapon;

    private BaseWeapon _gunCurrent;
    private List<BaseBullet> _listBullets;
    private int _indexBullet = 0;

    public int bulletCountInTheClip;

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
        bulletCountInTheClip = _listBullets.Count;
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0) || bulletCountInTheClip <= 0 || GameState.GameIsPaused) return;
        if (_indexBullet < gunPrefab.ClipCount - 1)
        {
            bulletCountInTheClip--;
            _indexBullet++;
        }
        else _indexBullet = 0;
        LetItFly(_indexBullet);
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

    private void  ResetBulletToSpawn(BaseBullet bullet)
    {
        bullet.ReduceDamage();
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
}
