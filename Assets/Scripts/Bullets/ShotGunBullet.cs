using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class ShotGunBullet : BaseBullet
{
    [SerializeField] private PartBullet prefabPartBullet;
    [SerializeField] private int countParts;
    [SerializeField] [Range(0,0.5f)] private float _spreadForce = 0.5f;

    private PartBullet[] _partsBullet;
    private Random _rdm;
    
    private void Start()
    {
        gameObject.SetActive(true);
        _rdm = new Random();
        IsActiveBullet += CollectAllParts;
        
        _partsBullet = new PartBullet[countParts];
        for(var i = 0; i < _partsBullet.Length; i++)
        {
            _partsBullet[i] = Instantiate(prefabPartBullet, transform);
        }
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        //Create event for activating parts
        if (!_isFlying) return;
        foreach (var part in _partsBullet)
        {
            part.gameObject.SetActive(true);
        }
    }

    public override void AddBulletDamage(int damage)
    {
        _bulletDamage *= damage;
    }
    
    private void CollectAllParts()
    {
        foreach (var part in _partsBullet)
        {
            part.transform.position = transform.position;
            part.transform.rotation = transform.rotation;
            SetStatsForPart(part);
        }
    }

    private void SetStatsForPart(PartBullet currentPart)
    {
        currentPart._bulletDamage = _bulletDamage/countParts;
        currentPart._speedBullet = _speedBullet;
        currentPart._lifeTimeBullet = _lifeTimeBullet;
        CalculateRandomSpread(currentPart);
    }

    private void CalculateRandomSpread(PartBullet part)
    {
        const int countRandomDirections = 50;
        var rdmDirection = _rdm.Next(-countRandomDirections, countRandomDirections);
        part.direction += new Vector3(rdmDirection*_spreadForce/countRandomDirections,0,0);
    }
}
