using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 执行方块对象的行为
 * 1. 检查删除
 * 2. 播放动画
 */
public class CubeActor : MonoBehaviour
{
    private bool isDestorying = false;
    

    public void DelayToFall(float time, Cube cube)
    {
        StartCoroutine(FallTimer(time, cube));
    }

    //重构
    private IEnumerator FallTimer(float time, Cube temp)
    {
        yield return new WaitForSeconds(time);
        temp.FallDown();
    }

    private IEnumerator DestoryFallingCube()
    {
        while (transform.position.y > CubeConstant.vanishingYPlane)
        {
            yield return new WaitForSeconds(CubeConstant.checkInterval);
        }
        Destroy(gameObject);
    }

    public void CheckToDestory()
    {
        if(!isDestorying)
            StartCoroutine(DestoryFallingCube());
    }
    public void DestoryCube()
    {
        Destroy(gameObject);
    }
    
    private IEnumerator DelayDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        DestoryCube();
    }

    public void DelayToDestory(float delay)
    {
        StartCoroutine(DelayDestroy(delay));
    }

    private IEnumerator Move(Vector3 interval)
    {
        while(true)
        {
            transform.position += interval;
            yield return null;
        }
    }

    public void UniformDecline(float speed, Vector3 direction)
    {
        Vector3 interval = Time.deltaTime * speed * direction;
        StartCoroutine(Move(interval));
    }

}
