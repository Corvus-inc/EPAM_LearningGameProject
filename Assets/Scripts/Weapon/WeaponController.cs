using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private BaseWeapon _gun;
    [SerializeField] private BaseBullet _bullet;
    [SerializeField] private Transform _poolBullet;
    [SerializeField] private Transform _spawnBullet;

    private List<BaseBullet> _listBullets;
    private int _indexBullet = 0;

    private void Awake()
    {
        _gun.WeaponActive = true;
        _listBullets = CreateClip(_bullet);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_indexBullet < _gun.ClipCount-1) _indexBullet++;
            else _indexBullet = 0;
            LetItFly(_indexBullet);
        }
    }

    private void LetItFly(int indexBullet)
    {
        if (_gun.WeaponActive)
        {
            //Add check on Null for List
            BaseBullet bullet = _listBullets[indexBullet];
            
            if (!bullet.IsFlying)
            {   
                _gun.AddBullet(bullet);
                _gun.Shoot();
                StartCoroutine(bullet.DeactivatingBullet(bullet.TimeLiveBullet));
                StartCoroutine(ResetBulletToSpawn(bullet));
            }
        }
    }

    private IEnumerator ResetBulletToSpawn(BaseBullet bullet)
    { 
        yield return new WaitForSeconds(bullet.TimeLiveBullet);
       
        bullet.ReduceDamage(_gun.ForceWeapon);
        bullet.transform.position = _spawnBullet.transform.position;
        bullet.transform.rotation = _spawnBullet.transform.rotation;
        bullet.gameObject.SetActive(false);
    }

    private List<BaseBullet> CreateClip(BaseBullet bullet)
    {
        List<BaseBullet> listBullets = new List<BaseBullet>(_gun.ClipCount);
        for (int i = 0; i < _gun.ClipCount; i++)
        {
            listBullets.Add(Instantiate(bullet, _spawnBullet.transform.position, _spawnBullet.transform.rotation, _poolBullet)); 
        }

        return listBullets;
    }
}
