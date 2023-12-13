using System;
using UnityEngine;

// attaches to the "turn around" pickup and makes the player turn around.
public class TurnPickup : MonoBehaviour
{
    // Start is called before the first frame update
    public static event Action turnaround;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.isTrigger) {
            Debug.Log(collision.gameObject);
            Debug.Log("turn around");
            turnaround?.Invoke();
        }
    }
}
