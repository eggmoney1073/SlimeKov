using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    float _rotateSpeed = 5f;
    [Header("Gun Prefabs")]
    [SerializeField] GameObject[] _gunPrefabs;

    IGunBehaviour[] _currentGuns;
    GunDataBase[] _currentGunDatas;
    bool[] _isGunEquips;
    int _currentGunNumber;

    Vector3 _gunDirection;

    public void Start_Shoot()
    {
        if (_isGunEquips[_currentGunNumber])
            _currentGuns[_currentGunNumber].Start_Shoot(_gunDirection);
    }

    public void Finish_Shoot()
    {
        if (_isGunEquips[_currentGunNumber])
            _currentGuns[_currentGunNumber].Finish_Shoot();
    }

    public void SetGuns()
    {
        _currentGunNumber = 0;

        int childCount = transform.childCount;

        if(childCount == 1)
        {
            IGunBehaviour gun = transform.GetChild(0).GetComponent<IGunBehaviour>();
            if(gun != null)
            {
                _currentGuns[0] = gun;
                _currentGunDatas[0] = gun.GetGunBase();
                _isGunEquips[0] = true;
            }
            else
            {
                _isGunEquips[0] = false;
            }
        }
        else if(childCount == 2)
        {
            for (int i = 0; i < 2; i++)
            {
                IGunBehaviour gun = transform.GetChild(0).GetComponent<IGunBehaviour>();
                if (gun != null)
                {
                    _currentGuns[i] = gun;
                    _currentGunDatas[i] = gun.GetGunBase();
                    _isGunEquips[i] = true;
                }
                else
                {
                    _isGunEquips[i] = false;
                }
            }
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                _isGunEquips[i] = false;
            }
        }
    }

    public void SetGunDirection(Vector3 aimPosition)
    {
        _gunDirection = aimPosition - transform.position;
        _gunDirection.y = 0.0f;
    }

    void GunRotate()
    {
        if (_gunDirection != Vector3.zero)
        {
            Quaternion shootdirection = Quaternion.LookRotation(_gunDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, shootdirection, _rotateSpeed * Time.deltaTime);
        }
    }

    void Start()
    {
        _currentGuns = new IGunBehaviour[2];
        _currentGunDatas = new GunDataBase[2];
        _isGunEquips = new bool[2];
        SetGuns();
    }

    void Update()
    {
        GunRotate();
    }
}
