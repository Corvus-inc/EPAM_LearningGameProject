using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] BaseWeapon _gun;
    [SerializeField] BaseBullet _bullet;
    [SerializeField] Transform _spawnBullet;
    [SerializeField] float _timeLiveBullet;

    private void Start()
    {
        _gun.weaponActive = true;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (_gun.weaponActive)
            {
                StartCoroutine(_bullet.ActiveBullet(_timeLiveBullet));
                StartCoroutine(PutBulletOnSpawn(_timeLiveBullet));

            }
        }
    }

    private IEnumerator PutBulletOnSpawn(float secToSpawn)
    {
        yield return new WaitForSeconds(secToSpawn);
        _bullet.transform.position = _spawnBullet.transform.position;
        _bullet.transform.rotation = _spawnBullet.transform.rotation;
    }
}
