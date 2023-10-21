using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{ 
    public static event Action OnSpacePressed;
    public Button playButton;
    public Button pauseButton;
    private Vector3 playerPos;
    private Animator playerAnimator; 

    public void Play()
    {
        Debug.Log("play");
        playerAnimator.SetBool("isWalking", true);
        playerAnimator.SetBool("isPaused", false);
        Debug.Log(playerAnimator.GetBool("isWalking"));
        Debug.Log(playerAnimator.GetBool("isPaused"));
    }

    public void Pause()
    {
        Debug.Log("pause");
        playerAnimator.SetBool("isPaused", true);
        Debug.Log(playerAnimator.GetBool("isPaused"));
    }

    private void Start()
    {
        playerAnimator = gameObject.GetComponent<Animator>(); 
        playerPos = this.transform.position; 
        playButton.onClick.AddListener(Play);
        pauseButton.onClick.AddListener(Pause);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSpacePressed?.Invoke(); 
        }
    }
}
