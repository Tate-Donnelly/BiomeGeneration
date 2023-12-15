using AYellowpaper.SerializedCollections;
using UnityEngine;
using System;
/// <summary>
/// Manages loading the chunkPositions and using it to create chunks with the specified position and biome.
/// </summary>
public class ChunkManager : MonoBehaviour
{
    //Inspector
    //References
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private MapObject _propPrefab;
    [SerializeField]
    private SerializedDictionary<TileMesh, Mesh> meshDictionary = new SerializedDictionary<TileMesh, Mesh>();
    [SerializeField]
    private SerializedDictionary<PropType, Mesh> propMeshDictionary = new SerializedDictionary<PropType, Mesh>();
    [SerializeField]
    private SerializedDictionary<Biome, Material> materialDictionary = new SerializedDictionary<Biome, Material>();
    private MapGenerator mapGen;

    //Variables
    public static int tileWidth=2;
    public static int tileHeight=2;
    public static int numRows;
    public static  int numCols;

    //Internal
    private Tile[,]chunks;
    private float mapWidth;
    private float mapLength;
    
    void Awake()
    {
        
        //TODO: Remove temporary chunkPositions and LoadData function from Awake
        string[,] chunk =
        {
            { "2","1","1lh","1cb" },
            { "1tr","1","0","1tl" },
            { "1lv","2t","3t","1lv" },
            { "1cd","1td","1lh","1cc" },
        };

        int[,] biomes =
        {
            { 0, 1, 2},
            { 3, 4, 5}
        };
        
        //First number is chunk size
        // Second is n x n num of chunks
        mapGen = new MapGenerator(8, 7);
        

        // mapWidth = tileWidth * mapGen.chunkSize * (biomes.GetLength(0)/100) * biomes.GetLength(0);
        // mapLength = tileWidth * mapGen.chunkSize * (biomes.GetLength(1)/100) * biomes.GetLength(1);

        //  mapWidth = tileWidth * chunk.GetLength(0) * .30f * biomes.GetLength(0);
        // mapLength = tileWidth * chunk.GetLength(1) * .20f * biomes.GetLength(1);

        mapWidth = tileWidth * mapGen.chunkSize * 0.5f;
        mapLength = tileWidth * mapGen.chunkSize * 0.5f;

        // mapWidth = tileWidth * mapGen.chunkSize * (mapGen.chunkSize/100) * biomes.GetLength(0);
        // mapLength = tileWidth * mapGen.chunkSize * (mapGen.chunkSize/100) * biomes.GetLength(1);

        
        for (int i = 0; i < mapGen.numChunks; i++)
        {
            for (int j = 0; j < mapGen.numChunks; j++)
            {
                // This is where the biomes need to be generated
                //print(mapGen.chunks[0,0].heightMap);
                //Debug.Log((mapGen.chunks[i,j] == null) + " " + i.ToString() + " " + j.ToString());
                int[] loc = {i,j};

                LoadData(mapGen.chunks[i,j].heightMap, loc, mapGen.chunkBiomes[i,j]);
                Debug.Log(loc[0] + " " + loc[1]);


            }
        }
    }

    /// <summary>
    /// Loads the mapPosition of a given tile
    /// </summary>
    /// <param name="tileType">An array where the chunks in the map</param>
    /// <param name="chunkPosition">An array containing the positioning of each
    /// of the tiles in a tile</param>
    /// <param name="biome">The biome of the tile</param>
    public void LoadData(string[,] tileType, int[] chunkPosition, int biome)
    {
        numRows = tileType.GetLength(0);
        numCols = tileType.GetLength(1);
        chunks = new Tile[numRows,numCols];
        
        //Renders the chunk
        CreateChunks(tileType, chunkPosition, biome);
    }

    /// <summary>
    /// Get mesh that corresponds to the given char
    /// </summary>
    /// <param name="prop">The char that corresponds to a prop</param>
    /// <returns>The mesh needed for the given prop</returns>
    private Mesh GetPropMesh(char prop)
    {
        switch (prop)
        {
            case 'c': return propMeshDictionary[PropType.Cabin];
            case 'f': return propMeshDictionary[PropType.Flower];
            case 'g': return propMeshDictionary[PropType.Grass];
            case 'r': return propMeshDictionary[PropType.Rock];
            case 't': return propMeshDictionary[PropType.Tree];
            case 's': return propMeshDictionary[PropType.Stump];
            case 'b' or _: return propMeshDictionary[PropType.FlatRock];
        }
    }

    /// <summary>
    /// Creates the tiles in the specified tile
    /// </summary>
    /// <param name="chunkPositions">The positions of tiles within the tile</param>
    /// <param name="mapPosition">An array where the chunks in the map</param>
    /// <param name="biome">The biome of the tile</param>
    private void CreateChunks(string[,] chunkPositions, int[] mapPosition, int biome)
    {
        // actual chunk creation

        for (int i = 0; i < chunks.GetLength(0); i++)
        {
            for (int j = 0; j < chunks.GetLength(1); j++)
            {

                string tileInfo = chunkPositions[i,j];
                int height = Int32.Parse(tileInfo[0].ToString());;
                //Calculate the position
                Vector3 pos = GetTilePosition(height,mapPosition,i,j);
                
                //Gets the correct biome material
                Material material = materialDictionary[(Biome) biome];
                
                //Gets the needed mesh
                Mesh mesh = GetTileMesh(chunkPositions[i, j]);

                //Create the actual tile
                InstantiateTiles(chunkPositions[i, j],pos,mesh,material);
            }
        }
    }

