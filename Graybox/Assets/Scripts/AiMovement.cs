using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AiMovement : MonoBehaviour
{
    public GameObject pointA;
    public Transform pointB;
    public Button startButton;
    public Button pauseButton;
    private Rigidbody2D rb;
    public float speed;
    bool isMoving = false;
    public float jumpForce = 200;

    public void StopMovement()
    {
        Time.timeScale = 0;
        isMoving = false;
    }

    public void StartMovement()
    {
        Time.timeScale = 1;
        isMoving = true; 
    }

    public void Jump()
    {
        rb.AddForce(new Vector2(0, jumpForce));
    }

    void Die()
    {
        transform.position = pointA.transform.position;
        rb.velocity = new Vector2(0,0);
        StopMovement();
    }

    void Start()
    {
        transform.position = pointA.transform.position;
        Time.timeScale = 0;
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        //testing
        if (Input.GetKeyDown(KeyCode.Space)) Jump();

        //testing
        if (Input.GetKeyDown(KeyCode.K)) Die();
        rb.velocity = new Vector2(speed, rb.velocity.y);
        
    }
}




