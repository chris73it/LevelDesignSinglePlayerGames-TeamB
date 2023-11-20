using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UrchinAI : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D boxCollider2D;
    Animator anim;
    public float distance;
    bool isFalling = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        PlayerController.respawn += Reset;
    }

    private void Reset() {
        isFalling = false;
        rb.gravityScale = 0;
        anim.SetBool("Falling", false);
    }

    void Update()
    {
        Physics2D.queriesStartInColliders = false;
        if (isFalling == false)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distance);

            Debug.DrawRay(transform.position, Vector2.down * distance, Color.red);

            if (hit.transform.tag == "Player")
            {
                rb.gravityScale = 5;
                isFalling = true;
                anim.SetBool("Falling", true);
            }
        }
    }
}
