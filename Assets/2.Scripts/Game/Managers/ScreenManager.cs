using UnityEngine;
using UnityEngine.InputSystem;

public class ScreenManager : SingletonGameobject<ScreenManager>
{
    Vector2 _mousePosition;
    public Vector2 _MousePosition { get { return _mousePosition; } }

    GameObject _crossHair;
    CrossHairController _crossHairController;
    PlayerController _playerController;

    void Start()
    {
        _crossHair = GameObject.FindGameObjectWithTag("CrossHair");
        _crossHairController = _crossHair.GetComponent<CrossHairController>();

        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void Event_Aim(InputAction.CallbackContext context)
    {
        _mousePosition = context.ReadValue<Vector2>();
        if(_playerController != null)
            _playerController.SetAimPosition(_mousePosition);
    }

    public void Event_Shoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _playerController.ShootBullet();
        }
        //if (context.performed)
        //    Debug.Log("performed");
        //if (context.canceled)
        //    Debug.Log("canceled");
    }

    public void Event_Test(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _crossHairController._SubCrossHair.CrossHair_KillEffect();
            //Debug.Log("Test Start");
        }
    }
}
