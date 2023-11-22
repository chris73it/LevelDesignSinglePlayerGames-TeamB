using UnityEngine;
using UnityEngine.UI;
using System;

public class BlockButton : TileButton
{
    //new public static event Action<GameObject, bool> tileButtonClicked;

    private void Start()
    {
        icon = GetComponent<Image>();
        icon.sprite = buttonSprite;
    }

    private void OnEnable()
    {
        BuildableCounters.blockCountChange += Counters_Change; 
    }

    private void OnDisable()
    {
        BuildableCounters.blockCountChange -= Counters_Change; 
    }

    private void Counters_Change(int total, int max)
    {
        if (total == 0)
        {
            icon.color = Color.gray;
            countTMP.text = " ";
        }

        if (total > 0)
        {
            countTMP.text = total + "/" + max;
            icon.color = Color.white;
        }
    }

   
}
