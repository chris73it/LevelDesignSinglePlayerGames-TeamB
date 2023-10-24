using UnityEngine;

// draws the player start as a gizmo so you see it in the scene but not the running game
public class IconGizmo : MonoBehaviour
{
    [SerializeField] Vector3 gizmoSize = new Vector3(1f, 1.5f, 1f);
    [SerializeField] Color gizmoColor = new Color(0, 255, 0);

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor; 
        Gizmos.DrawCube(this.transform.position, gizmoSize); 
    }
}
