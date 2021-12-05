using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHolder : MonoBehaviour
{
    public RawImage CurrentIcon { get; private set; }
    public BaseWeapon GunCurrent { get; private set; }

    
    [SerializeField] private BaseWeapon gunPrefab;
    [SerializeField] private Transform spawnWeapon;


    private void Awake()
    {
        GunCurrent = Instantiate(gunPrefab, spawnWeapon);

        spawnWeapon.localScale = GunCurrent.GetRateScale();
        //Need correctly write this calculated
        var localPosition = GunCurrent.transform.localPosition;
        localPosition = new Vector3(
            localPosition.x - GunCurrent.GetPointLocalPositionWeapon().x,
            localPosition.y - GunCurrent.GetPointLocalPositionWeapon().y,
            localPosition.z - GunCurrent.GetPointLocalPositionWeapon().z);
        GunCurrent.transform.localPosition = localPosition;

        GunCurrent.WeaponActive = true;

        CurrentIcon = GunCurrent.Icon;
    }
    
    public void AimLookAt(Vector3 target)
    {
        GunCurrent.SpawnBullet.LookAt(target);
    }
}