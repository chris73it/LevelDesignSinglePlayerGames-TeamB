using UnityEngine;

[CreateAssetMenu(menuName = "CurrentLevel", order = 2)]
public class CurrentLevel : ScriptableObject
{
    [HideInInspector]
    public static int currentLevel = 0;
}
