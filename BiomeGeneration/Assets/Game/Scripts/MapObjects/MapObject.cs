using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : MonoBehaviour
{
    [SerializeField] protected MeshRenderer _meshRenderer;
    [SerializeField] protected MeshFilter _meshFilter;

    public void Init(Mesh mesh, Material material)
    {
        _meshFilter.mesh = mesh;
        _meshRenderer.material = material;
    }
}
