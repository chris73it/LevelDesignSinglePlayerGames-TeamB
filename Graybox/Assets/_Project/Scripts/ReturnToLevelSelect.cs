using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToLevelSelect : MonoBehaviour
{
    public int returnToLevelSelect;
    public GameObject returnButton;

    public void ReturnToLevelSelectButton()
    {
        SceneManager.LoadScene(returnToLevelSelect);
    }
}
