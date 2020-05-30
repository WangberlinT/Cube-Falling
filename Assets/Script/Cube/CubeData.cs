using UnityEngine;
[System.Serializable]
public class CubeData
{
    public bool isFalling;
    //初始位置，所有方块的初始位置和属性在World类中记录
    public float[] initPos = new float[3];
    //Cube 的种类(Stone,Sand...)
    public CubeType cubeType;

    public CubeData(Cube cube)
    {
        isFalling = cube.GetIsFalling();
        Vector3 pos = cube.GetInitPosition();
        initPos[0] = pos.x;
        initPos[1] = pos.y;
        initPos[2] = pos.z;
        cubeType = GetCubeType(cube);
    }

    /*
     * Cube type 转换为存储的index
     */
    public static CubeType GetCubeType(Cube cube)
    {
        if (cube is Stone)
            return CubeType.Stone;
        else if (cube is Sand)
            return CubeType.Sand;
        else if (cube is Ice)
            return CubeType.Ice;
        else
            return CubeType.Air;
    }
}

