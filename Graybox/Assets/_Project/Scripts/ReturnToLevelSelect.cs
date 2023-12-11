using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ReturnToLevelSelect : MonoBehaviour
{
    public int returnToLevelSelect;
    public GameObject returnButton;

    public static event Action Return;

    public void ReturnToLevelSelectButton()
    {
        SceneManager.LoadScene(returnToLevelSelect);
        Return?.Invoke();
    }
}
