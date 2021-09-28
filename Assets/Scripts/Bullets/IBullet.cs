using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet
{
    float TimeLiveBullet { get;}
    
    int GetBulletDamage();
    void ReduceDamage(int damage);
    void AddBulletDamage(int damage);
    void ActivatingBullet();
    IEnumerator DeactivatingBullet(float timeLive);
}