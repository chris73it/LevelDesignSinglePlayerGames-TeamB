using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum Category {
    Wall,
    Floor
}

[CreateAssetMenu (fileName ="Buildable", menuName ="BuildingObjects/Create Buildable")]
public class TilePlacement : ScriptableObject
{
    // Start is called before the first frame update
    [SerializeField] Category category;
    [SerializeField] TileBase tileBase;
    public TileBase TileBase {
        get {
            return tileBase;
        }
    }

    public Category Category {
        get { 
            return category; 
        }
    }

}
