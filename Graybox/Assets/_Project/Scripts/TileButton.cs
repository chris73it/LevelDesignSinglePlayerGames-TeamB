using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TileButton : MonoBehaviour
{
    [SerializeField] int levelLimit;
    [SerializeField] GameObject itemForButton;
    [SerializeField] Sprite buttonSprite;
    [SerializeField] TMP_Text countTMP; 
    private bool onCurrentLevel = false;

    private Image icon;
    private int currentTotal;
    private string labelText; 


    private void Awake()
    {

        if (levelLimit >= 1)
        {
            onCurrentLevel = true;
        }

        currentTotal = levelLimit;
        labelText = currentTotal + "/" + levelLimit;
    }

    private void Start()
    {
        icon = this.GetComponent<Image>();
        icon.sprite = buttonSprite;

        if (!onCurrentLevel)
        {
            icon.color = Color.gray;
            labelText = " ";
        }
    }

    private void Update()
    {
        countTMP.text = labelText;
    }
}
