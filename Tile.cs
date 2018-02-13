using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tile
{


    //Define all possible tile types
    public enum TileType { Grass, Dirt, Water, Empty };

    private TileType _type = TileType.Empty;
    public TileType Type
    {
        get { return _type; }
        set
        {
            TileType oldType = _type;
            _type = value;
            // Call the callback and let things know we've changed.

            if (cbTileTypeChanged != null && oldType != _type)
                cbTileTypeChanged(this);
        }
    }

    World world;
    public int X { get; protected set; }
    public int Y { get; protected set; }
    public float gold;

    Action<Tile> cbTileTypeChanged;


    public Tile( World world, int x, int y, float gold)
    {
        this.world = world;
        this.X = x;
        this.Y = y;
        this.gold = gold;
    }

    /// <summary>
    /// Register a function to be called back when our tile type changes.
    /// </summary>
    public void RegisterTileTypeChangedCallback(Action<Tile> callback)
    {
        cbTileTypeChanged += callback;
    }

    /// <summary>
    /// Unregister a callback.
    /// </summary>
    public void UnegisterTileTypeChangedCallback(Action<Tile> callback)
    {
        cbTileTypeChanged -= callback;
    }

}