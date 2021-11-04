using UnityEngine;

public class PartBullet : ShotGunBullet
{
    public Vector3 direction;

    private void Start()
    {
        //override start for no disable the object
    }
    private void FixedUpdate()
    {
        var t =gameObject;
        transform.Translate(direction * (Time.deltaTime * _speedBullet));
    }
    public override void ActivatingBullet()
    {
        gameObject.SetActive(true);
        OnActiveBullet();
        _isFlying = true;
    }
}
