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

    CharacterController _characterController;
    Vector2 _moveInput;

    Vector3 _playerDirection = Vector3.zero;

    public void Event_Move(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void SetPlayerDirection(Vector3 aimPosition)
    {
        _playerDirection = aimPosition - transform.position;
        _playerDirection.y = 0.0f;
    }

    void RotatePlayer()
    {
        if (_playerDirection != Vector3.zero)
        {
            Quaternion quaternionDirection = Quaternion.LookRotation(_playerDirection);
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


    void Awake()
    {
        _characterController = gameObject.GetComponent<CharacterController>();        
    }

    void Start()
    {
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

        RotatePlayer();
    }
}
