using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class TileButton : MonoBehaviour
{
    [SerializeField] int levelLimit;
    [SerializeField] GameObject itemForButton;
    [SerializeField] Sprite buttonSprite;
    [SerializeField] TMP_Text countTMP;
    [SerializeField] Button tileButton;

    public static event Action noMoreTiles;
    public static event Action tilesReplinished; 
    public static event Action<GameObject> tileButtonClicked;

    private Image icon;
    private int currentTotal;
    

    private void Awake()
    {
        currentTotal = levelLimit;
    }

    private void Start()
    {
        icon = this.GetComponent<Image>();
        icon.sprite = buttonSprite;

        tileButton.onClick.AddListener(placeModeInvoked);
        countTMP.text = currentTotal + "/" + levelLimit;
        if (levelLimit <= 0)
        {
            icon.color = Color.gray;
            countTMP.text = " ";
        }
    }


    private void OnEnable()
    {
        TileCreator.tilePlaced += LowerCount;
        TileCreator.tileRemoved += RaiseCount;
    }

    private void OnDisable()
    {
        TileCreator.tilePlaced -= LowerCount;
        TileCreator.tileRemoved -= RaiseCount;
    }

    private void placeModeInvoked()
    {
        if (currentTotal != 0)
        {
            tileButtonClicked?.Invoke(itemForButton);
        }
    }

    void LowerCount()
    {
        
            currentTotal -= 1;
            countTMP.text = currentTotal + "/" + levelLimit;
        
        if (currentTotal <= 0)
        {
            currentTotal = 0;
            icon.color = Color.gray;
            countTMP.text = " ";
            noMoreTiles?.Invoke();
        }
    }

    void RaiseCount()
    {
        if (levelLimit != 0)
        {
            currentTotal += 1;
            countTMP.text = currentTotal + "/" + levelLimit;
            icon.color = Color.white;
            tilesReplinished?.Invoke();
        }
        
    }

}
