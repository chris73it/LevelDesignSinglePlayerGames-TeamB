using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GhostAI : MonoBehaviour
{
    public GameObject pointC;
    public GameObject pointD;
    public Rigidbody2D rb;
    private Transform currentPoint;
    private Animator anim;
    public float speed;


    public void StopMovement()
    {
        Time.timeScale = 0;
    }

    public void StartMovement()
    {
        Time.timeScale = 1;
    }


    void Start()
    {
        rb.GetComponent<Rigidbody2D>();
        anim.GetComponent<Animator>();
        //transform.position = pointD.transform.position;
        //currentPoint = pointD.transform;

    }

    
    void Update()
    {
        
        if (transform.position == pointD.transform.position)
        {
            rb.velocity = new Vector2(speed, 0);

        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointD.transform)
        {
            currentPoint = pointC.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointC.transform)
        {
            currentPoint = pointD.transform;
        }
    }
}
