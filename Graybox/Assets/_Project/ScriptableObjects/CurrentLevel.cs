using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CurrentLevel", order = 2)]
public class CurrentLevel : ScriptableObject
{
    [HideInInspector]
    public int currentLevel;
}
