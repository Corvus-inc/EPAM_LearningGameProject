using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBullet : MonoBehaviour, IBullet
{
    [SerializeField] public int bulletDamage;

    public virtual IEnumerator ActiveBullet(float timeLive)
    {
        throw new System.NotImplementedException();
    }
}
