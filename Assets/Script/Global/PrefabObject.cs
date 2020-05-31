using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabObject
{
    protected GameObject prefab;
    protected PrefabBehaviour behaviour;

    public PrefabObject(GameObject prefab, PrefabBehaviour behaviour)
    {
        this.prefab = prefab;
        this.behaviour = behaviour;
    }

    public GameObject GetGameObject()
    {
        return prefab;
    }

    public GameObject GetPlaceModel()
    {
        GameObject prefabCopy = GameObject.Instantiate(prefab);
        behaviour.RemoveCollider(prefabCopy);
        behaviour.RemovePhysicsComponent(prefabCopy);
        return prefabCopy;
    }
}
