using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelTracker", order = 1)]
public class LevelTracker : ScriptableObject
{
    [HideInInspector]
    public Dictionary<int, bool> completedLevels = new Dictionary<int, bool>();

    [HideInInspector]
    public bool firstGate = true;

    public bool secondGate = false;
    public bool thirdGate = false;
    public bool fourthGate = false;
    public bool fifthGate = false;
}
