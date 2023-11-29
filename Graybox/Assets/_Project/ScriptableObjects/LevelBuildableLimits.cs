using UnityEngine;

[CreateAssetMenu(menuName = "LevelLimits", order = 0)]
public class LevelBuildableLimits : ScriptableObject
{
    public int blockLimit = 1;
    public int rampLimit = 1;
    public int jumpLimit = 0;
    public int directionLimit = 0;
    public int wallsLimit = 0; 
}
