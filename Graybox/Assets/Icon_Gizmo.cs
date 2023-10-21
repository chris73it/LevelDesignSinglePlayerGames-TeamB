using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon_Gizmo : MonoBehaviour
{
    public Vector3 gizmoSize = new Vector3(1f, 1.5f, 1f);
    public Color gizmoColor = new Color(0, 255, 0);
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor; 
        Gizmos.DrawCube(this.transform.position, gizmoSize); 
    }
}
