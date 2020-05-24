using UnityEngine;
[System.Serializable]
public class WorldData
{
    public int worldWidth;
    public int worldHeight;
    public float[] spawnPos = new float[3];
    public CubeData[,,] cubeDatas;
    public WorldData(World world)
    {
        worldWidth = world.worldWidth;
        worldHeight = world.worldHeight;
        Vector3 pos = world.spawnPos;
        spawnPos[0] = pos.x;
        spawnPos[1] = pos.y;
        spawnPos[2] = pos.z;
        //处理Cube数据的存储
        Cube[,,] cubes = world.GetCubes();
        cubeDatas = new CubeData[worldWidth, worldHeight, worldWidth];
        for(int x = 0;x < worldWidth;x ++)
        {
            for(int y = 0;y < worldHeight; y ++)
            {
                for(int z = 0; z < worldWidth; z ++)
                {
                    if(cubes[x,y,z] != null)
                        cubeDatas[x, y, z] = new CubeData(cubes[x, y, z]);
                }
            }
        }
    }

    public Vector3 GetSpawnPos()
    {
        return new Vector3(spawnPos[0], spawnPos[1], spawnPos[2]);
    }

}
