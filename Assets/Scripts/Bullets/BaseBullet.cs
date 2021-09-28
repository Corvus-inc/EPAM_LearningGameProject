using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBullet : MonoBehaviour, IBullet
{
    [SerializeField] protected int _bulletDamage;
    [SerializeField] protected float _timeLiveBullet;
    [SerializeField] protected float bulletSpeed = 300f;
    [SerializeField] private LayerMask layerMask;
    
    protected bool _isFlying = false;

    public float TimeLiveBullet => _timeLiveBullet;
    public bool IsFlying => _isFlying;

    public abstract void ActivatingBullet();
    public abstract IEnumerator DeactivatingBullet(float timeLive);

    public int GetBulletDamage()
    {
        return _bulletDamage;
    }

    public void AddBulletDamage(int damage)
    {
        _bulletDamage += damage;
    }

    public void ReduceDamage(int damage)
    {
        _bulletDamage -= damage;
    }
}
