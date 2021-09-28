using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2 : MonoBehaviour
{
    [SerializeField] Transform _followTarget;
    [SerializeField] Vector3 _followOffset;
    
    [Range(0, 2)]
    [SerializeField]
    private float smoothTime;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
       var desiredPosition = _followTarget.position + _followOffset;
       transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
    }
}
