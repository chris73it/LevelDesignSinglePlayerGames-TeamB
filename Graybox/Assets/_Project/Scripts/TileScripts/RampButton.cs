using UnityEngine;
using UnityEngine.UI;
using System;

public class RampButton : TileButton
{
    //public event Action<GameObject, bool> tileButtonClicked;
    
    private void Start()
    {
     
    }

    private void Awake() {
        icon = GetComponent<Image>();
        icon.sprite = buttonSprite;
    }
    private void OnEnable()
    {
        BuildableCounters.rampCountChange += Counters_Change;
    }

    private void OnDisable()
    {
        BuildableCounters.rampCountChange -= Counters_Change;
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

    /*public void PlaceModeInvoked()
    {
        Debug.Log("ramp invoked"); 
        tileButtonClicked?.Invoke(itemForButton, isEmpty) ;
    }
    */
}