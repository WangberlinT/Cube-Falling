using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehaviour : PrefabBehaviour
{
    public void RemoveCollider(GameObject g)
    {
        GameObject.Destroy(g.GetComponent<BoxCollider>());
    }

    public void RemovePhysicsComponent(GameObject g)
    {
        GameObject.Destroy(g.GetComponent<Rigidbody>());
    }
}
