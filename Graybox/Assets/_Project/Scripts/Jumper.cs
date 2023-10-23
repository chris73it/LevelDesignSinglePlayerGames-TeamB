using System; 
using UnityEngine;

public class Jumper : MonoBehaviour
{
    public static event Action OnJumpPressed;
    private Rigidbody2D rb;
    public float jumpForce = 400f;
    private bool isTouchingGround = true;
    [SerializeField] Transform checkBox;
    // required field for physics2d.overlapbox, can be used to filter out certain collisions
    [SerializeField] ContactFilter2D filter;
    Collider2D[] results = new Collider2D[1];

    public void Jump()
    {
        if (isTouchingGround == true)
        {
            Debug.Log("signal through");
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Physics2D.OverlapBox(checkBox.position, new Vector2(checkBox.localScale.x, checkBox.localScale.y), 0, filter.NoFilter(), results) > 0)
        {
            isTouchingGround = true;
        }
        else
        {
            Debug.Log("setting to not touch");
            isTouchingGround = false;
        }

        

    }

    /*
    private void OnCollisionExit2D(Collision2D collision)
    {
        isTouchingGround = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isTouchingGround = true;
    }
    */

    

}
