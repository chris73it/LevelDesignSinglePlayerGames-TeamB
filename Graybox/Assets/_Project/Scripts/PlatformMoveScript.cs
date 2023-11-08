using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MoveState {
    Moving, Waiting
}

public class PlatformMoveScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject targetObject;
    public float speed = 5f;
    Vector3 origin;
    MoveState moveState = MoveState.Waiting;
    Rigidbody2D rb;
    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    public void BeginMovement() {
        if(moveState != MoveState.Waiting) return;
        Vector3 displacement = targetObject.transform.position - transform.position;
        rb.velocity = displacement.normalized * speed;
        StartCoroutine(EndMoveIE(displacement.magnitude / speed));
        moveState = MoveState.Moving;
        origin = transform.position;
    }

    IEnumerator EndMoveIE(float time) {
        yield return new WaitForSeconds(time);
        moveState = MoveState.Waiting;
        rb.velocity = Vector2.zero;
        targetObject.transform.position = origin;
    }
}
