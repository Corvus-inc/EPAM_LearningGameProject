using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarEnemy : HealthBar 
{

    private Camera _eventCamera => Camera.main;

    public void FollowTo(Transform target, Vector3 offset)
    {
        transform.position = target.position + offset;
    }
    private void LateUpdate()
    {
        transform.LookAt(_eventCamera.transform);
        transform.Rotate(0, 180, 0);
    }
}
