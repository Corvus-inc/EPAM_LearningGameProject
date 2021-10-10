using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private BaseWeapon _gunPrefab;
    [SerializeField] private BaseBullet _bulletPrefab;
    [SerializeField] private Transform _poolBullet;
    [SerializeField] private Transform _spawnWeapon;

    private BaseWeapon _gunCurrent;
    private List<BaseBullet> _listBullets;
    private int _indexBullet = 0;

    public int BulletCountInTheClip;

    private void Awake()
    {
        _gunCurrent = Instantiate(_gunPrefab, _spawnWeapon);

        _spawnWeapon.localScale = _gunCurrent.GetRateScale();
        //Need correctly write this calculated
        _gunCurrent.transform.localPosition = new Vector3(
            _gunCurrent.transform.localPosition.x - _gunCurrent.GetPointLocalPositionWeapon().x,
            _gunCurrent.transform.localPosition.y - _gunCurrent.GetPointLocalPositionWeapon().y,
            _gunCurrent.transform.localPosition.z - _gunCurrent.GetPointLocalPositionWeapon().z);

        _gunCurrent.WeaponActive = true;

        _listBullets = CreateClip(_bulletPrefab);
        BulletCountInTheClip = _listBullets.Count;
    }

    void  FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0)&& BulletCountInTheClip > 0)
        {
            if (_indexBullet < _gunPrefab.ClipCount - 1)
            {
                BulletCountInTheClip--;
                _indexBullet++;
            }
            else _indexBullet = 0;
            LetItFly(_indexBullet);
        }
    }

    private void LetItFly(int indexBullet)
    {
        if (_gunCurrent.WeaponActive)
        {
            //Add check on Null for List
            BaseBullet bullet = _listBullets[indexBullet];
            
            if (!bullet.IsFlying)
            {   
                _gunCurrent.AddBullet(bullet);
                ResetBulletToSpawn(bullet);
                _gunCurrent.Shoot();
                StartCoroutine(bullet.DeactivatingBullet(bullet.TimeLiveBullet));
            }
        }
    }

    private void  ResetBulletToSpawn(BaseBullet bullet)
    {
        bullet.ReduceDamage();
        bullet.transform.position = _gunCurrent.GetSpawnBulletPosition();
        bullet.transform.localRotation = _gunCurrent.GetSpawnBulletLocalRotation();
    }

    private List<BaseBullet> CreateClip(BaseBullet bullet)
    {
        List<BaseBullet> listBullets = new List<BaseBullet>(_gunCurrent.ClipCount);
        for (int i = 0; i < _gunCurrent.ClipCount; i++)
        {
            listBullets.Add(Instantiate(bullet, _poolBullet)); 
        }

        return listBullets;
    }
}
