using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] BaseWeapon _gun;
    [SerializeField] BaseBullet _bullet;
    [SerializeField] Transform _poolBullet;
    [SerializeField] Transform _spawnBullet;
    [SerializeField] float _timeLiveBullet;

    private List<BaseBullet> listBullets;
    private int indexBullet = 0;

    private void Start()
    {
        _gun.weaponActive = true;
        listBullets = CreateClip(_bullet);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (indexBullet < _gun.clipCount-1) indexBullet++;
            else indexBullet = 0;
            LetItFly(indexBullet);
        }
    }

    private IEnumerator PutBulletOnSpawn(float secToSpawn, int indexBullet)
    {
        yield return new WaitForSeconds(secToSpawn);
        
        listBullets[indexBullet].ReduceDamage(_gun.ForceRate);
        listBullets[indexBullet].transform.position = _spawnBullet.transform.position;
        listBullets[indexBullet].transform.rotation = _spawnBullet.transform.rotation;
        listBullets[indexBullet].gameObject.SetActive(false);
    }


    private void LetItFly(int indexBullet)
    {
        if (_gun.weaponActive)
        {
            if (!listBullets[indexBullet].IsFlying)
            {
                listBullets[indexBullet].AddBulletDamage(_gun.ForceRate);
                StartCoroutine(listBullets[indexBullet].ActiveBullet(_timeLiveBullet));
                StartCoroutine(PutBulletOnSpawn(_timeLiveBullet, indexBullet));
            }
        }
    }

    private List<BaseBullet> CreateClip(BaseBullet bullet)
    {
        List<BaseBullet> listBullets = new List<BaseBullet>(_gun.clipCount);
        for (int i = 0; i < _gun.clipCount; i++)
        {
            listBullets.Add(Instantiate(bullet, _spawnBullet.transform.position, _spawnBullet.transform.rotation, _poolBullet)); 
        }

        return listBullets;
    }
}
