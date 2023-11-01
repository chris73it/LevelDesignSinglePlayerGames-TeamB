using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class TileButton : MonoBehaviour
{
    [SerializeField] int levelLimit;
    [SerializeField] GameObject itemForButton;
    [SerializeField] Sprite buttonSprite; 
    private bool onCurrentLevel = false;

    private Image icon; 

    private void Start()
    {
        icon = this.GetComponent<Image>();
        icon.sprite = buttonSprite;

        if (levelLimit >= 1)
        {
            onCurrentLevel = true;
        }

      if (!onCurrentLevel)
        {
            icon.color = Color.gray; 
        }
    }
}
