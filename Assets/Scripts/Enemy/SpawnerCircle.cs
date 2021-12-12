using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerCircle : MonoBehaviour, ISpawningWithHealth
{
    public event Action<IHaveHealth> spawnedObject;
    
    [SerializeField] private BaseHaveHealth prefabEnemy;
    
    [Range(1f, 7f)] [SerializeField] private int countHumans;
    [Range(0.5f, 3f)] [SerializeField] private float distanceRandomHuman;

    private List<BaseHaveHealth> _humansInCircle;
    private float _distanceRandomHuman;
    private int _countHumans;

    private void Start()
    {
        _countHumans = countHumans;
        _distanceRandomHuman = distanceRandomHuman;
        _humansInCircle = new List<BaseHaveHealth>();
        HumanPlaceInCircle();
    }

    private void HumanPlaceInCircle()
    {
        var points = CreateRandomPoints();
        var humanRotate = new Vector3(0f, -90.0f, 0f);

        for (var i = 0; i < points.Count-1; i++)
        {

            var rndPos = new Vector3(points[i].x, prefabEnemy.transform.position.y, points[i].y);

            var obj = Instantiate(prefabEnemy, rndPos, Quaternion.identity, transform);
            obj.transform.localPosition = rndPos;
            spawnedObject?.Invoke(obj);

           
            _humansInCircle.Add(obj);
        }
        // spawnedObject = null;
    }

    private List<Vector2> CreateRandomPoints() 
    {
        var points = new List<Vector2>();
        #region MyRegion

        #endregion
        var disH = _distanceRandomHuman;
        var crashCounter = 0;

        var flagP = true;

        for (var i = 0; i <= _countHumans; i++)
        {
            var rnd = Random.insideUnitCircle * gameObject.GetComponent<SphereCollider>().radius;
            if (points.Count > 0)
            {
                if (points.Select(el => Vector2.Distance(el, rnd)).Any(dis => dis < disH))
                {
                    flagP = false;
                }
            }
            if (!flagP)
            {
                flagP = true;
                i--;
                crashCounter++;
            }
            else
            {
                points.Add(rnd);
            }
            if (crashCounter == 20) break;
        }
        return points;
    }
}