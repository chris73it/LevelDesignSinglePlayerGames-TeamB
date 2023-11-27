using System;
using UnityEngine;

// attaches to the "turn around" pickup and makes the player turn around.
public class TurnPickup : Pickup
{
    public static event Action turnaround;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        turnaround?.Invoke();
        pickup.SetActive(false);
    }
}
