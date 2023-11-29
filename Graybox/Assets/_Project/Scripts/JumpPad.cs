using UnityEngine;

public class JumpPad : MonoBehaviour {
    private bool isTouchingPlayer = false;
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player" && !isTouchingPlayer) {
            Debug.Log(collision.gameObject.name + " jumping");
            collision.gameObject.SendMessage("Jump");
            GetComponent<Animator>().SetTrigger("jump");
            isTouchingPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            isTouchingPlayer = false;
        }
    }
}
