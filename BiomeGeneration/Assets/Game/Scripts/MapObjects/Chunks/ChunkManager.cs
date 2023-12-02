using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;


/// <summary>
/// Manages loading the chunkPositions and using it to create chunks with the specified position and biome.
/// </summary>
public class ChunkManager : MonoBehaviour
{
    //Inspector
    //References
    [SerializeField] private Chunk chunkPrefab;
    [SerializeField] private Material _material;
    [SerializeField] private Mesh _mesh;

    [SerializeField]
    private SerializedDictionary<TileMesh, Mesh> meshDictionary = new SerializedDictionary<TileMesh, Mesh>();
    [SerializeField]
    private SerializedDictionary<Biome, Material> materialDictionary = new SerializedDictionary<Biome, Material>();

    //Variables
    public static int tileWidth=2;
    public static int tileHeight=2;
    public static int numRows;
    public static  int numCols;

    //Internal
    private Chunk[,] chunks;
    private float mapWidth;
    private float mapLength;

    void Awake()
    {
        //TODO: Remove temporary chunkPositions and LoadData function from Awake
        int[,] chunk =
        {
            { 1, 1, 1, 1 },
            { 1, 1, 1, 1 },
            { 1, 1, 1, 1 },
            { 1, 1, 1, 1 }
        };

        int[,] biomes =
        {
            { 0, 1, 2},
            { 3, 4, 5}
        };

        mapWidth = tileWidth * chunk.GetLength(0) * .30f * biomes.GetLength(0);
        mapLength = tileWidth * chunk.GetLength(1) * .20f * biomes.GetLength(1);
        
        for (int i=0; i<biomes.GetLength(0);i++)
        {
            for (int j=0; j<biomes.GetLength(1);j++)
            {
                LoadData(chunk,  new []{i,j},biomes[i,j]);
            }
        }
    }

    /// <summary>
    /// Loads the mapPosition of a given chunk
    /// </summary>
    /// <param name="mapPosition">An array where the chunks in the map</param>
    /// <param name="chunkPosition">An array containing the positioning of each
    /// of the tiles in a chunk</param>
    /// <param name="biome">The biome of the chunk</param>
    public void LoadData(int[,] mapPosition, int[] chunkPosition, int biome)
    {
        numRows = mapPosition.GetLength(0);
        numCols = mapPosition.GetLength(1);
        chunks=new Chunk[numRows,numCols];
        
        CreateChunks(mapPosition, chunkPosition,biome);
    }

    /// <summary>
    /// Creates the tiles in the specified chunk
    /// </summary>
    /// <param name="chunkPositions">The positions of tiles within the chunk</param>
    /// <param name="mapPosition">An array where the chunks in the map</param>
    /// <param name="biome">The biome of the chunk</param>
    private void CreateChunks(int[,] chunkPositions,int[] mapPosition, int biome)
    {
        for (int i = 0; i < chunks.GetLength(0); i++)
        {
            for (int j = 0; j < chunks.GetLength(1); j++)
            {
                //Calculate the position
                Vector3 pos = GetTilePosition(chunkPositions,mapPosition,i,j);
                
                //Gets the correct biome material
                Material material=materialDictionary[(Biome) biome];
                
                //Gets the needed mesh
                Mesh mesh = GetTileMesh(i, j);
                
                //Create the actual chunk
                Chunk chunk=Instantiate(chunkPrefab, transform);
                chunk.Init(pos,mesh,material);
            }
        }
    }

    /// <summary>
    /// Calculates the tile's position based on it's position in the chunk and the
    /// chunk's position in the map
    /// </summary>
    /// <param name="chunkPositions">The array of positions within the chunk</param>
    /// <param name="mapPosition">An array where the chunks in the map</param>
    /// <param name="chunkRow">The row of the tile in chunkPositions</param>
    /// <param name="chunkCol">The col of the tile in chunkPositions</param>
    /// <returns>Returns the position of the tile in the scene</returns>
    private Vector3 GetTilePosition(int[,] chunkPositions, int[] mapPosition, int chunkRow, int chunkCol)
    {
        float xPos = ((chunkRow - (mapWidth*mapPosition[0])) * tileWidth);
        float yPos = chunkPositions[chunkRow,chunkCol];
        float zPos = ((chunkCol - (mapLength*mapPosition[1])) * tileWidth);
        return new Vector3(xPos, yPos, zPos);
    }
    
    /// <summary>
    /// Returns the appropriate mesh for the tile
    /// </summary>
    /// <param name="row">The row of the tile</param>
    /// <param name="col">The col of the tile</param>
    /// <param name="isSpecial">Whether or not the tile requires a special mesh,
    /// such as a road or water</param>
    /// <returns>The appropriate mesh for the specified tile</returns>
    private Mesh GetTileMesh(int row, int col, bool isSpecial=false)
    {
        // TODO: Allow for rotation
        // TODO: Take into account tiles with differing heights
        if (isSpecial)
        {
            //TODO: Use this once landscape chunkPositions is ready
        }
        
        //If in the center
        if ((row == numRows - 1 && (col==0 || col==numCols-1)) || 
            (col == numCols - 1 && (row==0 || row==numRows-1))) {
            return meshDictionary[TileMesh.Corner];
        }
        
        //If on the edge
        if (row == numRows - 1 || col == numCols - 1)
        {
            return meshDictionary[TileMesh.Edge];
        }
        return meshDictionary[TileMesh.Center];
    }
}

public enum TileMesh
{
    Center,
    Edge,
    Corner
}

public enum MaterialType
{
    Tile,
    Path
}

public enum Biome
{
    Spring=0,
    Summer=1,
    Fall=2,
    Winter=3,
    Desert=4,
    CandyLand=5
}