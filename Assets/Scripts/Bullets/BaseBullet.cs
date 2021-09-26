using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBullet : MonoBehaviour, IBullet
{
    [SerializeField] public int bulletDamage;

    protected bool isFlying = false;

    public bool IsFlying => isFlying;

    public virtual IEnumerator ActiveBullet(float timeLive)
    {
        throw new System.NotImplementedException();
    }
}
