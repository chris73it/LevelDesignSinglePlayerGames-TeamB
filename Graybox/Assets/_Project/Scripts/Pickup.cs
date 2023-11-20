using System;
using UnityEngine;

// attaches to the pickups and has its own behavior on enable and disable.
public class Pickup : MonoBehaviour
{
    [SerializeField] protected GameObject pickup;
    private void OnEnable()
    {
        PlayerController.respawn -= Replace;
        GetComponent<Renderer>().enabled = true;
    }

    private void OnDisable()
    {
        PlayerController.respawn += Replace;
        GetComponent<Renderer>().enabled = false;
    }

    private void Replace()
    {
        pickup.SetActive(true);
    }
}
