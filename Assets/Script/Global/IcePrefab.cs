using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePrefab : PrefabObject
{
    public IcePrefab(GameObject ice) : base(ice, new CubeBehaviour())
    {

    }
}
