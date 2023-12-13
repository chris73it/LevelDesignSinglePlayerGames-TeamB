using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelTracker", order = 1)]
public class LevelTracker : ScriptableObject
{
    public static Dictionary<int, bool> completedLevels = new Dictionary<int, bool>();

    [HideInInspector]
    public static bool firstGate;
    [HideInInspector]
    public static bool secondGate;
    [HideInInspector]
    public static bool thirdGate;
    [HideInInspector]
    public static bool fourthGate;
    [HideInInspector]
    public static bool fifthGate;
    [HideInInspector]
    public static bool sixthGate;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoad()
    {
            firstGate = false;
            secondGate = false;
            thirdGate = false;
            fourthGate = false;
            fifthGate = false;
            sixthGate = false;
            completedLevels[1] = false;
            completedLevels[2] = false;
            completedLevels[3] = false;
            completedLevels[4] = false;
            completedLevels[5] = false;
            completedLevels[6] = true;
            completedLevels[7] = false;
            completedLevels[8] = false;
            completedLevels[9] = false;
            completedLevels[10] = false;
            completedLevels[11] = false;
            completedLevels[12] = false;


    }
    private void OnEnable()
    {
        WinScript.onWin += WinState;
    }
    private void OnDisable()
    {
        WinScript.onWin -= WinState;
    }

    private void WinState()
    {
        completedLevels[CurrentLevel.currentLevel] = true;
    }
}
