using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToOrigin : MonoBehaviour {
    Vector3 origin;

    // Start is called before the first frame update
    void Start() {
        origin = transform.position;
        PlayerController.Respawn += Reset;
    }

    private void Reset() {
        transform.position = origin;
    }
}
