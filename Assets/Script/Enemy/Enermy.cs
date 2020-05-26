using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Enermy
{
    //死亡行为
    void DeadAction();
    // 追踪行为
    void TraceAction();
    // 平常行为
    void NormalAction();

}
