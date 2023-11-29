using UnityEngine;
using UnityEngine.UI;

public class RampButton : TileButton
{
    private void Awake()
    {
        frame = GetComponent<Image>();
        icon = innerImage.GetComponentInChildren<Image>();
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
        if (total <= 0)
        {
            icon.color = Color.gray;
            frame.color = Color.gray;
            countTMP.text = " ";
        }

        if (total > 0)
        {
            countTMP.text = total + "/" + max;
            icon.color = Color.white;
            frame.color = Color.white; 
        }
    }
}