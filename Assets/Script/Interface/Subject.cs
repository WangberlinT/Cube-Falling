

using UnityEngine;

public interface MonsterSubject
{
    //怪物死了，通知World
    void OnDie();
    //怪物生成，通知World
    void OnCreate();

    void Delete();

    Vector3 GetPosition();
    
}
