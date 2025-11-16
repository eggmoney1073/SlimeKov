using UnityEngine;

public class CameraMoveController : MonoBehaviour
{
    [SerializeField]
    Vector3 _positionOffset;

    GameObject _playerGO;

    void Start()
    {
        _playerGO = GameObject.FindGameObjectWithTag("Player");
    }

    void LateUpdate()
    {
        gameObject.transform.position = _playerGO.transform.position + _positionOffset;
    }
}
