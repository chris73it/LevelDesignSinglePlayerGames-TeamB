using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerController : MonoBehaviour
{
    // class level reference setup
    private Animator playerAnimator;
    private GameObject thisPlayer;
    private Rigidbody2D playerRB;
    //logical bools
    private bool isWalking = false;
    //private bool isPaused = false;
    private bool isTouchingGround = false;
    private bool onFirstCollision = false;
    //inspector editable variables for jump and movement speed
    private float currentSpeed;
    [SerializeField] float playerSpeed;
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

    enum States
    {
        right = 0,
        left
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
        }
    }

    public void Restart()
    {
        Destroy(gameObject);
        Respawn?.Invoke();
    }

    /* commented out Ethan's changes here since we've shifted from Pause to Restart
    public void Pause()
    {
        if (!onFirstCollision)
        {
            playerAnimator.SetBool("isPaused", true);
            // We don't want to simulate physics while the game is paused because that would make it possible for the character
            // to keep falling when paused.
            playerRB.simulated = false;
            isPaused = true;
        }
    }
    */

    //Movement methods

    public void Accelerate(float factor)
    {
        currentSpeed += factor;
    }
    
    // Moved the MoveRight function into one function and made it bidirectional.
    public void Move(float speed)
    {
        if (isWalking == true)
        {
            //transform.Translate(Vector3.right * playerSpeed);

            // By adding a force rather than simply translating the player character's movement, we can make it possible to
            // simulate certain surfaces such as ice or mud, as well as make movement more realistic overall.
            if ((currentSpeed < maxMoveSpeed && state == States.right) || (currentSpeed > -(maxMoveSpeed) && state == States.left))
            {
                Accelerate(speed);
                //playerRB.AddForce(new Vector2(playerSpeed, 0), ForceMode2D.Impulse);  
            }
            playerRB.velocity = new Vector2(currentSpeed, playerRB.velocity.y);
        }
    }
    public void TurnAround(bool right)
    {
        switch (right)
        {
            case false:
                state = States.left;
                break;
            case true:
                state = States.right;
                break;
        }
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void Jump()
    {
        if (isTouchingGround == true && isPaused == false)
        {
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
    }

    // Update is called once per frame
    void Update()
    {
        if(isGhostMode && !inGhostTrigger) {
            List<Collider2D> cols = new();
            GetComponent<BoxCollider2D>().OverlapCollider(new ContactFilter2D(), cols);
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

#if UNITY_EDITOR
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
#endif
        //!! BROKEN !! Debug turn around. We only want to actually use this with power ups.
        /*if (Input.GetKeyDown(KeyCode.L))
        {
            TurnAround();
        }*/

    }

    //called every physics frame so the player keeps moving
    private void FixedUpdate()
    {
        switch (state) {
            case States.right:
                Move(playerSpeed);
                break;
            case States.left:
                Move(-playerSpeed);
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

    //ground check so player can't double jump
    //***requires "Ground" tag for any jumpable objects***
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isTouchingGround = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isTouchingGround = false;
        }
    }
}
