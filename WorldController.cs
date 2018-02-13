using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{

    public static WorldController Instance { get; protected set; }

    // The only tile sprite we have right now, so this
    // it a pretty simple way to handle it.

    [SerializeField]
    public Sprite[] cutSprites;
    public Sprite grass_;
    public Sprite dirt_;
    public Sprite cut_grass_N;

    // The world and tile data
    public World World { get; protected set; }

    void Start()
    {
        if (Instance != null)
        {
            Debug.LogError("There should never be two world controllers.");
        }
        Instance = this;

        // Create a world with Empty tiles
        World = new World();

        // Create a GameObject for each of our tiles, so they show visually. (and redunt reduntantly)
        for (int x = 0; x < World.mapWidth; x++)
        {
            for (int y = 0; y < World.mapHeight; y++)
            {
                // Get the tile data
                Tile tile_data = World.GetTileAt(x, y);

                // This creates a new GameObject and adds it to our scene.
                GameObject tile_go = new GameObject();
                tile_go.name = "Tile_" + x + "_" + y;
                tile_go.transform.position = new Vector3(tile_data.X, tile_data.Y, 0);
                tile_go.transform.SetParent(this.transform, true);
                

                // Add a sprite renderer, but don't bother setting a sprite
                // because all the tiles are empty right now.
                tile_go.AddComponent<SpriteRenderer>();
                

                // Use a lambda to create an anonymous function to "wrap" our callback function
                tile_data.RegisterTileTypeChangedCallback((tile) => { OnTileTypeChanged(tile, tile_go); });

            }
        }

        // Shake things up, for testing.
        World.RandomizeTiles();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // This function should be called automatically whenever a tile's type gets changed.
    void OnTileTypeChanged(Tile tile_data, GameObject tile_go)
    {

        if (tile_data.Type == Tile.TileType.Grass)
        {
            tile_go.GetComponent<SpriteRenderer>().sprite = grass_;
        }
        else if (tile_data.Type == Tile.TileType.Dirt)
        {
            
            tile_go.GetComponent<SpriteRenderer>().sprite = dirt_;
            //checkNeighbors(tile_data, tile_go);
        }
        else
        {
            Debug.LogError("OnTileTypeChanged - Unrecognized tile type.");
            Debug.Log(tile_data.Type);
        }
        

    }

    /// <summary>
    /// Gets the tile at the unity-space coordinates
    /// </summary>
    /// <returns>The tile at world coordinate.</returns>
    /// <param name="coord">Unity World-Space coordinates.</param>
    public Tile GetTileAtWorldCoord(Vector3 coord)
    {
        int x = Mathf.FloorToInt(coord.x);
        int y = Mathf.FloorToInt(coord.y);

        return World.GetTileAt(x, y);
    }

    /*public void checkNeighbors(Tile tile_data, GameObject tile_go)
    {
        string composition = string.Empty;
        

        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                if (x != 0 || y != 0)
                {
                    Vector3Int nPos = new Vector3Int(tile_data.X + x, tile_data.Y + y, 0);

                    Tile nData = WorldController.Instance.World.GetTileAt(nPos.x, nPos.y);

                   // GameObject nSprite = GameObject.Find("Tile_" + nPos.x + "_" + nPos.y);

                

                    if (nData.Type == Tile.TileType.Grass)
                    {
                        composition += "G";
                    }
                    else if (nData.Type == Tile.TileType.Dirt){
                        composition += "D";
                    }
                    //nSprite.GetComponent<SpriteRenderer>().sprite = cut_grass_N;
                    //Debug.Log(composition);
                    
                    




                    if (nData.Type == Tile.TileType.Grass)
                    {
                       // tile_go.GetComponent<SpriteRenderer>().sprite = dirt_;
                       // Debug.Log("Sprite set to: " + tile_go.GetComponent<SpriteRenderer>().sprite + 
                       //     "For tile X: " + nPos.x + "Y: " + nPos.y);
                    }


                    Debug.Log(nPos);
                    Debug.Log(nData.Type);
                    //Debug.Log(tile_go.transform);
                    



                }
            }
        }
        
       
        
    }*/

    public void changeTiles(string composition){
        string neighborComp = composition;

        Debug.Log(neighborComp);
    }
}






            
    

