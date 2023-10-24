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

    void Start()
    {
       startButton.onClick.AddListener(PlayInvoke);
       pauseButton.onClick.AddListener(PauseInvoke);
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
