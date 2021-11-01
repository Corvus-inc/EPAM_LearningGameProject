﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBullet : MonoBehaviour, IBullet
{
    public float TimeLiveBullet => _lifeTimeBullet;
    public bool IsFlying => _isFlying;
    public event Action IsActiveBullet;
    
    [SerializeField] protected int _bulletDamage;
    [SerializeField] protected float _lifeTimeBullet;
    [SerializeField] protected float _speedBullet = 300f;
    
    protected bool _isFlying = false;

    private int _startBulletDamage;
    private Transform saveParent;
    
    private void Awake()
    {
        saveParent = transform.parent;
        ApplyStartBulletDamage();
    }
    
    public virtual void ActivatingBullet()
    {
        gameObject.SetActive(true);
        IsActiveBullet?.Invoke();
        transform.parent = null;
        _isFlying = true;
    }
    
    public virtual IEnumerator DeactivatingBullet(float timeLive)
    {
        yield return new WaitForSeconds(timeLive);
            DeactivatingBullet();
    }

    public virtual void DeactivatingBullet()
    {
        transform.SetParent(saveParent);
        gameObject.SetActive(false);
        _isFlying = false;
    }

    public virtual void AddBulletDamage(int damage)
    {
        _bulletDamage += damage;
    }

    public int GetBulletDamage()
    {
        return _bulletDamage;
    }

    public void ReduceDamage()
    {
        _bulletDamage = _startBulletDamage;
    }

    private void ApplyStartBulletDamage()
    {
        if (!gameObject)
        {
            gameObject.SetActive(true);
            _startBulletDamage = _bulletDamage;
            gameObject.SetActive(false);
        }
        else _startBulletDamage = _bulletDamage;
    }
}
