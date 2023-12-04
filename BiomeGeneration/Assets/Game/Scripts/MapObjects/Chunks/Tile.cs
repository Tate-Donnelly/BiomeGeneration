using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MapObject
{
    //0 is left of the chunk, 1 is above the chunk, 2 is to the right of the chunk, 3 is below the chunk
    private Tile[] nearbyChunks = new Tile[4];
    private MapObject mapObject;
    
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