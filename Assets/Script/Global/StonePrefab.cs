using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonePrefab : PrefabObject
{
    PrefabBehaviour behaviour;
    public StonePrefab(GameObject stone):base(stone,new CubeBehaviour())
    {
        
    }

    
}
