using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudPrefab : PrefabObject
{
    public CloudPrefab(GameObject cloud) : base(cloud, new CubeBehaviour())
    {

    }
}
