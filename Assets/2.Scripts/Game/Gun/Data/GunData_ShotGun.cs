using UnityEngine;

[CreateAssetMenu(fileName = "GunData_ShotGun", menuName = "Scriptable Objects/GunData_ShotGun")]
public class GunData_ShotGun : GunDataBase
{
    [Header("샷건 추가 세팅")]
    // 한번에 발사되는 총알 수
    public int _bulletCount;
    // 산탄 각도
    public float _splitAngle;
}
