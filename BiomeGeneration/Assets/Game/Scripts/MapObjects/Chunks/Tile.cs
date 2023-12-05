using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MapObject
{
    //0 is left of the chunk, 1 is above the chunk, 2 is to the right of the chunk, 3 is below the chunk
    private Tile[] nearbyChunks = new Tile[4];
    [SerializeField] private MapObject mapObjectPrefab;
    private Mesh propMesh;
    
    public void Init(Vector3 position, Mesh mesh, Material material)
    {
        base.Init(mesh, material);
        transform.position = position;
    }
    
    public void Init(Vector3 position, Mesh mesh, Material material, Mesh propMesh)
    {
        base.Init(mesh, material);
        transform.position = position;
        this.propMesh = propMesh;
        InstantiateProp(material);
    }

    private void InstantiateProp(Material material)
    {
        Vector3 position = transform.position;
        MapObject environmentObject =Instantiate(mapObjectPrefab);
        environmentObject.Init(propMesh,material);
        environmentObject.transform.position =
            new Vector3(position.x, position.y, position.z);
    }
    
}
