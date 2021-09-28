using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _followTarget;
    [SerializeField] private Vector3 _followOffset;

    [SerializeField] private Transform _lookAtTarget;
    [SerializeField] private Vector3 _lookAtOffset;

    [Range(0, 10)]
    [SerializeField]
    private float _followSpeed;

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
