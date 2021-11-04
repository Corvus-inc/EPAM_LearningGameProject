using System;
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

    protected int _startBulletDamage;
    protected Transform saveParent;
    
    private void Awake()
    {
        saveParent = transform.parent;
        ApplyStartBulletDamage();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public virtual void ActivatingBullet()
    {
        gameObject.SetActive(true);
        OnActiveBullet();
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
        ReduceDamage();
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

    protected void OnActiveBullet()
    {
        IsActiveBullet?.Invoke();
    }
    protected void ApplyStartBulletDamage()
    {
         _startBulletDamage = _bulletDamage;
    }
}
