using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class BulletManager : SingletonGameobject<BulletManager>
{
    public struct BulletData
    {
        public Vector3 prev_position;
        public Vector3 position;
        public Vector3 direction;
        public float remainLifeTime;
        public float speed;
    }

    [SerializeField]
    GameObject _bulletPrefab;
    [SerializeField]
    float _bulletSpeed = 10f;

    ObjectPool<BulletController> _bulletPool;

    public void Initialize()
    {
        _bulletPool = new ObjectPool<BulletController>(10, () =>
        {
            GameObject bullet = Instantiate(_bulletPrefab);
            BulletController bulletcontroller = bullet.GetComponent<BulletController>();
            bullet.transform.SetParent(transform);
            bullet.SetActive(false);

            return bulletcontroller;
        });
    }

    public void ShootBullet(BulletData data)
    {
        BulletController bulletController = _bulletPool.Get();
        bulletController.gameObject.SetActive(true);
        data.speed = _bulletSpeed;
        bulletController.ShootBullet(data);
    }

    public void ReturnBullet(BulletController bullet)
    {
        _bulletPool.Set(bullet);
    }

    void Start()
    {
        // 임시 초기화
        Initialize();
    }
}
