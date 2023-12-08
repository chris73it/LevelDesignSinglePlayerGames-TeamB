using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelTracker", order = 1)]
public class LevelTracker : ScriptableObject
{
    [HideInInspector]
    public Dictionary<int, bool> completedLevels = new Dictionary<int, bool>();

    [HideInInspector]
    public bool firstGate = false;
    [HideInInspector]
    public bool secondGate = false;
    [HideInInspector]
    public bool thirdGate = false;
    [HideInInspector]
    public bool fourthGate = false;
    [HideInInspector]
    public bool fifthGate = false;
    [HideInInspector]
    public bool sixthGate = false;

    [SerializeField]
    private CurrentLevel levelSO; 

    private void OnEnable()
    {
        WinScript.onWin += WinState;
        completedLevels[1] = false;
        completedLevels[2] = false;
        completedLevels[3] = false;
        completedLevels[4] = false;
        completedLevels[5] = false;
        completedLevels[6] = false;
        completedLevels[7] = false;
        completedLevels[8] = false;
        completedLevels[9] = false;
        completedLevels[10] = false;
        completedLevels[11] = false;
        completedLevels[12] = false;
    }
    private void OnDisable()
    {
        WinScript.onWin -= WinState;
    }

    private void WinState()
    {
        completedLevels[levelSO.currentLevel] = true;
    }
}
