using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitBullet : BaseBullet
{
    private void FixedUpdate()
    {
        if (_isFlying)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * _speedBullet);
        }
    }
}
