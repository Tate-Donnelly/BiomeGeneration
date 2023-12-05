using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    [SerializeField] private ChunkManager _chunkManager;
    [SerializeField] int chunkSize = 16; // Num of tiles on a given edge of a chunk
    [SerializeField] int numChunks = 2; // Uses this to construct this into a square

    public Biome[,] chunkBiomes;

    public ChunkTile[,] chunks;

    void Start(){
        chunkBiomes=new Biome[numChunks,numChunks];
        chunks=new ChunkTile[numChunks,numChunks];
        //Create perlin texture
        
        //Run generation
        for(int i = 0; i < numChunks; i++){
            for(int j = 0; j < numChunks; j++){
                chunks[i,j] = new ChunkTile(Biome.Spring);
            }
        }
    }

    //Generates biome off of the averge heat map sectors of an image that is...
    //chunkSize * numChunks wide and high.
    //I.e. a 2x2 chunkMap with a 16x16 chunkSize would need a 32x32 heatMapImage
    private int[][] biomeGeneration(){

        return null;
    }

    //Generates an individual chunk based off th given chunkSize
    //Uses information of previous chunks (if avaialble) to ...
    // make clean transitions between chunks
    // *** WILL NEED TO BE LOOPED OVER
    private int[][] chunkGeneration(){

        return null;
    }

    public class ChunkTile {

        Biome biome;
        int chunkSize; 

        string[,] heightMap; //Numbers and letters are used in tandem

        char[,] objectMap; //Only chars are used for this


        //This is only set when you initialize since it needs it for ...
        // Literally Everything Else
        public ChunkTile(Biome biome){
            this.biome = biome;
            setHeights();
            heightMap=new string[chunkSize,chunkSize]; //Numbers and letters are used in tandem
            objectMap=new char[chunkSize,chunkSize]; //Only chars are used for this
        }



        void setHeights(){
            for (int i = 0; i < chunkSize; i++){
                for (int j = 0; j < chunkSize; j++){
                    if(i % 2 == 0){
                        heightMap[i,j] = "2";
                    } else {
                        heightMap[i,j] = "0";
                    }
                }
            }
        }
    }
}