using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    LayerMask _hitCheckLayer;

    bool _isAlive = false;
    BulletManager.BulletData _bulletData;
    TrailRenderer _bulletTrail;

    public BulletManager.BulletData _BulletData { set { _bulletData = value; } }

    public void ShootBullet(BulletManager.BulletData data)
    {
        _bulletData = data;
        _isAlive = true;
    }

    /// <summary>
    /// 총알을 다시 Pool에 넣고 초기화하는 함수
    /// </summary>
    void BulletDestroy()
    {
        BulletManager._Instance.ReturnBullet(this);
        _bulletTrail.Clear();
        _isAlive = false;
        gameObject.SetActive(false);
    }

    void Start()
    {
        _bulletTrail = gameObject.transform.GetChild(1).GetComponent<TrailRenderer>();
    }

    void Update()
    {
        if (!_isAlive)
        {
            BulletDestroy();
            return;
        }

        BulletCalculator.Calculate_BulletPosition(ref _bulletData);

        RaycastHit rayHit;
        Vector3 direction = _bulletData.position - _bulletData.prev_position;

        if(Physics.Raycast(_bulletData.prev_position, direction, out rayHit, _bulletData.speed * Time.deltaTime, _hitCheckLayer))
        {
            transform.position = rayHit.point;
            _isAlive = false;
        }
        else
        {
            if (_bulletData.remainLifeTime <= 0f)
            {
                _isAlive = false;
            }
            else
            {
                transform.position = _bulletData.position;
            }
        }
    }
}
