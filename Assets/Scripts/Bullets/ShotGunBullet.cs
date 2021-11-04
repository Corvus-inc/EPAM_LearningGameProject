
using UnityEngine;
using Random = System.Random;

public class ShotGunBullet : BaseBullet
{
    [SerializeField] private PartBullet prefabPartBullet;
    [SerializeField] private int countParts;
    [SerializeField] [Range(0,0.5f)] private float spreadForce = 0.5f;

    private PartBullet[] _partsBullet;
    private Random _rdm;
    
    private void Awake()
    {
        //base
        saveParent = transform.parent;
        ApplyStartBulletDamage();
        //create parts
        _rdm = new Random();
        _partsBullet = new PartBullet[countParts];
        IsActiveBullet += CollectAllParts;
        
        for(var i = 0; i < _partsBullet.Length; i++)
        {
            _partsBullet[i] = Instantiate(prefabPartBullet, transform);
        }
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        foreach (var bullet in _partsBullet)
        {
            bullet?.ActivatingBullet();
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
            var transform1 = part.transform;
            var transform2 = transform;
            transform1.position = transform2.position;
            transform1.rotation = transform2.rotation;
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
        part.direction += new Vector3(rdmDirection*spreadForce/countRandomDirections,0,0);
    }
}
