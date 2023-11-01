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
    public TileBase tileBase;
    [SerializeField] TagCategory tileTag; 

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = tileTag.ToString();
    }
}
