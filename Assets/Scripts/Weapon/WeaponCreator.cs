using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCreator : MonoBehaviour
{
    public List<Weapon> InitStoreWeapons(List<GameObject> weaponPrefabs, Transform startedPosition)
    {
        var storePosition = new GameObject("StoreWeapons").transform;
        storePosition.position = Vector3.down;
        List<Weapon> weapons = new List<Weapon>();
        foreach (var weapon in weaponPrefabs)
        {
            var newWeapon = Instantiate(weapon, startedPosition).GetComponent<Weapon>();
            weapons.Add(newWeapon);
            newWeapon.transform.SetParent(storePosition);
            newWeapon.gameObject.SetActive(false);
        }

        return weapons;
    }
}
