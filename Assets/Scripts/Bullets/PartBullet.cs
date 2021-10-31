using UnityEngine;

public class PartBullet : ShotGunBullet
{
    public Vector3 direction;
    private void FixedUpdate()
    {
        transform.Translate(direction * (Time.deltaTime * _speedBullet));
    }
}
