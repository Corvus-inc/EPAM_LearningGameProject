using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultBullet : BaseBullet
{
    private Transform saveParent;

    private float speedBullet = 300f;//вынести в базовый

    public AssaultBullet()
    {
        bulletDamage = 5;
    }

    private void Awake()
    {
        saveParent = transform.parent;
    }

    private void Update()
    {
        if(isFlying) transform.Translate(Vector3.up * Time.deltaTime* speedBullet);
    }

    public override IEnumerator ActiveBullet(float timeLive)
    {
        gameObject.SetActive(true);
        transform.parent = null;
        isFlying = true;
        yield return new WaitForSeconds(timeLive);
        transform.SetParent(saveParent);
        gameObject.SetActive(false);
        isFlying = false;
    }
}
