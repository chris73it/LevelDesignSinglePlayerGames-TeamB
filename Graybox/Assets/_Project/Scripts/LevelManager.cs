using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public CurrentLevel level;
    public LevelTracker tracker;
    public Button level1BT;
    public Button level2BT;
    public Button level3BT;
    public Button level4BT;
    public Button level5BT;
    public Button level6BT;
    public Button level7BT;
    public Button level8BT;
    public Button level9BT;
    public Button level10BT;
    public Button level11BT;
    public Button level12BT;

    private void Start()
    {
        level2BT.gameObject.SetActive(false);
        level3BT.gameObject.SetActive(false);
        level4BT.gameObject.SetActive(false);
        level5BT.gameObject.SetActive(false);
        level6BT.gameObject.SetActive(false);
        level7BT.gameObject.SetActive(false);
        level8BT.gameObject.SetActive(false);
        level9BT.gameObject.SetActive(false);
        level10BT.gameObject.SetActive(false);
        level11BT.gameObject.SetActive(false);
        level12BT.gameObject.SetActive(false);
    }

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
        level.currentLevel = 1;
        SceneManager.LoadScene("FinalLevel1", LoadSceneMode.Single);
    }
    public void EnterLevel2()
    {
        level.currentLevel = 2;
        SceneManager.LoadScene("FinalLevel2", LoadSceneMode.Single);
    }
    public void EnterLevel3()
    {
        level.currentLevel = 3;
        SceneManager.LoadScene("FinalLevel3", LoadSceneMode.Single);
    }
    public void EnterLevel4()
    {
        level.currentLevel = 4;
        SceneManager.LoadScene("FinalLevel4", LoadSceneMode.Single);
    }
    public void EnterLevel5()
    {
        level.currentLevel = 5;
    }
    public void EnterLevel6()
    {
        level.currentLevel = 6;
    }
    public void EnterLevel7()
    {
        level.currentLevel = 7;
    }
    public void EnterLevel8()
    {
        level.currentLevel = 8;
    }
    public void EnterLevel9()
    {
        level.currentLevel = 9;
    }
    public void EnterLevel10()
    {
        level.currentLevel = 10;
    }
    public void EnterLevel11()
    {
        level.currentLevel = 11;
    }
    public void EnterLevel12()
    {
        level.currentLevel = 12;
    }

    private void CheckProgress()
    {
        if (tracker.completedLevels[1] == true)
        {
            tracker.firstGate = true;
            UpdateMap();
        }
        if (tracker.completedLevels[2] == true && tracker.completedLevels[3] == true && tracker.completedLevels[4] == true)
        {
            tracker.secondGate = true;
            UpdateMap();
        }
        if (tracker.completedLevels[5] == true)
        {
            tracker.thirdGate = true;
            UpdateMap();
        }
        if (tracker.completedLevels[6] == true && tracker.completedLevels[7] == true && tracker.completedLevels[8] == true)
        {
            tracker.fourthGate = true;
            UpdateMap();
        }
        if (tracker.completedLevels[9] == true)
        {
            tracker.fifthGate = true;
            UpdateMap();
        }
        if (tracker.completedLevels[10] == true && tracker.completedLevels[11] && tracker.completedLevels[12] == true)
        {
            tracker.sixthGate = true;
            UpdateMap();
        }
    }

    private void UpdateMap()
    {
        if (tracker.firstGate == true)
        {
            level2BT.gameObject.SetActive(true);
            level3BT.gameObject.SetActive(true);
            level4BT.gameObject.SetActive(true);
        }
    }
}
