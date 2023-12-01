using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MapObject
{
    //0 is left of the chunk, 1 is above the chunk, 2 is to the right of the chunk, 3 is below the chunk
    private Chunk[] nearbyChunks = new Chunk[4];

    public void Init(Chunk[] nearbyChunks, Mesh mesh, Material material)
    {
        base.Init(mesh, material);
        this.nearbyChunks = nearbyChunks;
    }
    
}
