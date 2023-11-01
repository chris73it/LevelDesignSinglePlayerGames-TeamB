using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BuildingBlocks : MonoBehaviour
{
    private CanvasRenderer placeModeBackground;
    [SerializeField] Button hideButton;
    public static event Action dismiss; 

    // Start is called before the first frame update
    void Start()
    {
        placeModeBackground = GetComponent<CanvasRenderer>(); 
        hideButton.onClick.AddListener(TogglePlaceModeInactive);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnEnable()
    {
        PlaybackControl.pause += TogglePlaceModeInactive;
    }

    private void OnDisable()
    {
        PlaybackControl.pause -= TogglePlaceModeInactive;
    }

    public void TogglePlaceModeInactive()
    {
        dismiss?.Invoke();
    }
}