    /// <summary>
    /// Uses the given info to instantiate the tile
    /// </summary>
    /// <param name="tileInfo">The string of the title</param>
    /// <param name="pos">The position of the tile</param>
    /// <param name="mesh">The tile's mesh</param>
    /// <param name="material">The tile's material</param>
    private void InstantiateTiles(string tileInfo, Vector3 pos, Mesh mesh, Material material)
    {
        Tile tile;
        if (tileInfo.Length ==3)
        {
            Quaternion quat = RotateTile(tileInfo[1].ToString(), tileInfo[2].ToString());
            tile=Instantiate(_tilePrefab, pos, quat );
            tile.transform.parent = transform;
        }
        else
        {
            tile=Instantiate(_tilePrefab, transform);
        }

        if (tileInfo.Length == 2)
        {
            Mesh propMesh = GetPropMesh(tileInfo[1]);
            tile.Init(pos,mesh,material,propMesh);
        }
        else
        {
            tile.Init(pos,mesh,material);
        }
    }

    /// <summary>
    /// Calculates the tile's position based on it's position in the tile and the
    /// tile's position in the map
    /// </summary>
    /// <param name="height">The height of the tile</param>
    /// <param name="mapPosition">An array where the chunks in the map</param>
    /// <param name="tileRow">The row of the tile in chunkPositions</param>
    /// <param name="tileCol">The col of the tile in chunkPositions</param>
    /// <returns>Returns the position of the tile in the scene</returns>
    private Vector3 GetTilePosition(int height, int[] mapPosition, int tileRow, int tileCol){
        float xPos = ((tileRow - (mapWidth * mapPosition[0])) * tileWidth);
        float yPos = height-tileHeight-2;
        yPos = Mathf.Clamp(yPos, -tileHeight, 10);
        float zPos = ((tileCol - (mapLength * mapPosition[1])) * tileWidth);
        return new Vector3(xPos, yPos, zPos);
    }
    
    /// <summary>
    /// Returns the appropriate mesh for the tile
    /// </summary>
    /// <param name="tileInfo">The row of the tile</param>
    /// <returns>The appropriate mesh for the specified tile</returns>
    private Mesh GetTileMesh(string tileInfo)
        {
            int height=Int32.Parse(tileInfo[0].ToString());

            if (tileInfo.Length==3)
            {
                string topographyType = tileInfo[1].ToString();
                return GetPathMesh(topographyType);
            }

            if (height == 0)
            {
                return meshDictionary[TileMesh.Water];
            }

            return meshDictionary[TileMesh.Center];
        }

    /// <summary>
    /// Gets the path mesh that corresponds with the given pathType
    /// </summary>
    /// <param name="pathType">The type of the path</param>
    /// <returns>The mesh of the needed path</returns>
    private Mesh GetPathMesh(string pathType)
    {
        if (pathType == "c")
        {
            return meshDictionary[TileMesh.CornerPath];
        }
        if (pathType == "t")
        {
            return meshDictionary[TileMesh.ThreeWayPath];
        }
        return meshDictionary[TileMesh.StraightPath];
    }

    /// <summary>
    /// Rotates the tile based on the type and given direction
    /// </summary>
    /// <param name="type">The type of tile (mesh of the tile)</param>
    /// <param name="direction">The direction the tile should be oriented</param>
    /// <returns>A Quaternion with the necessary rotation</returns>
    private Quaternion RotateTile(string type,string direction)
    {
        if (type == "c")
        {
            return RotateCorner(direction);
        }
        if (type == "l")
        {
            Quaternion quat = RotateLine(direction);
            return quat;
        }

        if (type == "t")
        {
            return RotateMultiplePaths(direction);
        }
        return Quaternion.identity;
    }

    /// <summary>
    /// Rotates the tile based on the given direction
    /// </summary>
    /// <param name="direction">The direction the tile should be oriented</param>
    /// <returns>A Quaternion with the necessary rotation</returns>
    private Quaternion RotateMultiplePaths(string direction)
    {
        float y=0;
        if (direction == "t")
        {
            y = 90;
        }else if (direction == "l")
        {
            y = 180;
        }else if (direction == "d")
        {
            y = -90;
        }
        return Quaternion.Euler(0,y,0);
    }
    
    /// <summary>
    /// Rotates the line tile based on the given direction
    /// </summary>
    /// <param name="direction">The direction the tile should be oriented</param>
    /// <returns>A Quaternion with the necessary rotation</returns>
    private Quaternion RotateLine(string direction)
    {
        float y=0;
        if (direction == "v")
        {
            y = 90;
        }
        Quaternion quat = Quaternion.Euler(0,y,0);
        return quat;
    }

    /// <summary>
    /// Rotates the corner tile based on the given direction
    /// </summary>
    /// <param name="direction">The direction the tile should be oriented</param>
    /// <returns>A Quaternion with the necessary rotation</returns>
    private Quaternion RotateCorner(string direction)
    {
        float y=0;
        if (direction == "a")
        {
            y = -90f;
        }else if (direction == "c")
        {
            y = 90;
        }else if (direction == "d")
        {
            y = 180;
        }
        return Quaternion.Euler(0,y,0);
    }
}

public enum TileMesh
{
    Center,
    Edge,
    Corner,
    Water,
    CornerPath,
    StraightPath,
    ThreeWayPath
}

public enum PropType
{
    Cabin,
    Flower,
    Grass,
    Rock,
    Tree,
    Stump,
    FlatRock
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