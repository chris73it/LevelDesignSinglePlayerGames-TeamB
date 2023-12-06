using System.Collections;
using UnityEngine;

public class EvilBoxAI : MonoBehaviour {
    public Transform groundDetection;
    public float speed = 3f;
    public float groundDistance = 0.5f;

    bool isActive = false;
    bool movingRight = true;

    GhostAI patrol;
    DoesDamage damage;
    Rigidbody2D rb;

    void Play() {
        isActive = true;
        damage.enabled = false;
        patrol.enabled = false;
    }
    // Start is called before the first frame update
    void Start() {
        PlaybackControl.play += Play;

        patrol = GetComponent<GhostAI>();
        PlaybackControl.play -= patrol.Play;

        damage = GetComponent<DoesDamage>();
        rb = GetComponent<Rigidbody2D>();

        PlayerController.Respawn += Reset;
    }

    void Reset() {
        damage.enabled = false;
        patrol.enabled = false;
        isActive = false;
    }


    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.S)) {
            patrol.enabled = true;
            patrol.Play();
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player") {
            StartCoroutine(Awaken(1f, collision));
        }
    }
    

    IEnumerator Awaken(float time, Collision2D collision) {
        if(!isActive) {
            yield break;
        }
        yield return new WaitForSeconds(time);
        if(!isActive) yield break;
        rb.bodyType = RigidbodyType2D.Dynamic;
        patrol.enabled = true;
        patrol.Play();
        damage.enabled = true;

        if(patrol.movingRight != (collision.gameObject.transform.position.x > transform.position.x)) {
            patrol.TurnAround();
        }
    }
}
