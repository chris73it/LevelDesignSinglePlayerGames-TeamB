using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{ 
    public static event Action OnJumpPressed;
    public static event Action OnDeath;
    public Button playButton;
    public Button pauseButton;
    private Vector3 playerPos;
    private Animator playerAnimator;
    private bool isWalking = false;
    private bool isPaused = false;
    private Rigidbody2D rb;
    [SerializeField] float playerSpeed;

    public void Play()
    {
        playerAnimator.SetBool("isWalking", true);
        isWalking = true;
        playerAnimator.SetBool("isPaused", false);
        isPaused = false; 
    }

    public void Pause()
    {
        playerAnimator.SetBool("isPaused", true);
        isPaused = true; 
    }

    public void MoveRight()
    {
        if (isWalking == true  && isPaused == false)
        {
            //rb.AddForce(new Vector2(playerSpeed, 0), ForceMode2D.Impulse);
            transform.Translate(Vector3.right * playerSpeed);
        }
    }

    private void Start()
    {
        playerAnimator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerPos = this.transform.position; 
        playButton.onClick.AddListener(Play);
        pauseButton.onClick.AddListener(Pause);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            OnJumpPressed?.Invoke(); 
        }
    }

    private void FixedUpdate()
    {
        MoveRight();
    }
}
