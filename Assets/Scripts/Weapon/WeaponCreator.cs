using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCreator : MonoBehaviour
{
    public List<WeaponHolder> InitStoreWeapons(List<GameObject> weaponPrefabs, Transform startedPosition)
    {
        var storePosition = new GameObject("StoreWeapons").transform;
        storePosition.position = Vector3.down;
        List<WeaponHolder> weapons = new List<WeaponHolder>();
        foreach (var weapon in weaponPrefabs)
        {
            var newWeapon = Instantiate(weapon, startedPosition).GetComponent<WeaponHolder>();
            weapons.Add(newWeapon);
            newWeapon.transform.SetParent(storePosition);
            newWeapon.gameObject.SetActive(false);
        }

        return weapons;
    }
}
