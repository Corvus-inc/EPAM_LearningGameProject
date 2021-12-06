using System;
using System.Collections;
using System.Collections.Generic;
using LoaderSystem;
using UnityEngine;

public class CameraFollow : MonoBehaviour, ICameraFollow
{
    public Transform FollowTarget { get; set; }
    
    private Transform _followTarget;
    [SerializeField] private Vector3 _followOffset = new Vector3(0,26,-9);

    [SerializeField] private Transform _lookAtTarget;
    [SerializeField] private Vector3 _lookAtOffset;

    [Range(0, 10)]
    [SerializeField]
    private float _followSpeed;

    private ICameraFollow _cameraFollowImplementation;

    private void Awake()
    {
        _followTarget = FollowTarget;
    }

    void LateUpdate()
    {
        if (_followTarget != null)
        {
            var desiredPosition = _followTarget.position + _followOffset;
            var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _followSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }

        if (_lookAtTarget != null)   transform.LookAt(_lookAtTarget.position + _lookAtOffset);
    }
}