using System.Collections;
using UnityEngine;

public class Shotgun_Behaviour : MonoBehaviour, IGunBehaviour
{
    [SerializeField]
    GunDataBase _gunData;
    bool _isInputHeld = false;
    bool _camFire = true;

    GunData_ShotGun _shotGunData;

    public GunDataBase GetGunBase()
    {
        return _gunData;
    }

    public void Start_Shoot(Vector3 direction)
    {
        if (!_isInputHeld && _camFire)
        {
            _isInputHeld = true;

            Shoot(direction);
            _camFire = false;
            StartCoroutine(Co_CheckNextFireTime(1 / _gunData._shootRate));
        }
    }

    void Shoot(Vector3 direction)
    {
        for (int i = 0; i < _shotGunData._bulletCount; i++)
        {
            Quaternion rotateToMain = Quaternion.LookRotation(direction);

            float randomYaw = Random.Range(-_shotGunData._splitAngle, _shotGunData._splitAngle);
            Quaternion randomRotate = Quaternion.Euler(0.0f, randomYaw, 0.0f);
            Quaternion finalRotate = randomRotate * rotateToMain;
            Vector3 finalDirection = finalRotate * Vector3.forward;
            finalDirection.y = 0.0f;
            finalDirection.Normalize();

            BulletManager.BulletData bulletData = new BulletManager.BulletData()
            {
                prev_position = transform.position,
                position = transform.position,
                speed = _gunData._bulletSpeed,
                direction = finalDirection,
                remainLifeTime = _gunData._range / _gunData._bulletSpeed
            };

            BulletManager._Instance.ShootBullet(bulletData);
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
        _shotGunData = _gunData as GunData_ShotGun;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
