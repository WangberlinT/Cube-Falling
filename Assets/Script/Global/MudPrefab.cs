using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudPrefab : PrefabObject
{
    public MudPrefab(GameObject mud) : base(mud, new CubeBehaviour())
    {

    }
}
