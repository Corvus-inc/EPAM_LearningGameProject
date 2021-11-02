using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class WeaponSystem 
{
    private List<GameObject> _listWeapons;
    private GameObject _gunEquipped ;
    private Weapon _weapon ;
    private UIPlayer _ui;
    
    private PlayerCharacter _player;
    private  Transform playerTransform;

    public WeaponSystem(List<GameObject> listWeapons, PlayerCharacter playerCharacter, UIPlayer UI)
    {
        _listWeapons = listWeapons;
        _player = playerCharacter;
        _ui = UI;
        playerTransform = _player.transform;
        _gunEquipped = listWeapons[1];
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
        int remains = _weapon.Recharge(_player.CountBullets);
        _player.CountBullets = remains;
        if (_player.CountBullets < 0) _player.CountBullets = 0;
        UpdateUI();
    }
    
    private void EquipWeapon(GameObject weapon)
         {
                    //
             var transform = _player.transform;
             
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
            _player.CountBullets);
    }
    
}
