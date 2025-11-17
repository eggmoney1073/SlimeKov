using UnityEngine;

public interface IGunBehaviour
{
    GunDataBase GetGunBase();
    void Start_Shoot(Vector3 direction);
    void Finish_Shoot();
    void Reload();
}
