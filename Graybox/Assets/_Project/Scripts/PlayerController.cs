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
    [SerializeField] float boostSpeed;
    [SerializeField] float boostDuration;
    //inspector editable variable for delay from message of death to destruction and respawn
    [SerializeField] float deathDelay = 2.25f;
    private bool isPaused;

    private bool isIntangible = false;
    private float intangStart = 0f;
    private List<Collider2D> colliders;

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

    public void Boost()
    {
        maxMoveSpeed += boostSpeed;
        StartCoroutine(DisableBoost());
    }

    IEnumerator DisableBoost()
    {
        yield return new WaitForSeconds(boostDuration);
        maxMoveSpeed -= boostSpeed;
        currentSpeed = maxMoveSpeed;
    }
    public void TurnAround()
    {
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

    public void Jump()
    {
        if (isTouchingGround == true && isPaused == false)
        {
            Debug.Log(isTouchingGround);
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
        if(transform.position.y < -50)
        {
            Dying();
        }
        if(isIntangible && Time.time - intangStart >= 0.1f) {
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
                GoTangible();
            }
        }

        if(Input.GetKeyDown(KeyCode.A)) {
            if(isIntangible) {
                GoTangible();
            } else {
                GoIntangible();
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

        // Debug turn around. We only want to actually use this with power ups.
        if (Input.GetKeyDown(KeyCode.L))
        {
            TurnAround();
        }

    }
#endif

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

    void GoIntangible() {
        isIntangible = true;
        playerRB.isKinematic = true;
        intangStart = Time.time;
    }

    void GoTangible() {
        isIntangible = false;
        playerRB.isKinematic = false;
    }

    //subscribing and unsubscribing from delegates in other scripts 
    private void OnEnable()
    {
        BoostPickup.boost += Boost;
        TurnPickup.turnaround += TurnAround;
        DoesDamage.damage += Dying;
        PlaybackControl.play += Play;
        //PlaybackControl.restart += Pause;
        PlaybackControl.restart += Restart;
    }

    private void OnDisable()
    {
        BoostPickup.boost -= Boost;
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
