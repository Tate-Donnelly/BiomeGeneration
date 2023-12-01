using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    [SerializeField] private Chunk chunk;
    [SerializeField] private Material _material;
    [SerializeField] private Mesh _mesh;

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            chunk.Init(_mesh,_material);
        }
    }
}
