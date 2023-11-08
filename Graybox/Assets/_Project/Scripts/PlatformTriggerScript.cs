using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class PlatformTriggerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent onPlayerCollide;
    void Start() {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player"){
            onPlayerCollide.Invoke();
        }
    }

    // Update is called once per frame
    void Update() {
        
    }
}
