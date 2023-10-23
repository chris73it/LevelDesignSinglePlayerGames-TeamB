using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{

    public Collider2D collider;
    // Start is called before the first frame update

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            
        }
    }
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
