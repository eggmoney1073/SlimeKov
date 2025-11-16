using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _moveSpeed = 5.0f;
    [SerializeField]
    float _rotateSpeed = 5.0f;
    [SerializeField]
    float _gravity = 0.9f;
    [SerializeField]
    float _bulletRange = 3f;

    [SerializeField]
    LayerMask _groundLayer;

    GameObject _characterModelGO;
    GameObject _gunGO;

    Camera _mainCam;
    CharacterController _characterController;
    Vector2 _moveInput;

    Vector3 _gunDirection = Vector3.zero;
    Vector3 _playerDirection = Vector3.zero;

    public void Event_Move(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void SetAimPosition(Vector2 mousePosition)
    {
        Ray ray = _mainCam.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        Vector3 aimPosition = Vector3.zero;

        if (Physics.Raycast(ray, out hit, 100f, _groundLayer))
        {
            aimPosition = hit.point;
        }

        _playerDirection = aimPosition - transform.position;
        _playerDirection.y = 0.0f;

        _gunDirection = aimPosition - _gunGO.transform.position;
        _gunDirection.y = 0.0f;
    }

    public void ShootBullet()
    {
        BulletManager.BulletData bulletData = new BulletManager.BulletData()
        {
            prev_position = _gunGO.transform.position,
            position = _gunGO.transform.position,
            direction = _gunDirection,
            remainLifeTime = _bulletRange
        };

        BulletManager._Instance.ShootBullet(bulletData);
    }

    void RotatePlayer(Vector3 direction)
    {
        direction.y = 0.0f;

        if (direction != Vector3.zero)
        {
            Quaternion quaternionDirection = Quaternion.LookRotation(direction);
            Transform modelTransform = _characterModelGO.transform;
            modelTransform.rotation = Quaternion.Slerp(modelTransform.rotation, quaternionDirection, _rotateSpeed * Time.deltaTime);
        }
    }

    void MovePlayer(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Vector3 velocity = direction * _moveSpeed * Time.deltaTime;
            _characterController.Move(velocity);
        }
    }

    void RotateGun(Vector3 direction)
    {
        direction.y = 0.0f;
        Transform gunTransform = _gunGO.transform;

        if (direction != Vector3.zero)
        {
            Quaternion shootdirection = Quaternion.LookRotation(direction);
            gunTransform.rotation = Quaternion.Slerp(gunTransform.rotation, shootdirection, _rotateSpeed * Time.deltaTime);
        }
    }


    void Awake()
    {
        _characterController = gameObject.GetComponent<CharacterController>();        
    }

    void Start()
    {
        _mainCam = Camera.main;
        _characterModelGO = transform.GetChild(0).gameObject;
        _gunGO = _characterModelGO.transform.GetChild(2).gameObject;
    }


    void Update()
    {
        float x = _moveInput.x;
        float y = _moveInput.y;

        Vector3 direction = new Vector3(x, 0.0f, y);

        if (!_characterController.isGrounded)
        {
            direction.y = -_gravity;
        }

        MovePlayer(direction);

        RotatePlayer(_playerDirection);
        RotateGun(_gunDirection);
    }
}
