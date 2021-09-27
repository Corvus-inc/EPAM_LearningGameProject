using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBullet : MonoBehaviour, IBullet
{
    [SerializeField] public int bulletDamage;
    [SerializeField] private LayerMask layerMask;

    protected bool isFlying = false;

    public bool IsFlying => isFlying;

    public virtual IEnumerator ActiveBullet(float timeLive)
    {
        throw new System.NotImplementedException();
    }

    public virtual void MakeFinalDamage()
    {
        throw new System.NotImplementedException();
    }

    public void AddBulletDamage(int damage)
    {
        bulletDamage += damage;
    }

    public void ReduceDamage(int damage)
    {
        bulletDamage -= damage;
    }
}
