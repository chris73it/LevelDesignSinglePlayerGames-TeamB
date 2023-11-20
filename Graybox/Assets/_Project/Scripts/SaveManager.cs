using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance {  get; set; }
    public int unlockedLevels = 1;
    public bool music = true;
    public bool sound = true;
    void Start()
    {
        LoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveGame()
    {
        ES3.Save("unlockedLevels", unlockedLevels);
        ES3.Save("sound", sound);
        ES3.Save("music", sound);
        Debug.Log("Saved game. Unlocked Levels - " + unlockedLevels);
    }

    public void LoadGame()
    {
        unlockedLevels = ES3.Load("unlockedLevels", 1);
        music = ES3.Load("music", true);
        sound = ES3.Load("sound", true);
        Debug.Log("Loaded game. Unlocked Levels - " + unlockedLevels);
    }

// For debugging purposes. Increments the level unlocked counter by one.
    public void UnlockLevel(){
        unlockedLevels += 1;
    }
}
