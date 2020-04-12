using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTest : MonoBehaviour
{
    public float total_degree = 90;
    void Start()
    {
        Debug.Log("init vector:" + transform.eulerAngles.y);
        transform.eulerAngles = new Vector3(0, total_degree, 0);
        Debug.Log("temp vector:" + transform.eulerAngles.y);
    }
}
