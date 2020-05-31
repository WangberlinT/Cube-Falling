using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereBehaviour : PrefabBehaviour
{
    public void RemoveCollider(GameObject g)
    {
        g.GetComponent<SphereCollider>().enabled = false;
    }

    public void RemovePhysicsComponent(GameObject g)
    {
        GameObject.Destroy(g.GetComponent<Rigidbody>());
    }
}
