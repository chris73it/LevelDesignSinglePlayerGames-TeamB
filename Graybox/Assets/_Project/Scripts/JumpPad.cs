using UnityEngine;

public class JumpPad : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            collision.gameObject.SendMessage("Jump");
            GetComponent<Animator>().SetTrigger("jump");
        }
    }
}
