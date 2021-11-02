using System.Collections;
using System.Collections.Generic;
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
        _gunEquipped = _listWeapons[1];
        EquipWeapon(_gunEquipped);
        var weapon = _gunEquipped.gameObject.GetComponent<Weapon>();
                                    //
        weapon.GameState = _player.GameState;
        return weapon;
    }
    private void EquipWeapon(GameObject weapon)
         {
             var gunEquipped = _gunEquipped;
                 //
             var transform = _player.transform;
             
             
             gunEquipped = weapon;
             gunEquipped.SetActive(true);
             gunEquipped.transform.SetParent(transform);
             gunEquipped.transform.position = transform.position;
             gunEquipped.transform.rotation = transform.rotation;

             _weapon.IsEmptyClip += RechargeGun;

             _weapon.IsChangedClip += UpdateUI;
             RechargeGun();
         }
    public void RechargeGun()
    {
        int remains = _weapon.Recharge(_player.CountBullets);
        _player.CountBullets = remains;
        if (_player.CountBullets < 0) _player.CountBullets = 0;
        UpdateUI();
    }

    private void UpdateUI()
    {
        _ui.UpdateUIPlayerClip(_weapon.CountBulletInTheClip,
                        //
            _player.CountBullets);
    }
    
}
