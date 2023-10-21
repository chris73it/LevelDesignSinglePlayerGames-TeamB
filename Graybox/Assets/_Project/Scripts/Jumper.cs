using System; 
using UnityEngine;

public class Jumper : MonoBehaviour
{
    private Rigidbody2D rb;
    public float jumpForce = 200f;
    private Collider2D jumper_Collider;
    private bool isTouchingGround = true;

    public void Jump()
    {
        if (isTouchingGround == true)
        {
            Debug.Log("signal through");
            rb.AddForce(new Vector2(0, jumpForce * Time.deltaTime));
        }
    }

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        jumper_Collider = this.GetComponent<PolygonCollider2D>();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isTouchingGround = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isTouchingGround = true;
    }

    private void OnEnable()
    {
        PlayerController.OnSpacePressed += Jump;
    }

    private void OnDisable()
    {
        PlayerController.OnSpacePressed += Jump;
    }

}
