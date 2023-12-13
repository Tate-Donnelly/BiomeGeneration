using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator 
{
    //Default is 8
    public int chunkSize; // Num of tiles on a given edge of a chunk
    //Default is 2
    public int numChunks; // Uses this to construct this into a square

    public Biome[,] chunkBiomes;

    public ChunkTile[,] chunks;

    public MapGenerator(int chunkSize, int numChunks){
        //Biome generation
        //ExampleScript testPerlinScript = new ExampleScript();

        this.chunkSize = chunkSize;
        this.numChunks = numChunks;

        chunkBiomes = new Biome[numChunks,numChunks];
        chunks = new ChunkTile[numChunks,numChunks];
        
        //Then set Biomes...?

        //For entire world
        ExampleScript testPerlinScript = new ExampleScript();

        
        //Create perlin texture
        
        //Run generation for each chunk
        for(int i = 0; i < numChunks; i++){
            Debug.Log(i);

            for(int j = 0; j < numChunks; j++){
                Debug.Log(j);
                chunks[i,j] = new ChunkTile(Biome.Winter, chunkSize);
                Debug.Log("Chunk Made");
                
            }
        }

    }

    //Generates biome off of the averge heat map sectors of an image that is...
    //chunkSize * numChunks wide and high.
    //I.e. a 2x2 chunkMap with a 16x16 chunkSize would need a 32x32 heatMapImage
    private int[,] biomeGeneration(){

        return null;
    }

    //Generates an individual chunk based off th given chunkSize
    //Uses information of previous chunks (if avaialble) to ...
    // make clean transitions between chunks
    // *** WILL NEED TO BE LOOPED OVER
    private int[,] chunkGeneration(){

        return null;
    }

    public class ChunkTile {

        public Biome biome;
        int chunkSize; 

        public string[,] heightMap; //Numbers and letters are used in tandem

        public char[,] objectMap; //Only chars are used for this

        ExampleScript testPerlinScript = new ExampleScript();
        
        //This is only set when you initialize since it needs it for ...
        // Literally Everything Else
        public ChunkTile(Biome biome, int chunkSize){
            this.biome = biome;
            this.chunkSize = chunkSize;

            heightMap = new string[chunkSize,chunkSize];

            setHeights();
            
            //heightMap=new string[chunkSize,chunkSize]; //Numbers and letters are used in tandem
            //objectMap=new char[chunkSize,chunkSize]; //Only chars are used for this

        }



        void setHeights(){
            for (int i = 0; i < chunkSize; i++){
                for (int j = 0; j < chunkSize; j++){
                    //if(i % 2 == 0){
                        heightMap[i,j] = ((int)(5 * Mathf.PerlinNoise(i * 0.25f, j * 0.25f))).ToString();
                        //heightMap[i,j] = j.ToString();
                    //} else {
                        //heightMap[i,j] = "0";
                    //}
                }
            }
        }

        
    }

    public class ExampleScript{
        // Width and height of the texture in pixels.
        public int pixWidth = 8;
        public int pixHeight = 8;

        // The origin of the sampled area in the plane.
        public float xOrg;
        public float yOrg;

        // The number of cycles of the basic noise pattern that are repeated
        // over the width and height of the texture.
        public float scale = 1.0F;

        private Texture2D noiseTex;
        public Color[] pix;
        //private Renderer rend = new Renderer(); // not needed due to not setting texture

        public ExampleScript(){
            // Set up the texture and a Color array to hold pixels during processing.
            noiseTex = new Texture2D(pixWidth, pixHeight);
            pix = new Color[noiseTex.width * noiseTex.height];
            CalcNoise();
            //rend.material.mainTexture = noiseTex; // Not needed due to not setting texture
        }

        void CalcNoise(){
            // For each pixel in the texture...
            float y = 0.0F;

            while (y < noiseTex.height)
            {
                float x = 0.0F;
                while (x < noiseTex.width)
                {
                    float xCoord = xOrg + x / noiseTex.width * scale;
                    float yCoord = yOrg + y / noiseTex.height * scale;
                    float sample = Mathf.PerlinNoise(xCoord, yCoord);
                    pix[(int)y * noiseTex.width + (int)x] = new Color(sample, sample, sample);
                    //Debug.Log(sample);
                    x++;
                }
                y++;
            }


            // Copy the pixel data to the texture and load it into the GPU.
            noiseTex.SetPixels(pix);
            noiseTex.Apply();
        }


    }
}