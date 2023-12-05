using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    [SerializeField] int chunkSize = 16; // Num of tiles on a given edge of a chunk
    [SerializeField] int numChunks = 2; // Uses this to construct this into a square


    public enum BiomeTypes{

        Spring = 0,
        Summer = 1,
        Fall = 2,
        Winter = 3,
        Desert = 4,
        CandyLand = 5;
        
    }

    public BiomeTypes chunkBiomes[numChunks][numChunks];

    public ChunkTile chunks[numChunks][numChunks];

    void Start(){
        //Create perlin texture
        
        //Run generation
        for(int i = 0; i < numChunks; i++;){
            for(int j = 0; j < numChunks; j++){
                chunkBiomes[i][j] = new ChunkTile(BiomeTypes.Spring);

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

    class ChunkTile {

        BiomeTypes biome;
        //int chunkSize; 

        string heightMap[this.chunkSize][this.chunkSize]; //Numbers and letters are used in tandem

        char objectMap[this.chunkSize][this.chunkSize]; //Only chars are used for this


        //This is only set when you initialize since it needs it for ...
        // Literally Everything Else
        public ChunkTile(BiomeTypes biome){
            this.biome = biome;
            setHeights();

        }



        void setHeights(){
            for (int i = 0; i < this.chunkSize; i++){
                for (int j = 0; j < this.chunkSize; j++){
                    if(i % 2 == 0){
                        string heightMap[i][j] = 2;
                    } else {
                        string heightMap[i][j] = 0;

                    }

                }
                
            }




        }



       




    }







}