using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToOrigin : MonoBehaviour {
    Vector3 origin;
    Quaternion rotation;
    // Start is called before the first frame update
    void Start() {
        origin = transform.position;
        rotation = transform.rotation;
        PlayerController.Respawn += Reset;
    }

    private void Reset() {
        transform.position = origin;
        transform.rotation = rotation;
    }
}
