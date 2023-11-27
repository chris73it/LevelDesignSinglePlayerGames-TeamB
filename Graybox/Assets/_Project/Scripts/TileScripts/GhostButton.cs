using UnityEngine.UI;
using UnityEngine;

public class GhostButton : TileButton
{
    private void Awake()
    {
        frame = GetComponent<Image>();
        icon = innerImage.GetComponentInChildren<Image>();
    }

    private void OnEnable()
    {
        BuildableCounters.wallsCountChange += Counters_Change;
    }

    private void OnDisable()
    {
        BuildableCounters.wallsCountChange -= Counters_Change;
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