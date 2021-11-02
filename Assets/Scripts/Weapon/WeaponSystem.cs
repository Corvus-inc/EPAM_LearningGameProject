using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class WeaponSystem 
{
    private List<GameObject> _listWeapons;
    private  Transform _transformTo;
    private GameObject _gunEquipped ;
    private int _userCountBullets;//ref?;
    private Weapon _weapon;
    private UIPlayer _ui;

    public WeaponSystem(List<GameObject> listWeapons, Transform transformTo, UIPlayer UI, int userCountBullets)
    {
        _ui = UI;
        _listWeapons = listWeapons;
        _transformTo = transformTo;
        _gunEquipped = listWeapons[1];
        _userCountBullets = userCountBullets;
        _weapon = _gunEquipped.gameObject.GetComponent<Weapon>();

    }

    public void SwitchWeapon()
    {
        UnequippedGun();
        //change equipped
        EquipWeapon(new GameObject());
    }

    public void  UnequippedGun()
    {
        _weapon.IsEmptyClip -= RechargeGun;
        _weapon.IsChangedClip -= UpdateUI;
        UpdateUI();
        _gunEquipped.transform.SetParent(null);
        _gunEquipped.SetActive(false);
    }
    
    public Weapon GetEquippedWeapon()
    {
        EquipWeapon(_gunEquipped);
        var weapon = _gunEquipped.gameObject.GetComponent<Weapon>();
        return weapon;
    }
    public void RechargeGun()
    {
        int remains = _weapon.Recharge(_userCountBullets);
        _userCountBullets = remains;
        if (_userCountBullets < 0) _userCountBullets = 0;
        UpdateUI();
    }
    
    private void EquipWeapon(GameObject weapon)
         {
                    //
             var transform = _transformTo;
             
             _gunEquipped = weapon;
             _gunEquipped.SetActive(true);
             _gunEquipped.transform.SetParent(transform);
             _gunEquipped.transform.position = transform.position;
             _gunEquipped.transform.rotation = transform.rotation;

             _weapon.IsEmptyClip += RechargeGun;
             _weapon.IsChangedClip += UpdateUI;
             RechargeGun();
         }

    private void UpdateUI()
    {
        _ui.UpdateUIPlayerClip(_weapon.CountBulletInTheClip,
                        //
            _userCountBullets);
    }
    
}
