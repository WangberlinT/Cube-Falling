

using UnityEngine;

public interface EnermySubject
{
    //怪物死了，通知World
    void OnDie();
    //怪物生成，通知World
    void OnCreate();

    Vector3 GetPosition();
    
}
