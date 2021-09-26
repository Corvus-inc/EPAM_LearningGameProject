using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IBullet
{
    IEnumerator ActiveBullet(float timeLive);
}