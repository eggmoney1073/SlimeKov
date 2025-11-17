using UnityEngine;
using DefineEnums;

[CreateAssetMenu(fileName = "GunDataBase", menuName = "Scriptable Objects/GunDataBase")]
public abstract class GunDataBase : ScriptableObject
{
    [Header("기본 총기 세팅")]
    // 총기 타입
    public Gun_Type _type = Gun_Type.None;
    // 연사 속도
    public float _shootRate = 1f;
    // 재장전 시간
    public float _reloadTime = 1f;
    // 연발 가능여부
    public bool _isAuto = false;

    [Header("총알 세팅")]
    // 총알 타입
    public Bullet_Type _bulletType = Bullet_Type.None;
    // 탄창 수
    public int _maxAmmoCapacity = 10;
    // 총알 속도
    public float _bulletSpeed = 5f;
    // 사거리
    public float _range = 10f;
    // 대미지
    public float _damage = 10f;
}
