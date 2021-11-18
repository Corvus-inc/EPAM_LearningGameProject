﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMutant : MonoBehaviour
{
    [Range(0,1000)] [SerializeField] private int startHealthEnemy;

    [SerializeField] private GameObject prefabWeapon;
    [SerializeField] private LayerMask layerBullet;
    [SerializeField] private HealthBarEnemy healthBarEnemy;
    
    private HealthSystem _healthSystem;
    private WeaponSystem _weaponSystem;
    private GameObject _currentWeapon;
    private Weapon _enemyWeapon;

    private void Awake()
    {
        var newWeapon = Instantiate(prefabWeapon, transform).GetComponent<Weapon>();
        _weaponSystem = new WeaponSystem(newWeapon.gameObject, transform, 1000);
        _currentWeapon = newWeapon.gameObject;
        _enemyWeapon = _currentWeapon.GetComponent<Weapon>();
        
        
        _healthSystem = new HealthSystem(startHealthEnemy);
        _healthSystem.OnHealthStateMin += EnemyDie;
        healthBarEnemy.HealthSystem = _healthSystem;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        bool check = GameUtils.Utils.CheckLayerMask(collision.gameObject, layerBullet);
        
        if (check)
        {
            IBullet bullet = collision.gameObject.GetComponent<IBullet>();
            int damage = bullet.GetBulletDamage();
            _healthSystem.Damage(damage);
            
            StopCoroutine(bullet.DeactivatingBullet(0));
            bullet.DeactivatingBullet();
        }    
    }

    public void Attack(Transform targetPosition)
    {
        _enemyWeapon.AimLookAt(targetPosition.position + Vector3.up*5);
        _enemyWeapon.UsageWeapon();
    }

    private void EnemyDie(object sender, EventArgs e)
    {
        _healthSystem.OnHealthStateMin -= EnemyDie;
        Destroy(gameObject);    
    }
}