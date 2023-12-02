using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Manages loading the data and using it to create chunks with the specified position and biome.
/// </summary>
public class ChunkManager : MonoBehaviour
{
    //Inspector
    //References
    [SerializeField] private Chunk chunkPrefab;
    [SerializeField] private Material _material;
    [SerializeField] private Mesh _mesh;
    
    //Variables
    public static int tileWidth=2;
    public static int tileHeight=2;

    //Internal
    private int numRows;
    private int numCols;
    
    private Chunk[,] chunks;

    void Awake()
    {
        //TODO: Remove temporary data and LoadData function from Awake
        int[,] data = { 
            {1,1,1,1,1,2,3},
            {1,1,1,1,1,2,2},
            {1,1,1,1,1,1,1},
            {1,1,1,1,1,1,2},
            {1,1,1,1,1,2,2},
            {3,2,1,1,1,1,2}
        };
        LoadData(data);
    }

    public void LoadData(int[,] data)
    {
        numRows = data.GetLength(0);
        numCols = data.GetLength(1);
        print($"Rows:{numRows} Cols:{numCols}");
        chunks=new Chunk[numRows,numCols];

        SetChunks(data);
        CreateChunks();
    }

    private void SetChunks(int[,] data)
    {
        for (int i = 0; i < numRows; i++)
        {
            for (int j = 0; j < numCols; j++)
            {
                chunks[i,j] = new Chunk();
                chunks[i, j].height = data[i, j];
            }
        }
    }

    private void CreateChunks()
    {
        float halfChunkWidth = tileWidth * numRows * .5f;
        for (int i = 0; i < chunks.GetLength(0); i++)
        {
            for (int j = 0; j < chunks.GetLength(1); j++)
            {
                Chunk chunk=Instantiate(chunkPrefab, transform);
                float xPos = ((i - halfChunkWidth) * tileWidth);
                float zPos = ((j - halfChunkWidth) * tileWidth);
                chunk.transform.position=new Vector3(xPos,chunks[i,j].height-1,zPos);
            }
        }
    }
}
