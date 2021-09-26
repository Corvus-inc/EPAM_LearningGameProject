using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2 : MonoBehaviour
{
    [SerializeField] Transform followTarget;
    [SerializeField] Vector3 followOffset;
    
    [Range(0, 2)]
    public float smoothTime;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
       var desiredPosition = followTarget.position + followOffset;
       transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
    }
}
