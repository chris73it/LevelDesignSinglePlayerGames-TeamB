using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostModeTrigger : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player" && collision.isTrigger) {
            collision.gameObject.SendMessage("GhostTriggerEnter"); // do this better
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.tag == "Player" && collision.isTrigger) {
            collision.gameObject.SendMessage("GhostTriggerExit"); // do this better
        }
    }
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
