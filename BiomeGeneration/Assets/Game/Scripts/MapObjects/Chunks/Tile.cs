using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MapObject
{
    //0 is left of the chunk, 1 is above the chunk, 2 is to the right of the chunk, 3 is below the chunk
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
        InstantiateProp();
    }

    /// <summary>
    /// Instantiate prop and use the given material
    /// </summary>
    private void InstantiateProp()
    {
        Vector3 position = transform.position;
        MapObject environmentObject =Instantiate(mapObjectPrefab);
        environmentObject.Init(propMesh,base._meshRenderer.material);
        environmentObject.transform.position =
            new Vector3(position.x, position.y, position.z);
    }
    
}
