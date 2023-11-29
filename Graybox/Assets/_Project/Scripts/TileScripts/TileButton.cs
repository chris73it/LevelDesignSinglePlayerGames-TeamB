using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public abstract class TileButton : MonoBehaviour
{
    //[SerializeField] int levelLimit;
    public GameObject itemForButton;
    public GameObject innerImage;
    public TMP_Text countTMP;
    [SerializeField] Button tileButton;

    //public static event Action noMoreTiles;
    //public static event Action tilesReplinished; 
    public static event Action<GameObject> tileButtonClicked;

    [HideInInspector] public Image frame;
    [HideInInspector] public Image icon;

    public void PlaceModeInvoked()
    {
        tileButtonClicked?.Invoke(itemForButton);
    }


}