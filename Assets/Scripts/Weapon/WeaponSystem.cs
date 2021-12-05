using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using LoaderSystem;
using UnityEngine;

public class WeaponSystem
{
    public int UserCountBullets{ get; private set; }
    
    private int _indexWeapon;
    private GameObject _gunEquipped;
    private WeaponHolder _currentWeaponHolder;
    
    private readonly UIPlayer _ui;
    
    private readonly int _countIndexWeapon;
    private readonly Transform _transformTo;
    private readonly IStatLoader _statLoader;
    private readonly List<WeaponHolder> _listWeapons;

    public WeaponSystem(GameObject currentWeapon, Transform transformTo, int userCountBullets)
    {
        _gunEquipped = currentWeapon;
        _currentWeaponHolder = _gunEquipped.GetComponent<WeaponHolder>();
        _currentWeaponHolder.GunCurrent.CountBulletInTheClip = _currentWeaponHolder.GunCurrent.MaxBulletInTheClip;
        _transformTo = transformTo;
        UserCountBullets = userCountBullets;
        EquipWeapon(_gunEquipped);
    }
    public WeaponSystem(List<WeaponHolder> listWeapons, Transform transformTo, UIPlayer UI, int userCountBullets, IStatLoader statLoader)
    {
        _statLoader = statLoader;
        // on destroy
        _statLoader.OnSavePlayerData += SaveWeaponPlayerData;
        
        _indexWeapon = statLoader.WeaponPlayerData.Index;
        _countIndexWeapon = statLoader.WeaponPlayerData.WeaponSavingStatsList.Count;
        
        _ui = UI;
        _listWeapons = listWeapons;
        _transformTo = transformTo;
        _gunEquipped = listWeapons[_indexWeapon].gameObject;
        UserCountBullets = userCountBullets;
        _currentWeaponHolder = listWeapons[_indexWeapon];

        for (var i = 0; i < _listWeapons.Count; i++ )
        {
            var clip = _statLoader.WeaponPlayerData.WeaponSavingStatsList[i].ClipCount;
            _listWeapons[i].GunCurrent.CountBulletInTheClip = clip;
        }
    }

    private void SaveWeaponPlayerData()
    {
        _statLoader.WeaponPlayerData.Index = _indexWeapon;
        
        var weaponSavingStatsList = new List<WeaponSavingStats>();
        for (var i = 0; i < _listWeapons.Count; i++)
        {
            {
                var weaponSavingStats = new WeaponSavingStats()
                {
                    ClipCount = _listWeapons[i].GunCurrent.CountBulletInTheClip,
                    ID = i
                };
                weaponSavingStatsList.Add(weaponSavingStats);
            }
        }
        _statLoader.WeaponPlayerData.WeaponSavingStatsList = weaponSavingStatsList;

    }

    public WeaponHolder GetCurrentWeaponHolder()
    {
      return _gunEquipped.gameObject.GetComponent<WeaponHolder>();
    }
    public WeaponHolder SwitchWeapon()
    {
        _currentWeaponHolder.GunCurrent.ReturnAllBulletToSpawn();
        UnequippedGun();
        _indexWeapon = _indexWeapon < _countIndexWeapon-1 ? ++_indexWeapon : 0;
        _gunEquipped = _listWeapons[_indexWeapon].gameObject;
        return GetEquippedWeapon();
    }

    public void  UnequippedGun()
    {
        _currentWeaponHolder.GunCurrent.IsEmptyClip -= RechargeGun;
        _currentWeaponHolder.GunCurrent.IsChangedClip -= UpdateUI;
        UpdateUI();
        _gunEquipped.transform.SetParent(null);
        _gunEquipped.SetActive(false);
    }
    
    public WeaponHolder GetEquippedWeapon()
    {
        EquipWeapon(_gunEquipped);
        return _currentWeaponHolder;
    }
    public void RechargeGun()
    {
        int remains = _currentWeaponHolder.GunCurrent.Recharge(UserCountBullets);
        UserCountBullets = remains;
        if (UserCountBullets < 0) UserCountBullets = 0;
        UpdateUI();
    }
    
    private void EquipWeapon(GameObject weaponObject)
         {
             _gunEquipped = weaponObject;
             _gunEquipped.SetActive(true);
             _gunEquipped.transform.SetParent(_transformTo);
             _gunEquipped.transform.position = _transformTo.position;
             _gunEquipped.transform.rotation = _transformTo.rotation;

             _currentWeaponHolder = GetCurrentWeaponHolder();
             _currentWeaponHolder.GunCurrent.IsEmptyClip += RechargeGun;
             _currentWeaponHolder.GunCurrent.IsChangedClip += UpdateUI;
             RechargeGun(); 
         }

    private void UpdateUI()
    {
        _ui?.UpdateUIPlayerClip(_currentWeaponHolder.GunCurrent.CountBulletInTheClip, UserCountBullets, _currentWeaponHolder.CurrentIcon);
    }
}
