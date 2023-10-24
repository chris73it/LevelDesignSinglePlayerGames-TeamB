using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] GameObject playerCharacter;

    private void Start()
    {
        // spawns initial Player at the PlayerStartPosition prefab in the scene
        Spawn(); 
    }

    // subscribes to PlayerController script's respawn method, called when Player dies 
    private void OnEnable()
    {
       PlayerController.respawn += Spawn;
    }

    private void OnDisable()
    {
       PlayerController.respawn -= Spawn;
    }

    void Spawn()
    {
        Instantiate(playerCharacter, transform.root.position, Quaternion.identity.normalized);
    }

}
