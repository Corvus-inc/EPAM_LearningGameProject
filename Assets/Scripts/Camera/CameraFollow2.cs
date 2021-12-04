using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2 : MonoBehaviour, ICameraFollow
{
    public Transform FollowTarget {  get; set; }
    
    [Range(0, 1)]
    private float _smoothTime;
    private Vector3 _followOffset;
    private Vector3 _velocity = Vector3.zero;
    private Transform _followTarget;

    private void Awake()
    {
        _smoothTime = 0.39f;
        _followOffset = new Vector3(0,26,-9);
        _velocity = Vector3.zero;
        _followTarget = FollowTarget;
    }

    void LateUpdate()
    {
       var desiredPosition = _followTarget.position + _followOffset;
       transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref _velocity, _smoothTime);
    }
}
