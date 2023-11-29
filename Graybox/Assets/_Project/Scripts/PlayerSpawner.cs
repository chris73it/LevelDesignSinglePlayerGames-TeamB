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
       PlayerController.Respawn += Spawn;
    }

    private void OnDisable()
    {
       PlayerController.Respawn -= Spawn;
    }

    void Spawn()
    {
        Instantiate(playerCharacter, transform.position, Quaternion.identity.normalized);
    }

}
