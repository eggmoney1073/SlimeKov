using UnityEngine;
using System.Runtime.CompilerServices;

public class BulletCalculator
{
    /// <summary>
    /// 총알의 물리 연산 계산기
    /// </summary>
    /// <param name="currentBulletData">현재 BulletData </param>
    /// <returns>물리 연산 후 BulletData </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Calculate_BulletPosition(ref BulletManager.BulletData currentBulletData)
    {
        currentBulletData.prev_position = currentBulletData.position;
        currentBulletData.position += currentBulletData.speed * currentBulletData.direction.normalized * Time.deltaTime;
        currentBulletData.remainLifeTime -= Time.deltaTime;
    }
}
