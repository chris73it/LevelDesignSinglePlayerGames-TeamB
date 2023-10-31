using UnityEngine;
using UnityEngine.UI;
using System;

// invokes play and pause events when UI (PlayBT and PauseBT on PlayControsl prefab) is...
// clicked so play and pause state can multi-cast if needed

public class PlaybackControl : MonoBehaviour
{
   [SerializeField] Button startButton;
   [SerializeField] Button pauseButton;
    public static event Action play;
    public static event Action pause;
    //reference to attached UI canvas; you should know you'll have this since it's a UI based script
    private Canvas canvas;

    void Start()
    {
        // adds onClick event listeners to the UI buttons (my preference over dragging in the Inspector, could be wrong)
        startButton.onClick.AddListener(PlayInvoke);
        pauseButton.onClick.AddListener(PauseInvoke);

        canvas = GetComponent<Canvas>();
        // assigns the main camera as the render camera for the Canvas, only works if we stick with main camera going forward
        canvas.worldCamera = Camera.main;
    }

    public void PlayInvoke()
    {
        play?.Invoke();
    }

    public void PauseInvoke()
    {
        pause?.Invoke();
    }
}
