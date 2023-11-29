using System;
using UnityEngine;

// Attaches to the "boost" power up and boosts the player's speed for a specified duration.
public class BoostPickup : Pickup
{
    public static event Action boost;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && enabled)
        {
            boost?.Invoke();
            pickup.SetActive(false);
        }

    }
}
