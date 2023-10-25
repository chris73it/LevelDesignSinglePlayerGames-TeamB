using UnityEngine;
using System;

//attaches to objects that do damage, invokes the damage delegate when it's collider is entered
public class DoesDamage : MonoBehaviour
{
    //could be useful to rewrite as "public static event Action<Collision2D> damage;" so that the delegate can pass in the object that collided
    public static event Action damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        damage?.Invoke(); 
    }

}
