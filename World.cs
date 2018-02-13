using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World  {

    //Array to hold tile data
    Tile[,] tiles;

    //Define map height and width
    public int mapWidth { get; protected set; }
    public int mapHeight { get; protected set; }


    //Initialize new instance of the World class
    public World(int width = 100, int height = 100)
    {
        mapWidth = width;
        mapHeight = height;


        tiles = new Tile[width, height];

        //Create a new tile based on map width and height
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tiles[x, y] = new Tile(this, x, y, GoldGenerator());
            }
        }

        Debug.Log("World created with " + (width * height) + " tiles.");
    }

    public float GoldGenerator()
    {
        float gold = UnityEngine.Random.Range(0, 50);
        return gold;
    }

    public void RandomizeTiles()
    {
        Debug.Log("RandomizeTiles");
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {

                if (UnityEngine.Random.Range(0, 2) == 0)
                {
                    tiles[x, y].Type = Tile.TileType.Grass;
                }
                else
                {
                    tiles[x, y].Type = Tile.TileType.Grass;
                }

            }
        }
    }


    //Gets tile data at x and y
    public Tile GetTileAt(int x, int y)
    {
        if (x > mapWidth || x < 0 || y > mapHeight || y < 0)
        {
            Debug.LogError("Tile (" + x + "," + y + ") is out of range.");
            return null;
        }
        return tiles[x, y];
    }

}
