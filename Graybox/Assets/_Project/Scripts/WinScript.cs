using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour {
    
    public static event Action onWin;
    public string nextScene = "";
    public float sceneDelay = 0.5f;
    public int returnToLevelSelect;
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            WinLog();
            onWin?.Invoke(); 
        }      
    }

    void WinLog() {
        //Debug.Log("wow you won, no thanks to marshall");
        if(nextScene != "") {
            StartCoroutine(NextScene(sceneDelay));
        }
    }

    IEnumerator NextScene(float delay) {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nextScene);
    }

}
