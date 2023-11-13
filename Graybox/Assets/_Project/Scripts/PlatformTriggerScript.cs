using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class PlatformTriggerScript : MonoBehaviour {
    public UnityEvent onPlayerCollide;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            onPlayerCollide.Invoke();
        }
    }
}
