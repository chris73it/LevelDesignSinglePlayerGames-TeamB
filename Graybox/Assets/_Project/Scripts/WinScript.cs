using System;
using UnityEngine;

public class WinScript : MonoBehaviour {
    public static event Action onWin;
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player")
            onWin?.Invoke();
    }

    private void Start() {
        onWin += WinLog;
    }

    void WinLog() {
        Debug.Log("wow you won good job");
    }
}
