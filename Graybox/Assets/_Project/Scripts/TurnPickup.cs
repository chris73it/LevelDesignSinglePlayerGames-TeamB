using System;
using UnityEngine;

// attaches to the "turn around" pickup and makes the player turn around.
public class TurnPickup : MonoBehaviour
{
    // Start is called before the first frame update
    public static event Action<bool> turnaround;
    [SerializeField] public bool right;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && enabled)
        {
            turnaround?.Invoke(right);
            //pickup.SetActive(false);
        }

    }
}
