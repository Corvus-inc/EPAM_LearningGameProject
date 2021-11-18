using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using LoaderSystem;
using UnityEngine;

public class WeaponSystem
{
    public StatLoader StatLoader { private get; set; }
    public Weapon CurrentWeapon { get; private set; }
    public int UserCountBullets{ get; private set; }
    
    private List<Weapon> _listWeapons;
    private readonly Transform _transformTo;
    private GameObject _gunEquipped ;
    private readonly UIPlayer _ui;
    private readonly int _countIndexWeapon;
    private int _indexWeapon;

    public WeaponSystem(GameObject currentWeapon, Transform transformTo, int userCountBullets)
    {
        _gunEquipped = currentWeapon;
        CurrentWeapon = _gunEquipped.GetComponent<Weapon>();
        CurrentWeapon.CountBulletInTheClip = CurrentWeapon.MaxBulletInTheClip;
        _transformTo = transformTo;
        UserCountBullets = userCountBullets;
        EquipWeapon(_gunEquipped);
    }
    public WeaponSystem(List<Weapon> listWeapons, Transform transformTo, UIPlayer UI, int userCountBullets, StatLoader statLoader)
    {
        StatLoader = statLoader;
        // on destroy
        StatLoader.OnSavePlayerData += SaveWeaponPlayerData;
        
        _indexWeapon = statLoader.WeaponPlayerData.Index;
        _countIndexWeapon = statLoader.WeaponPlayerData.WeaponSavingStatsList.Count;
        
        _ui = UI;
        _listWeapons = listWeapons;
        _transformTo = transformTo;
        _gunEquipped = listWeapons[_indexWeapon].gameObject;
        UserCountBullets = userCountBullets;
        CurrentWeapon = listWeapons[_indexWeapon];

        for (var i = 0; i < _listWeapons.Count; i++ )
        {
            var clip = StatLoader.WeaponPlayerData.WeaponSavingStatsList[i].ClipCount;
            _listWeapons[i].CountBulletInTheClip = clip;
        }
    }

    private void SaveWeaponPlayerData()
    {
        StatLoader.WeaponPlayerData.Index = _indexWeapon;
        
        var weaponSavingStatsList = new List<WeaponSavingStats>();
        for (int i = 0; i < _listWeapons.Count; i++)
        {
            {
                var weaponSavingStats = new WeaponSavingStats()
                {
                    ClipCount = _listWeapons[i].CountBulletInTheClip,
                    ID = i
                };
                weaponSavingStatsList.Add(weaponSavingStats);
            }
        }
        StatLoader.WeaponPlayerData.WeaponSavingStatsList = weaponSavingStatsList;

    }

    public Weapon GetCurrentWeapon()
    {
      return _gunEquipped.gameObject.GetComponent<Weapon>();
    }
    public Weapon SwitchWeapon()
    {
        CurrentWeapon.ReturnAllBulletToSpawn();
        UnequippedGun();
        _indexWeapon = _indexWeapon < _countIndexWeapon-1 ? ++_indexWeapon : 0;
        _gunEquipped = _listWeapons[_indexWeapon].gameObject;
        return GetEquippedWeapon();
    }

    public void  UnequippedGun()
    {
        CurrentWeapon.IsEmptyClip -= RechargeGun;
        CurrentWeapon.IsChangedClip -= UpdateUI;
        UpdateUI();
        _gunEquipped.transform.SetParent(null);
        _gunEquipped.SetActive(false);
    }
    
    public Weapon GetEquippedWeapon()
    {
        EquipWeapon(_gunEquipped);
        return CurrentWeapon;
    }
    public void RechargeGun()
    {
        int remains = CurrentWeapon.Recharge(UserCountBullets);
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

             CurrentWeapon = GetCurrentWeapon();
             CurrentWeapon.IsEmptyClip += RechargeGun;
             CurrentWeapon.IsChangedClip += UpdateUI;
             RechargeGun(); 
         }

    private void UpdateUI()
    {
        _ui?.UpdateUIPlayerClip(CurrentWeapon.CountBulletInTheClip, UserCountBullets, CurrentWeapon.CurrentIcon);
        
    }
    
}
