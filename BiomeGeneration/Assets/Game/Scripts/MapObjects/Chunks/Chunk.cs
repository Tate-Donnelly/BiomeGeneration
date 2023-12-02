using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MapObject
{
    //0 is left of the chunk, 1 is above the chunk, 2 is to the right of the chunk, 3 is below the chunk
    private Chunk[] nearbyChunks = new Chunk[4];
    private MapObject mapObject;

    public void Init(Chunk[] nearbyChunks, MapObject mapObject, Vector3 position, Mesh mesh, Material material)
    {
        base.Init(mesh, material);
        this.nearbyChunks = nearbyChunks;
        this.mapObject = mapObject;
        transform.position = position;
        InstantiateObject();
    }
    
    public void Init(Vector3 position, Mesh mesh, Material material)
    {
        base.Init(mesh, material);
        transform.position = position;
        InstantiateObject();
    }

    private void InstantiateObject()
    {
        if (mapObject != null)
        {
            Vector3 position = transform.position;
            MapObject environmentObject =Instantiate(mapObject);
            environmentObject.transform.position =
                new Vector3(position.x, position.y + ChunkManager.tileHeight, position.z);
        }
    }
    
}
