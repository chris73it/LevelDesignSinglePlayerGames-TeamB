using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // class level reference setup
    private Animator playerAnimator;
    private GameObject thisPlayer;
    private Rigidbody2D playerRB;
    //logical bools
    private bool isWalking = false;
    private bool isPaused = false;
    private bool isTouchingGround = true;
    private bool onFirstCollision = false;
    //inspector editable variables for jump and movement speed
    [SerializeField] float playerSpeed;
    [SerializeField] float jumpForce = 600f;
    //inspector editable variable for delay from message of death to destruction and respawn
    [SerializeField] float deathDelay = 2.25f;

    //delegate to invoke a respawn message upon death
    public static event Action respawn;

    //animation controls 
    public void Play()
    {
        playerAnimator.SetBool("isWalking", true);
        isWalking = true;
        playerAnimator.SetBool("isPaused", false);
        playerRB.WakeUp();
        isPaused = false;
    }

    public void Pause()
    {
        playerAnimator.SetBool("isPaused", true);
        isPaused = true;
        playerRB.Sleep();
    }

    //Movement methods
    public void MoveRight()
    {
        if (isWalking == true && isPaused == false)
        {

            //might be better to rewrite as "rb.AddForce(new Vector2(playerSpeed, 0), ForceMode2D.Impulse);"
            //not sure if the translate method here is affected by drag and other physics attributes of the ground (might want slick ice ground or sticky mud ground). worth investigating?
            transform.Translate(Vector3.right * playerSpeed);
        }
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
        respawn?.Invoke();
        Destroy(thisPlayer);
    }

    void Dying()
    {
        if (onFirstCollision == false)
        {
            onFirstCollision = true;
            StartCoroutine(Die());
            playerAnimator.SetBool("isDead", true);
            playerRB.Sleep();
            isWalking = false;
            thisPlayer = GameObject.FindWithTag("Player");
        }
    }

    private void Start()
    {
        // grab references
        playerAnimator = gameObject.GetComponent<Animator>();
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        // prevent jumping before pressing play 
        Pause();
    }

    // Update is called once per frame
    void Update()
    {
        // continuation from previous scripts; likely would only call the jump method from jumpy blocks not player control
        if (Input.GetKeyDown(KeyCode.J))
        {
            Jump();
        }

        // Debug kill
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("k pressed");
            Dying();
        }
    }

    //called every physics frame so the player keeps moving
    private void FixedUpdate()
    {
        MoveRight();
    }

    //subscribing and unsubscribing from delegates in other scripts 
    private void OnEnable()
    {
        DoesDamage.damage += Dying;
        PlaybackControl.play += Play;
        PlaybackControl.pause += Pause;
    }

    private void OnDisable()
    {
        DoesDamage.damage -= Dying;
        PlaybackControl.play -= Play;
        PlaybackControl.pause -= Pause; 
    }

    //ground check so player can't double jump
    //***requires "Ground" tag for any jumpable objects***
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isTouchingGround = true;
         /*   for(int i = 0; i < collision.contactCount; ++i) {
                if(collision.GetContact(i).point.y < 0) {
                    isTouchingGround = true;
                    break;
                }
            }*/
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
