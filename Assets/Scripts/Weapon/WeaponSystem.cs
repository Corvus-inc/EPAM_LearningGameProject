using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class WeaponSystem 
{
    private List<GameObject> _listWeapons;
    private readonly Transform _transformTo;
    private GameObject _gunEquipped ;
    private Weapon _weapon;
    private int _userCountBullets;
    private readonly UIPlayer _ui;
    private int _countIndexWeapon;
    private int _indexWeapon;

    public WeaponSystem(List<GameObject> listWeapons, Transform transformTo, UIPlayer UI, int userCountBullets)
    {
        _indexWeapon = 1;
        _countIndexWeapon = listWeapons.Count;
        
        _ui = UI;
        _listWeapons = listWeapons;
        _transformTo = transformTo;
        _gunEquipped = listWeapons[_indexWeapon];
        _userCountBullets = userCountBullets;
        _weapon = _gunEquipped.gameObject.GetComponent<Weapon>();

    }

    public Weapon SwitchWeapon()
    {
        UnequippedGun();
        _indexWeapon = _indexWeapon < _countIndexWeapon-1 ? ++_indexWeapon : 0;
        _gunEquipped = _listWeapons[_indexWeapon];
        return GetEquippedWeapon();
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
        return _weapon;
    }
    public void RechargeGun()
    {
        int remains = _weapon.Recharge(_userCountBullets);
        _userCountBullets = remains;
        if (_userCountBullets < 0) _userCountBullets = 0;
        UpdateUI();
    }
    
    private void EquipWeapon(GameObject weaponObject)
         {
             _gunEquipped = weaponObject;
             _gunEquipped.SetActive(true);
             _gunEquipped.transform.SetParent(_transformTo);
             _gunEquipped.transform.position = _transformTo.position;
             _gunEquipped.transform.rotation = _transformTo.rotation;
             
             _weapon = _gunEquipped.gameObject.GetComponent<Weapon>();
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
