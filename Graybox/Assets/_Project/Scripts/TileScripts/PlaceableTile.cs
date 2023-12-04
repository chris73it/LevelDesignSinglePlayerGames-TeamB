using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TagCategory
{
    //tag to use for walls and ground blocks
    Solid,
    //tag to use for objects that affect player
    Powerup
}


public class PlaceableTile : MonoBehaviour
{
    public bool IsPlayerPlaced = false;
    public TileBase tileBase;
}
