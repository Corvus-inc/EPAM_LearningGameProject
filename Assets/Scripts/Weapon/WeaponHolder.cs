using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHolder : MonoBehaviour
{
    public RawImage CurrentIcon { get; private set; }

    
    [SerializeField] private BaseWeapon gunPrefab;
    [SerializeField] private Transform spawnWeapon;

    public BaseWeapon _gunCurrent { get; private set; }

    private void Awake()
    {
        _gunCurrent = Instantiate(gunPrefab, spawnWeapon);

        spawnWeapon.localScale = _gunCurrent.GetRateScale();
        //Need correctly write this calculated
        var localPosition = _gunCurrent.transform.localPosition;
        localPosition = new Vector3(
            localPosition.x - _gunCurrent.GetPointLocalPositionWeapon().x,
            localPosition.y - _gunCurrent.GetPointLocalPositionWeapon().y,
            localPosition.z - _gunCurrent.GetPointLocalPositionWeapon().z);
        _gunCurrent.transform.localPosition = localPosition;

        _gunCurrent.WeaponActive = true;

        CurrentIcon = _gunCurrent.Icon;
    }
    
    public void AimLookAt(Vector3 target)
    {
        _gunCurrent.SpawnBullet.LookAt(target);
    }
}