using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using LoaderSystem;
using UnityEngine;

public class WeaponSystem
{
    public IStatLoader StatLoader { private get; set; }
    public WeaponHolder CurrentWeaponHolder { get; private set; }
    public int UserCountBullets{ get; private set; }
    
    private List<WeaponHolder> _listWeapons;
    private readonly Transform _transformTo;
    private GameObject _gunEquipped ;
    private readonly UIPlayer _ui;
    private readonly int _countIndexWeapon;
    private int _indexWeapon;

    public WeaponSystem(GameObject currentWeapon, Transform transformTo, int userCountBullets)
    {
        _gunEquipped = currentWeapon;
        CurrentWeaponHolder = _gunEquipped.GetComponent<WeaponHolder>();
        CurrentWeaponHolder._gunCurrent.CountBulletInTheClip = CurrentWeaponHolder._gunCurrent.MaxBulletInTheClip;
        _transformTo = transformTo;
        UserCountBullets = userCountBullets;
        EquipWeapon(_gunEquipped);
    }
    public WeaponSystem(List<WeaponHolder> listWeapons, Transform transformTo, UIPlayer UI, int userCountBullets, IStatLoader statLoader)
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
        CurrentWeaponHolder = listWeapons[_indexWeapon];

        for (var i = 0; i < _listWeapons.Count; i++ )
        {
            var clip = StatLoader.WeaponPlayerData.WeaponSavingStatsList[i].ClipCount;
            _listWeapons[i]._gunCurrent.CountBulletInTheClip = clip;
        }
    }

    private void SaveWeaponPlayerData()
    {
        StatLoader.WeaponPlayerData.Index = _indexWeapon;
        
        var weaponSavingStatsList = new List<WeaponSavingStats>();
        for (var i = 0; i < _listWeapons.Count; i++)
        {
            {
                var weaponSavingStats = new WeaponSavingStats()
                {
                    ClipCount = _listWeapons[i]._gunCurrent.CountBulletInTheClip,
                    ID = i
                };
                weaponSavingStatsList.Add(weaponSavingStats);
            }
        }
        StatLoader.WeaponPlayerData.WeaponSavingStatsList = weaponSavingStatsList;

    }

    public WeaponHolder GetCurrentWeaponHolder()
    {
      return _gunEquipped.gameObject.GetComponent<WeaponHolder>();
    }
    public WeaponHolder SwitchWeapon()
    {
        CurrentWeaponHolder._gunCurrent.ReturnAllBulletToSpawn();
        UnequippedGun();
        _indexWeapon = _indexWeapon < _countIndexWeapon-1 ? ++_indexWeapon : 0;
        _gunEquipped = _listWeapons[_indexWeapon].gameObject;
        return GetEquippedWeapon();
    }

    public void  UnequippedGun()
    {
        CurrentWeaponHolder._gunCurrent.IsEmptyClip -= RechargeGun;
        CurrentWeaponHolder._gunCurrent.IsChangedClip -= UpdateUI;
        UpdateUI();
        _gunEquipped.transform.SetParent(null);
        _gunEquipped.SetActive(false);
    }
    
    public WeaponHolder GetEquippedWeapon()
    {
        EquipWeapon(_gunEquipped);
        return CurrentWeaponHolder;
    }
    public void RechargeGun()
    {
        int remains = CurrentWeaponHolder._gunCurrent.Recharge(UserCountBullets);
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

             CurrentWeaponHolder = GetCurrentWeaponHolder();
             CurrentWeaponHolder._gunCurrent.IsEmptyClip += RechargeGun;
             CurrentWeaponHolder._gunCurrent.IsChangedClip += UpdateUI;
             RechargeGun(); 
         }

    private void UpdateUI()
    {
        _ui?.UpdateUIPlayerClip(CurrentWeaponHolder._gunCurrent.CountBulletInTheClip, UserCountBullets, CurrentWeaponHolder.CurrentIcon);
    }
}
