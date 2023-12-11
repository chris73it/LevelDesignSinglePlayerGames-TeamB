using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // class level reference setup
    private Animator playerAnimator;
    private GameObject thisPlayer;
    private Rigidbody2D playerRB;
    //logical bools
    private bool isWalking = false;
    private bool onFirstCollision = false;
    //inspector editable variables for jump and movement speed
    private float currentSpeed;
    [SerializeField] float playerSpeed;
    float speedScalar = 1;
    [SerializeField] float maxMoveSpeed;
    [SerializeField] float jumpForce = 400f;
    //inspector editable variable for delay from message of death to destruction and respawn
    [SerializeField] float deathDelay = 2.25f;
    private bool isPaused;
    private bool isGhostMode = false;
    private List<Collider2D> colliders;
    private bool inGhostTrigger = false;
    public Vector2 ghostVelocityScalars = new(1.5f, 1.5f);

    //delegate to invoke a respawn message upon death
    public static event Action Respawn;

    Collider2D trigger;

    enum States
    {
        right = 1,
        left = -1
    }

    States state = States.right;

    //animation controls 
    public void Play()
    {
        if (!onFirstCollision)
        {
            playerAnimator.SetBool("isWalking", true);
            isWalking = true;
            // Simulate physics again when play button is pressed.
            playerRB.simulated = true;
            playerAnimator.SetBool("isPaused", false);
            isPaused = false;
            state = States.right;
        }
    }

    public void Restart()
    {
        Destroy(gameObject);
        Respawn?.Invoke();
    }
    
    // Moved the MoveRight function into one function and made it bidirectional.
    public void Move(float speed)
    {
        if (isWalking == true)
        {
            if ((playerRB.velocity.x < maxMoveSpeed && state == States.right) || (playerRB.velocity.x > -(maxMoveSpeed) && state == States.left))
            {
                //Accelerate(speed);

                
                playerRB.AddForce(new(speed, 0), ForceMode2D.Impulse);  
            } else {
                playerRB.velocity = new(Mathf.Clamp(playerRB.velocity.x, -maxMoveSpeed, maxMoveSpeed), playerRB.velocity.y);
            }
            //playerRB.velocity = new Vector2(currentSpeed, playerRB.velocity.y);
        }
    }
    public void TurnAround()
    {
        //playerRB.velocity *= new Vector2(-1f, 1f);
        switch (state)
        {
            case States.right:
                state = States.left;
                break;
            case States.left:
                state = States.right;
                break;
        }
    }

    public void EnterRampMode() {
        speedScalar = Mathf.Sqrt(2) * 2;
    }

    public void ExitRampMode() {
        speedScalar = 1;
    }

    public void Jump()
    {
        if (isPaused == false)
        {
            playerRB.velocity = new(playerRB.velocity.x, 0);
            playerRB.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    //WaitForSeconds method used so the death animation had time to play before player is destroyed
    IEnumerator Die()
    {
        yield return new WaitForSeconds(deathDelay);
        Respawn?.Invoke();
        Destroy(thisPlayer);
    }

    void Dying()
    {
        if (onFirstCollision == false)
        {
            onFirstCollision = true;
            StartCoroutine(Die());
            playerAnimator.SetBool("isDead", true);
            playerRB.simulated = false;
            thisPlayer = gameObject;
        }
    }

    private void Start()
    {
        // grab references
        playerAnimator = gameObject.GetComponent<Animator>();
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        // prevent jumping before pressing play 
        playerAnimator.SetBool("isPaused", true);
        isPaused = true;

        colliders = new();
        playerRB.GetAttachedColliders(colliders);
        trigger = colliders.Find(collider => collider.isTrigger);
    }

    // Update is called once per frame
    void Update()
    {
        if(isGhostMode && !inGhostTrigger) {
            List<Collider2D> cols = new();
            trigger.OverlapCollider(new ContactFilter2D(), cols);
            bool inWall = false;

            foreach(Collider2D col in cols) {
                if(col.tag != "Player") { // temp; figure out a way to specify only walls
                    inWall = true;
                    break;
                }
            }
            if(!inWall) {
                EndGhost();
            }
        }

        // continuation from previous scripts; likely would only call the jump method from jumpy blocks not player control
        if (Input.GetKeyDown(KeyCode.J))
        {
            Jump();
        }

        // Debug kill
        if (Input.GetKeyDown(KeyCode.K))
        {
            Dying();
        }

        // Debug turn around. We only want to actually use this with power ups.
        if (Input.GetKeyDown(KeyCode.L))
        {
            TurnAround();
        }

    }

    //called every physics frame so the player keeps moving
    private void FixedUpdate()
    {
        switch (state) {
            case States.right:
                Move(playerSpeed * speedScalar);
                break;
            case States.left:
                Move(-playerSpeed * speedScalar);
                break;
            default:
                break;
        }
    }

    // starts intangibility; wall checks not performed
    public void GhostTriggerEnter() {
        isGhostMode = true;
        playerRB.velocity *= ghostVelocityScalars;
        playerRB.isKinematic = true;
        GetComponent<SpriteRenderer>().color = Color.cyan;
        inGhostTrigger = true;
        isWalking = false;
    }

    // upon exiting the intangibility triger, start performing wall checks to end intangibility
    public void GhostTriggerExit() {
        GetComponent<SpriteRenderer>().color = Color.blue;
        inGhostTrigger = false;
    }


    void EndGhost() {
        isGhostMode = false;
        playerRB.isKinematic = false;
        GetComponent<SpriteRenderer>().color = Color.white;
        isWalking = true;
    }

    //subscribing and unsubscribing from delegates in other scripts 
    private void OnEnable()
    {
        TurnPickup.turnaround += TurnAround;
        DoesDamage.damage += Dying;
        PlaybackControl.play += Play;
        PlaybackControl.restart += Restart;
    }

    private void OnDisable()
    {
        TurnPickup.turnaround -= TurnAround;
        DoesDamage.damage -= Dying;
        PlaybackControl.play -= Play;
        //PlaybackControl.restart -= Pause;
        PlaybackControl.restart -= Restart;
    }
}
