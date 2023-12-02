using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : MonoBehaviour
{
    public MeshRenderer _meshRenderer;
    public MeshFilter _meshFilter;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshFilter = GetComponent<MeshFilter>();
    }

    public void Init(Mesh mesh, Material material)
    {
        _meshFilter.mesh = mesh;
        _meshRenderer.material = material;
    }
}
