using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IBullet
{
    IEnumerator ActiveBullet(float timeLive);
    void MakeFinalDamage();
    void AddBulletDamage(int damage);
    void ReduceDamage(int damage);
}