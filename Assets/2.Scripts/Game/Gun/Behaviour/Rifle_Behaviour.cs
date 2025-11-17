using System.Collections;
using UnityEngine;

public class Rifle_Behaviour : MonoBehaviour, IGunBehaviour
{
    [SerializeField]
    GunDataBase _gunData;
    bool _isInputHeld = false;
    bool _camFire = true;

    public GunDataBase GetGunBase()
    {
        return _gunData;
    }

    public void Start_Shoot(Vector3 direction)
    {
        if (!_isInputHeld && _camFire)
        {
            _isInputHeld = true;

            BulletManager.BulletData bulletData = new BulletManager.BulletData()
            {
                prev_position = transform.position,
                position = transform.position,
                speed = _gunData._bulletSpeed,
                direction = direction,
                remainLifeTime = _gunData._range / _gunData._bulletSpeed
            };

            BulletManager._Instance.ShootBullet(bulletData);
            _camFire = false;
            StartCoroutine(Co_CheckNextFireTime(1 / _gunData._shootRate));
        }
    }

    public void Finish_Shoot()
    {
        _isInputHeld = false;
    }

    public void Reload()
    {

    }

    IEnumerator Co_CheckNextFireTime(float fireCoolDownTime)
    {
        yield return new WaitForSeconds(fireCoolDownTime);

        _camFire = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
