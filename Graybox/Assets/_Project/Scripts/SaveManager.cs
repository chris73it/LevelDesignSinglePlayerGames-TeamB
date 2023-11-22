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
        // We don't want to destroy the save manager on load, since we want to access it whenever we like during gameplay.
        DontDestroyOnLoad(this);
        LoadGame();
    }

    int timesAwaken = 0;
    private void Awake()
    {
        timesAwaken++;
        if(timesAwaken >= 2)
        {
            Debug.Log("WARNING: There can only be one save manager. Please delete the duplicate in your scene.");
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveGame()
    {
        ES3.CreateBackup("save.dat");
        ES3.Save("unlockedLevels", unlockedLevels);
        ES3.Save("sound", sound);
        ES3.Save("music", sound);
        ES3.CreateBackup("save.dat");
#if UNITY_EDITOR
        Debug.Log("Saved game. Unlocked Levels - " + unlockedLevels);
#endif
    }

    public void LoadGame()
    {
        try {
            unlockedLevels = ES3.Load("unlockedLevels", 1);
            music = ES3.Load("music", true);
            sound = ES3.Load("sound", true);
        }
        catch
        {
            if(ES3.RestoreBackup("save.dat")) {
                #if UNITY_EDITOR 
                Debug.Log("Save file was corrupted. Restored Successfully.");
#endif
                ES3.CreateBackup("save.dat");
                LoadGame();
            }
            else {
                #if UNITY_EDITOR
                Debug.Log("Save File was corrupted. Restore failed. Deleting corrupted file.");
#endif
                ES3.DeleteFile("save.dat");
            }
        }
        
#if UNITY_EDITOR
        Debug.Log("Loaded game. Unlocked Levels - " + unlockedLevels);
#endif
    }

// For debugging purposes. Increments the level unlocked counter by one.
    public void SetUnlockedLevels(){
        unlockedLevels += 1;
    }
}
