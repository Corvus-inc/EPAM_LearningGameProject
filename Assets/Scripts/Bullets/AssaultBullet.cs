using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultBullet : BaseBullet
{
    private Transform saveParent;

    private float speedBullet = 300f;//вынести в базовый

    private void Awake()
    {
        saveParent = transform.parent;
    }

    private void Update()
    {
        if (_isFlying)
        {
            transform.Translate(Vector3.up * Time.deltaTime * speedBullet);

        }
    }

    public override void ActivatingBullet()
    {
        gameObject.SetActive(true);
        transform.parent = null;
        _isFlying = true;
    }

    public override IEnumerator DeactivatingBullet(float timeLive)
    {
        
        yield return new WaitForSeconds(timeLive);
        transform.SetParent(saveParent);
        gameObject.SetActive(false);
        _isFlying = false;
    }
}
