using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarEnemy : HealthBar
{

    private Camera _eventCamera;

    void Start()
    {
        _eventCamera = Camera.main;//how correctly make  ref
    }

    private void LateUpdate()
    {
        transform.LookAt(_eventCamera.transform);
        transform.Rotate(0, 180, 0);
    }
}
