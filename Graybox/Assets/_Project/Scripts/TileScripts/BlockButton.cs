using UnityEngine;
using UnityEngine.UI;

public class BlockButton : TileButton
{
    //new public static event Action<GameObject, bool> tileButtonClicked;

    private void Awake()
    {
        frame = GetComponent<Image>();
        icon = innerImage.GetComponentInChildren<Image>();
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
        if (total <= 0)
        {
            frame.color = Color.gray;
            icon.color = Color.gray;
            countTMP.text = " ";
        }

        if (total > 0)
        {
            countTMP.text = total + "/" + max;
            frame.color = Color.white;
            icon.color = Color.white; 
        }
    }
}
