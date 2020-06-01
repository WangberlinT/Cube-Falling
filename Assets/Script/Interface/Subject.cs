

using UnityEngine;

public interface MonsterSubject
{
    //怪物死了，通知World
    void OnDie();
    //怪物生成，通知World
    void OnCreate();

    void Delete();

    Vector3 GetPosition();  // 获得当前位置
    Vector3 GetFallPosition();  // 获得爆炸半径
    Vector3 GetDestination();   // 获得目的地
    int GetFallRange();
}
