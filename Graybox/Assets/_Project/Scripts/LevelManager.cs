using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private void OnEnable()
    {
        WinScript.onWin += CheckProgress;
        ReturnToLevelSelect.Return += CheckProgress;
    }

    private void OnDisable()
    {
        WinScript.onWin -= CheckProgress;
        ReturnToLevelSelect.Return -= CheckProgress;
    }
    public void EnterLevel1()
    {
        CurrentLevel.currentLevel = 1;
        //Debug.Log(LevelTracker.completedLevels[1]);
        SceneManager.LoadScene("FinalLevel1", LoadSceneMode.Single);
    }
    public void EnterLevel2()
    {
        CurrentLevel.currentLevel = 2;
        SceneManager.LoadScene("FinalLevel2", LoadSceneMode.Single);
    }
    public void EnterLevel3()
    {
        CurrentLevel.currentLevel = 3;
        SceneManager.LoadScene("FinalLevel3", LoadSceneMode.Single);
    }
    public void EnterLevel4()
    {
        CurrentLevel.currentLevel = 4;
        SceneManager.LoadScene("FinalLevel4", LoadSceneMode.Single);
    }
    public void EnterLevel5()
    {
        CurrentLevel.currentLevel = 5;
        SceneManager.LoadScene("FinalLevel5", LoadSceneMode.Single);
    }
    public void EnterLevel6()
    {
        CurrentLevel.currentLevel = 6;
        SceneManager.LoadScene("FinalLevel6", LoadSceneMode.Single);
    }
    public void EnterLevel7()
    {
        CurrentLevel.currentLevel = 7;
        SceneManager.LoadScene("FinalLevel7", LoadSceneMode.Single);
    }
    public void EnterLevel8()
    {
        CurrentLevel.currentLevel = 8;
        SceneManager.LoadScene("FinalLevel8", LoadSceneMode.Single);
    }
    public void EnterLevel9()
    {
        CurrentLevel.currentLevel = 9;
        SceneManager.LoadScene("FinalLevel9", LoadSceneMode.Single);
    }
    public void EnterLevel10()
    {
        CurrentLevel.currentLevel = 10;
        SceneManager.LoadScene("FinalLevel10", LoadSceneMode.Single);
    }
    public void EnterLevel11()
    {
        CurrentLevel.currentLevel = 11;
        SceneManager.LoadScene("FinalLevel11", LoadSceneMode.Single);
    }
    public void EnterLevel12()
    {
        CurrentLevel.currentLevel = 12;
        SceneManager.LoadScene("FinalLevel12", LoadSceneMode.Single);
    }

    private void CheckProgress()
    {
        if (LevelTracker.completedLevels[1] == true)
        {
            LevelTracker.firstGate = true;

        }
        if (LevelTracker.completedLevels[2] == true & LevelTracker.completedLevels[3] == true & LevelTracker.completedLevels[4] == true)
        {
            LevelTracker.secondGate = true;

        }
        if (LevelTracker.completedLevels[5] == true)
        {
            LevelTracker.thirdGate = true;
        }
        if (LevelTracker.completedLevels[6] == true & LevelTracker.completedLevels[7] == true & LevelTracker.completedLevels[8] == true)
         {
                LevelTracker.fourthGate = true;

         }
        if (LevelTracker.completedLevels[9] == true)
            {
                LevelTracker.fifthGate = true;

            }
         if (LevelTracker.completedLevels[10] == true & LevelTracker.completedLevels[11] & LevelTracker.completedLevels[12] == true)
            {
                LevelTracker.sixthGate = true;
            }
        }
}
