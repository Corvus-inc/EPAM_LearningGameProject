using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    [Range(0,10)] [SerializeField] private float _cameraSpeedRotate;   

    void Update()
    {   

        transform.Rotate(new Vector3(0, Time.deltaTime*_cameraSpeedRotate, 0));
    }
}
