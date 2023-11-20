using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostAI : MonoBehaviour {

    public float speed;
    public float distance;
    public bool movingRight = true;
    private bool isActive = false;
    public Transform groundDetection;


    public void Play() {
        isActive = true;
    }

    private void Start() {
        PlaybackControl.play += Play;
        PlayerController.respawn += Reset;
    }


    private void Update() {
        if(isActive) {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        Debug.DrawRay(groundDetection.position, Vector2.down * distance, Color.red);
        if(groundInfo.collider == null) {
            TurnAround();
        }
    }

    private void Reset() {
        isActive = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(isActive) {
            TurnAround();
            Debug.Log(collision.gameObject.name);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(isActive) {
            TurnAround();
        }
    }

    public void TurnAround() {
        Debug.Log("turning around");
        if(movingRight == true) {
            transform.eulerAngles = new Vector3(0, -180, 0);
            movingRight = false;
        } else {
            transform.eulerAngles = new Vector3(0, 0, 0);
            movingRight = true;
        }
    }

}
