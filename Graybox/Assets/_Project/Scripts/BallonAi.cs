using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallonAi : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] Button pauseButton;

    public float speed;
    public float distance;
    private bool movingRight = true;
    private bool isActive = false;
    public Transform groundDetection;
    private Animator ballonAnimator;


    private void Play()
    {
        isActive = true;
    }

    private void Start()
    {
        ballonAnimator = gameObject.GetComponent<Animator>();
        PlaybackControl.play += Play;
    }


    private void Update()
    {
        if (isActive)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if (groundInfo.collider == false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, 0, -180);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }
}
