using UnityEngine;
using UnityEngine.InputSystem;

public class ScreenManager : SingletonGameobject<ScreenManager>
{
    [SerializeField]
    LayerMask _groundLayer;

    Vector2 _mousePosition;
    public Vector2 _MousePosition { get { return _mousePosition; } }

    Vector3 _aimPosition;
    public Vector3 _AimPosition { get { return _aimPosition; } }

    GameObject _crossHair;

    Camera _mainCam;

    CrossHairController _crossHairController;

    PlayerController _playerController;

    GunController _gunController;

    void Start()
    {
        _mainCam = Camera.main;

        _crossHair = GameObject.FindGameObjectWithTag("CrossHair");
        _crossHairController = _crossHair.GetComponent<CrossHairController>();

        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _gunController = GameObject.FindGameObjectWithTag("GunController").GetComponent<GunController>();
    }

    public void Event_Aim(InputAction.CallbackContext context)
    {
        _mousePosition = context.ReadValue<Vector2>();
        GetAimPosition(_mousePosition);

        if (_playerController != null)
            _playerController.SetPlayerDirection(_aimPosition);

        if (_gunController != null)
            _gunController.SetGunDirection(_aimPosition);
    }

    void GetAimPosition(Vector2 mousePosition)
    {
        Ray ray = _mainCam.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, _groundLayer))
        {
            _aimPosition = hit.point;
        }        
    }

    public void Event_Shoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _gunController.Start_Shoot();
        }

        //if (context.performed)
        //    Debug.Log("performed");

        if (context.canceled)
        {
            _gunController.Finish_Shoot();
        }
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
